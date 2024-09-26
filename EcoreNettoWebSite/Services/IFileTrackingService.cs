// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IFileTrackingService.cs" company="Starion Group S.A.">
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

namespace EcoreNettoWebsite.Services
{
    using EcoreNettoWebsite.Model;

    /// <summary>
    /// The purpose of the <see cref="IFileTrackingService"/> is to store files on disk, keep track of them and delete
    /// them after a caching period
    /// </summary>
    public interface IFileTrackingService
    {
        /// <summary>
        /// Saves a file to disk and registers 
        /// </summary>
        /// <param name="file">
        /// The <see cref="IFormFile"/> that is to be saved
        /// </param>
        /// <returns>
        /// The unique key that represents the file
        /// </returns>
        Task<FileUpload> SaveFileAsync(IFormFile file);

        /// <summary>
        /// Gets the <see cref="FileInfo"/> that matches the key
        /// </summary>
        /// <param name="key">
        /// the key that represents the file
        /// </param>
        /// <returns>
        /// the requested <see cref="FileInfo"/>
        /// </returns>
        FileInfo GetFile(string key);

        /// <summary>
        /// Gets the <see cref="FileUpload"/> that matches the key
        /// </summary>
        /// <param name="key">
        /// the key that represents the <see cref="FileUpload"/>
        /// </param>
        /// <returns>
        /// the requested <see cref="FileUpload"/>
        /// </returns>
        FileUpload GetFileUpload(string key);
    }
}