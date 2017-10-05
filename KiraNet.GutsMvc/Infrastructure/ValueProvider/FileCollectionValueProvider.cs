using System.Collections.Generic;
using System.Globalization;

namespace KiraNet.GutsMVC.Infrastructure
{
    public sealed class FileCollectionValueProvider : ValueProvider<DictionaryValueProviderResultWrapper<IFormFile>>
    {
        public FileCollectionValueProvider(IFormFileCollection files) : this(files, CultureInfo.CurrentCulture)
        {
        }

        public FileCollectionValueProvider(IFormFileCollection files, CultureInfo culture) : base(culture)
        {
            if (files == null)
            {
                _values.Clear();
            }

            var fileDictionary = new Dictionary<string, IFormFile>();
            foreach (var file in files)
            {
                fileDictionary.Add(file.FileName + file.GetHashCode(), file);
            }

            foreach (var file in fileDictionary)
            {
                _values[file.Key] = new DictionaryValueProviderResultWrapper<IFormFile>(file.Key, fileDictionary, culture);
            }
        }
    }
}
