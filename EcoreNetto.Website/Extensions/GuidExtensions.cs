// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GuidExtensions.cs" company="Starion Group S.A.">
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

namespace EcoreNetto.Website.Extensions
{
    using System;
    
    /// <summary>
    /// Extension class for <see cref="Guid"/>
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// converts a <see cref="Guid" /> to its base64 encoded short form
        /// </summary>
        /// <param name="guid">
        /// an instance of <see cref="Guid" />
        /// </param>
        /// <returns>
        /// a shortGuid representation of the provided <see cref="Guid" />
        /// </returns>
        /// <remarks>
        /// A ShortGuid is a base64 encoded guid-string representation where any "/" has been replaced with a "_"
        /// and any "+" has been replaced with a "-" (to make the string representation <see cref="Uri" /> friendly)
        /// </remarks>
        public static string ToShortGuid(this Guid guid)
        {
            var enc = Convert.ToBase64String(guid.ToByteArray());
            return enc.Replace("/", "_").Replace("+", "-").Substring(0, 22);
        }

        /// <summary>
        /// Creates a <see cref="Guid" /> based the ShortGuid representation
        /// </summary>
        /// <param name="shortGuid">
        /// a shortGuid string
        /// </param>
        /// <returns>
        /// an instance of <see cref="Guid" />
        /// </returns>
        /// <remarks>
        /// A ShortGuid is a base64 encoded guid-string representation where any "/" has been replaced with a "_"
        /// and any "+" has been replaced with a "-" (to make the string representation <see cref="Uri" /> friendly)
        /// </remarks>
        public static Guid FromShortGuid(this string shortGuid)
        {
            var buffer = Convert.FromBase64String(shortGuid.Replace("_", "/").Replace("-", "+") + "==");
            return new Guid(buffer);
        }
    }
}
