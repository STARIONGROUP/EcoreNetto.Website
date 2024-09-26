// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FileUpload.cs" company="Starion Group S.A.">
// 
//     Copyright (c) 2024 Starion Group S.A.
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

using DocumentFormat.OpenXml.Office.CoverPageProps;

namespace EcoreNettoWebsite.Model
{
    /// <summary>
    /// Represents the DTO to capture information regarding an uploaded file
    /// </summary>
    public class FileUpload
    {
        /// <summary>
        /// Gets or sets the Key of an uploaded file
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the filename as provided by the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the temporary filename as it is stored on the server
        /// </summary>
        public string TemporaryFileName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> on which the file is expired and removed from
        /// the server
        /// </summary>
        public DateTime ExpirationDateTime { get; set; }
    }
}