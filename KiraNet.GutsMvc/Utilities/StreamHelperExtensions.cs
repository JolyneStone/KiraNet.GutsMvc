using System.Buffers;
using System.IO;

namespace KiraNet.GutsMVC
{
    public static class StreamHelperExtensions
    {
        private const int _maxReadBufferSize = 1024 * 4;

        public static void Drain(this Stream stream, long? limit = null)
        {
            stream.Drain(ArrayPool<byte>.Shared, limit);
        }

        public static void Drain(this Stream stream, ArrayPool<byte> bytePool, long? limit)
        {
            var buffer = bytePool.Rent(_maxReadBufferSize);
            long total = 0;
            try
            {
                var read = stream.Read(buffer, 0, buffer.Length);
                while (read > 0)
                {
                    if (limit.HasValue && limit.Value - total < read)
                    {
                        throw new InvalidDataException($"The stream exceeded the data limit {limit.Value}.");
                    }
                    total += read;
                    read = stream.Read(buffer, 0, buffer.Length);
                }
            }
            finally
            {
                bytePool.Return(buffer);
            }
        }
    }
}
