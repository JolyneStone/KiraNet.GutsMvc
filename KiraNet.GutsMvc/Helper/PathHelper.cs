using System;

namespace KiraNet.GutsMVC.Helper
{
    internal class PathHelper
    {
        public static string RelativeToAbsolutePath(string absolutePath, string relativePath)
        {
            if (string.IsNullOrWhiteSpace(absolutePath))
            {
                throw new ArgumentException("message", nameof(absolutePath));
            }

            if (string.IsNullOrWhiteSpace(relativePath))
            {
                throw new ArgumentException("message", nameof(relativePath));
            }

            var absoluteUri = new Uri(absolutePath);
            Uri.TryCreate(absoluteUri, relativePath, out var result);
            return result.AbsolutePath;
        }
    }
}
