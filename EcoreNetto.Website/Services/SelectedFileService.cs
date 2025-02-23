// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SelectedFileService.cs" company="Starion Group S.A.">
// 
//     Copyright (c) 2024-2025 Starion Group S.A.
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
    /// <summary>
    /// The purpose of the <see cref="SelectedFileService"/> is to keep track
    /// of the reqif file that is selected when navigating from page to page
    /// </summary>
    public class SelectedFileService : ISelectedFileService
    {
        /// <summary>
        /// The key or unique identifier of the file
        /// </summary>
        public string Key { get; set; }
    }
}