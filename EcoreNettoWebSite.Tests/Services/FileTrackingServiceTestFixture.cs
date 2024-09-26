// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FileTrackingServiceTestFixture.cs" company="Starion Group S.A.">
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

namespace EcoreNettoWebsite.Tests.Services
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using EcoreNettoWebsite.Services;

    using Microsoft.AspNetCore.Http;

    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class FileTrackingServiceTestFixture
    {
        private IMemoryCache memoryCache;

        private Mock<ILoggerFactory> loggerFactory;

        private FileTrackingService fileTrackingService;

        private Mock<IFormFile> mockFile;

        [SetUp]
        public void SetUp()
        {
            this.memoryCache = new MemoryCache(new MemoryCacheOptions());

            this.loggerFactory = new Mock<ILoggerFactory>();

            this.mockFile = new Mock<IFormFile>();
            
            this.fileTrackingService = new FileTrackingService(this.memoryCache, this.loggerFactory.Object);
        }

        [Test]
        public void Verify_that_when_fileinfo_is_null_or_empty_an_exception_is_thrown()
        {
            IFormFile file = null;

            Assert.That( async () => await this.fileTrackingService.SaveFileAsync(file), Throws.TypeOf<ArgumentException>());

            this.mockFile.Setup(x => x.Length).Returns(0);

            Assert.That(async () => await this.fileTrackingService.SaveFileAsync(this.mockFile.Object), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task Verify_that_PayloadResult_is_returned()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "recipe.ecore");
            
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var mockFormFile = new Mock<IFormFile>();

            // Setup the mock to return a specific file name, content type, and file stream
            mockFormFile.Setup(_ => _.FileName).Returns(Path.GetFileName(filePath));
            mockFormFile.Setup(_ => _.Length).Returns(fileStream.Length);
            mockFormFile.Setup(_ => _.OpenReadStream()).Returns(fileStream);
            mockFormFile.Setup(_ => _.ContentType).Returns("application/octet-stream");
            mockFormFile.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns(async (Stream target, CancellationToken token) =>
                {
                    await fileStream.CopyToAsync(target, token);
                });

            var result = await this.fileTrackingService.SaveFileAsync(mockFormFile.Object);

            Assert.That(result, Is.Not.Null);
        }
    }
}