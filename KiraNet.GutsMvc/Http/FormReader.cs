using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Primitives;
using System.Buffers;
using System.Threading.Tasks;
using System.Threading;
using KiraNet.GutsMVC.Helper;

namespace KiraNet.GutsMVC
{
    ///补充：
    ///form的enctype属性为编码方式，常用有两 种：application/x-www-form-urlencoded和multipart/form-data
    ///默认为application /x-www-form-urlencoded。 当action为get时候，浏览器用x-www-form-urlencoded的编码方式把form数据转换成一个字串（name1=value1& amp;name2=value2...），然后把这个字串append到url后面，用?分割，加载这个新的url。 
    ///当action为post时候，浏览器把form数据封装到http body中，然后发送到server。 如果没有type=file的控件，用默认的application/x-www-form-urlencoded就可以了。 但是如果有type=file的话，就要用到multipart/form-data了。浏览器会把整个表单以控件为单位分割，并为每个部分加上 Content-Disposition(form-data或者file),Content-Type(默认为text/plain),name(控件 name)等信息，并加上分割符(boundary)。
   
    /// <summary>
    /// 用于处理GET的表单数据
    /// enctype 默认值为 'application/x-www-form-urlencoded' 数据被编码为键值对集合
    /// </summary>
    public class FormReader : IDisposable
    {
        public const int DefaultValueCountLimit = 1024;
        public const int DefaultKeyLengthLimit = 1024 * 2;
        public const int DefaultValueLengthLimit = 1024 * 1024 * 4;

        // ArrayPool<T>是一个缓存池用来避免存储大数据时占用太高内存，或引发GC回收 
        // Shard属性：获取ArrayPool的一个实例
        // Rent方法：从缓存池中获取一定的数组
        // Return方法：将一个数组返回到缓存池中
        // 详情见：http://adamsitnik.com/Array-Pool/
        private readonly ArrayPool<char> _charPool;
        private const int _rentedCharPoolLength = 8192;
        private readonly TextReader _reader;
        private readonly char[] _buffer;
        private readonly StringBuilder _builder = new StringBuilder();
        private int _bufferOffset;
        private int _bufferCount;
        private string _currentKey;
        private string _currentValue;
        private bool _endOfStream;
        private bool _disposed;

        public FormReader(string data) : this(data, ArrayPool<char>.Shared)
        {
        }

        public FormReader(string data, ArrayPool<char> charPool)
        {
            if (String.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            _charPool = charPool;
            _buffer = charPool.Rent(_rentedCharPoolLength);
            _reader = new StringReader(data);
        }

        public FormReader(Stream stream)
            : this(stream, ArrayPool<char>.Shared, Encoding.UTF8)
        {
        }

        public FormReader(Stream stream, Encoding encoding)
            : this(stream, ArrayPool<char>.Shared, Encoding.UTF8)
        {
        }

        public FormReader(Stream stream, ArrayPool<char> charPool, Encoding encoding)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            _buffer = charPool.Rent(_rentedCharPoolLength);
            _charPool = charPool;
            // detectEncodingFromByteOrderMarks:是否从文件开头查找
            // leaveOpen: 是否在释放treamReader后保持流的打开状态
            _reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks: true, bufferSize: 1024 * 2, leaveOpen: true);
        }

        /// <summary>
        /// 允许从Form中读入的值的最大值，默认为1M
        /// </summary>
        public int ValueCountLimit { get; set; } = DefaultValueCountLimit;

        /// <summary>
        /// Form键的最大长度，默认为2M
        /// </summary>
        public int KeyLengthLimit { get; set; } = DefaultKeyLengthLimit;

        /// <summary>
        /// Form中值的最大长度，默认为4M  
        /// </summary>
        public int ValueLengthLimit { get; set; } = DefaultValueLengthLimit;

        /// <summary>
        /// 从form中读取下一个键值对
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, string>? ReadNextPair()
        {
            ReadNextPairImpl();
            if (ReadSucceded())
            {
                return new KeyValuePair<string, string>(_currentKey, _currentValue);
            }
            return null;
        }

        private void ReadNextPairImpl()
        {
            StartReadNextPair();
            while (!_endOfStream)
            {
                // Empty
                if (_bufferCount == 0)
                {
                    Buffer();
                }
                if (TryReadNextPair())
                {
                    break;
                }
            }
        }

        // Format: key1=value1&key2=value2
        /// <summary>
        /// 从Form中读取key/value
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>如果为null，则Form已经到底</returns>
        public async Task<KeyValuePair<string, string>?> ReadNextPairAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await ReadNextPairAsyncImpl(cancellationToken);
            if (ReadSucceded())
            {
                return new KeyValuePair<string, string>(_currentKey, _currentValue);
            }
            return null;
        }

        private async Task ReadNextPairAsyncImpl(CancellationToken cancellationToken = new CancellationToken())
        {
            StartReadNextPair();
            while (!_endOfStream)
            {
                // Empty
                if (_bufferCount == 0)
                {
                    await BufferAsync(cancellationToken);
                }
                if (TryReadNextPair())
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 初始化当前的key/value
        /// </summary>
        private void StartReadNextPair()
        {
            _currentKey = null;
            _currentValue = null;
        }

        private bool TryReadNextPair()
        {
            if (_currentKey == null)
            {
                if (!TryReadWord('=', KeyLengthLimit, out _currentKey))
                {
                    return false;
                }

                if (_bufferCount == 0)
                {
                    return false;
                }
            }

            if (_currentValue == null)
            {
                if (!TryReadWord('&', ValueLengthLimit, out _currentValue))
                {
                    return false;
                }
            }
            return true;
        }

        private bool TryReadWord(char seperator, int limit, out string value)
        {
            do
            {
                if (ReadChar(seperator, limit, out value))
                {
                    return true;
                }
            } while (_bufferCount > 0);
            return false;
        }

        private bool ReadChar(char seperator, int limit, out string word)
        {
            // 结尾
            if (_bufferCount == 0)
            {
                word = BuildWord();
                return true;
            }

            var c = _buffer[_bufferOffset++];
            _bufferCount--;

            if (c == seperator)
            {
                word = BuildWord();
                return true;
            }
            if (_builder.Length >= limit)
            {
                throw new InvalidDataException($"Form的key或value长度超出{limit}限制");
            }
            _builder.Append(c);
            word = null;
            return false;
        }

        private string BuildWord()
        { 
            _builder.Replace('+', ' '); // HTTP在使用ajax在发送数据时，会发生转义，‘+’在HTTP的URL中表示空格  http://www.cnblogs.com/wanhua-wu/p/6513697.html
            var result = _builder.ToString();
            _builder.Clear();
            return Uri.UnescapeDataString(result); // 将result转换成非转义形式
        }

        /// <summary>
        /// 读取_buffer长度的字符数组
        /// </summary>
        private void Buffer()
        {
            _bufferOffset = 0;
            _bufferCount = _reader.Read(_buffer, 0, _buffer.Length);
            _endOfStream = _bufferCount == 0;
        }

        /// <summary>
        /// 读取_buffer长度的字符数组
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task BufferAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _bufferOffset = 0;
            _bufferCount = await _reader.ReadAsync(_buffer, 0, _buffer.Length);
            _endOfStream = _bufferCount == 0;
        }

        /// <summary>
        /// 从Form中读取数据
        /// </summary>
        /// <returns>包含已分析的 HTTP 窗体主体的集合</returns>
        public Dictionary<string, StringValues> ReadForm()
        {
            var accumulator = new KeyMultValuesPair();
            while (!_endOfStream)
            {
                ReadNextPairImpl();
                Append(ref accumulator);
            }
            return accumulator.GetResults();
        }

        /// <summary>
        /// 从Form中读取数据
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>包含已分析的 HTTP 窗体主体的集合。
        public async Task<Dictionary<string, StringValues>> ReadFormAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var accumulator = new KeyMultValuesPair();
            while (!_endOfStream)
            {
                await ReadNextPairAsyncImpl(cancellationToken);
                Append(ref accumulator);
            }
            return accumulator.GetResults();
        }

        private bool ReadSucceded()
        {
            return _currentKey != null && _currentValue != null;
        }

        private void Append(ref KeyMultValuesPair accumulator)
        {
            if (ReadSucceded())
            {
                accumulator.Append(_currentKey, _currentValue);
                if (accumulator.ValueCount > ValueCountLimit)
                {
                    throw new InvalidDataException($"Form 的value超出限制{ValueCountLimit}。");
                }
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _charPool.Return(_buffer); // 写入缓存池
            }
        }
    }
}
