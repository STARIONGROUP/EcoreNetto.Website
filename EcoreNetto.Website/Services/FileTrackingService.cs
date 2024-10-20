// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FileTrackingService.cs" company="Starion Group S.A.">
// 
//    Copyright 2024 Starion Group S.A.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//         http://www.apache.org/licenses/LICENSE-2.0
// 
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace EcoreNetto.Website.Services
{
    using EcoreNetto.Website.Extensions;
    using EcoreNetto.Website.Model;

    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// The purpose of the <see cref="FileTrackingService"/> is to store files on disk, keep track of them and delete
    /// them after a caching period
    /// </summary>
    public class FileTrackingService : IFileTrackingService
    {
        /// <summary>
        /// injected logger
        /// </summary>
        private readonly ILogger<FileTrackingService> logger;

        /// <summary>
        /// The injected <see cref="IMemoryCache"/>
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The temporary path where the uploaded ecore models are stored
        /// </summary>
        private readonly string tempPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTrackingService"/> class
        /// </summary>
        /// <param name="cache">
        /// The injected <see cref="IMemoryCache"/> used to keep track of the uploaded files
        /// </param>
        /// <param name="loggerFactory">
        /// The injected logger
        /// </param>
        public FileTrackingService(IMemoryCache cache, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<FileTrackingService>();

            this.memoryCache = cache;

            this.tempPath = Path.GetTempPath(); 
        }

        /// <summary>
        /// Saves a file to disk and registers the file with the service for access and automated cleanup
        /// </summary>
        /// <param name="file">
        /// The <see cref="IFormFile"/> that is to be saved
        /// </param>
        /// <returns>
        /// The unique key that represents the file
        /// </returns>
        public async Task<FileUpload> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty.");
            }

            var key = Guid.NewGuid().ToShortGuid();

            var tempFileName = Path.Combine(this.tempPath, Path.GetRandomFileName());

            await using (var stream = new FileStream(tempFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Add the file path to the cache with a 15-minute expiration
            var cacheEntryOptions = new MemoryCacheEntryOptions
            { 
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15), // Expire in 15 minutes
                Priority = CacheItemPriority.Normal
            }.RegisterPostEvictionCallback((evictionKey, value, reason, state) =>
            {
                // Callback to delete the file when cache expires
                if (value is string filePath && File.Exists(filePath))
                {
                    File.Delete(filePath); // Delete the file

                    this.logger.LogInformation("file at {filePath} automatically deleted", filePath);
                }
            });

            var fileUpload = new FileUpload
            {
                Key = key,
                Name = file.Name,
                TemporaryFileName = tempFileName,
                ExpirationDateTime = DateTime.Now + cacheEntryOptions.AbsoluteExpirationRelativeToNow.Value
            };

            this.memoryCache.Set(key, fileUpload, cacheEntryOptions);

            return fileUpload;
        }

        /// <summary>
        /// Gets the <see cref="FileInfo"/> that matches the key
        /// </summary>
        /// <param name="key">
        /// the key that represents the file
        /// </param>
        /// <returns>
        /// the requested <see cref="FileInfo"/>
        /// </returns>
        public FileInfo GetFile(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (this.memoryCache.TryGetValue(key, out FileUpload? fileUpload))
            {
                if (fileUpload != null && File.Exists(fileUpload.TemporaryFileName))
                {
                    return new FileInfo(fileUpload.TemporaryFileName);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the <see cref="FileUpload"/> that matches the key
        /// </summary>
        /// <param name="key">
        /// the key that represents the <see cref="FileUpload"/>
        /// </param>
        /// <returns>
        /// the requested <see cref="FileUpload"/>
        /// </returns>
        public FileUpload GetFileUpload(string key)
        {
            if (this.memoryCache.TryGetValue(key, out FileUpload? fileUpload))
            {
                return fileUpload;
            }

            return null;
        }
    }
}