﻿using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// Represents a form multipart section
    /// </summary>
    public class FormMultipartSection
    {
        private ContentDispositionHeaderValue _contentDispositionHeader;

        /// <summary>
        /// Creates a new instance of the <see cref="FormMultipartSection"/> class
        /// </summary>
        /// <param name="section">The section from which to create the <see cref="FormMultipartSection"/></param>
        /// <remarks>Reparses the content disposition header</remarks>
        public FormMultipartSection(MultipartSection section)
            : this(section, section.GetContentDispositionHeader())
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FormMultipartSection"/> class
        /// </summary>
        /// <param name="section">The section from which to create the <see cref="FormMultipartSection"/></param>
        /// <param name="header">An already parsed content disposition header</param>
        public FormMultipartSection(MultipartSection section, ContentDispositionHeaderValue header)
        {
            if (header == null || !header.IsFormDisposition())
            {
                throw new ArgumentException($"Argument must be a form section", nameof(section));
            }

            Section = section;
            _contentDispositionHeader = header;
            Name = HeaderUtilities.RemoveQuotes(_contentDispositionHeader.Name).ToString();
        }

        /// <summary>
        /// Gets the original section from which this object was created
        /// </summary>
        public MultipartSection Section { get; }

        /// <summary>
        /// The form name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the form value
        /// </summary>
        /// <returns>The form value</returns>
        public Task<string> GetValueAsync() => Section.ReadAsStringAsync();

        public string GetValue() => Section.ReadAsString();
    }
}