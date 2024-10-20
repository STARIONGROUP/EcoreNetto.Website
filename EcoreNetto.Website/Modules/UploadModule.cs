// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="UploadModule.cs" company="Starion Group S.A.">
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

namespace EcoreNetto.Website.Modules
{
    using System.Text.Json;

    using Carter;

    using EcoreNetto.Website.Services;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The purpose of the <see cref="UploadModule"/> is to support ecore model upload
    /// </summary>
    public class UploadModule : CarterModule
    {
        /// <summary>
        /// injected logger
        /// </summary>
        private readonly ILogger<UploadModule> logger;

        /// <summary>
        /// The injected <see cref="IFileTrackingService"/>
        /// </summary>
        private readonly IFileTrackingService fileTrackingService;

        /// <summary>
        /// The Upload module used to upload files
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="fileTrackingService"></param>
        public UploadModule(ILoggerFactory loggerFactory, IFileTrackingService fileTrackingService)
        {
            this.logger = loggerFactory.CreateLogger<UploadModule>();
            this.fileTrackingService = fileTrackingService;
        }

        /// <summary>
        /// Add the routes to the <see cref="IEndpointRouteBuilder"/>
        /// </summary>
        /// <param name="app">
        /// The <see cref="IEndpointRouteBuilder"/> to which the routes are added
        /// </param>
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/upload", async (HttpRequest req, HttpResponse res) =>
            {
                try
                {
                    var form = await req.ReadFormAsync();
                    var file = form.Files["file"];

                    this.logger.LogInformation("file with name {filename} received", file.FileName); 

                    var fileUpload = await this.fileTrackingService.SaveFileAsync(file);

                    this.logger.LogInformation("file with name {filename} stored with key {key} and accessible until {expirationDate}", 
                        file.FileName, fileUpload.Key, fileUpload.ExpirationDateTime);

                    res.StatusCode = 200;

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    await res.WriteAsJsonAsync(fileUpload, options);
                }
                catch (Exception ex)
                {
                    res.StatusCode = 500;
                    await res.WriteAsync(ex.Message);
                }
            });
        }
    }
}
