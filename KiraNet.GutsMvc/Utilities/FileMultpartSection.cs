﻿using Microsoft.Net.Http.Headers;
using System;
using System.IO;

namespace KiraNet.GutsMvc
{
    public class FileMultipartSection
    {
        private ContentDispositionHeaderValue _contentDispositionHeader;

        /// <summary>
        /// Creates a new instance of the <see cref="FileMultipartSection"/> class
        /// </summary>
        /// <param name="section">The section from which to create the <see cref="FileMultipartSection"/></param>
        /// <remarks>Reparses the content disposition header</remarks>
        public FileMultipartSection(MultipartSection section)
            : this(section, section.GetContentDispositionHeader())
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FileMultipartSection"/> class
        /// </summary>
        /// <param name="section">The section from which to create the <see cref="FileMultipartSection"/></param>
        /// <param name="header">An already parsed content disposition header</param>
        public FileMultipartSection(MultipartSection section, ContentDispositionHeaderValue header)
        {
            if (!header.IsFileDisposition())
            {
                throw new ArgumentException($"Argument must be a file section", nameof(section));
            }

            Section = section;
            _contentDispositionHeader = header;

            Name = HeaderUtilities.RemoveQuotes(_contentDispositionHeader.Name).ToString();
            FileName = HeaderUtilities.RemoveQuotes(
                    _contentDispositionHeader.FileNameStar.HasValue ?
                        _contentDispositionHeader.FileNameStar :
                        _contentDispositionHeader.FileName).ToString();
        }

        /// <summary>
        /// Gets the original section from which this object was created
        /// </summary>
        public MultipartSection Section { get; }

        /// <summary>
        /// Gets the file stream from the section body
        /// </summary>
        public Stream FileStream => Section.Body;

        /// <summary>
        /// Gets the name of the section
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the name of the file from the section
        /// </summary>
        public string FileName { get; }

    }
}
