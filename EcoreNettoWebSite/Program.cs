// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Program.cs" company="Starion Group S.A.">
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

using Carter;
using ECoreNetto.Reporting.Generators;
using EcoreNettoWebsite.Components;
using EcoreNettoWebsite.Services;
using Radzen;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMemoryCache();

builder.Services.AddRadzenComponents();

builder.Services.AddCarter();

builder.Services.AddSingleton<IFileTrackingService, FileTrackingService>();
builder.Services.AddSingleton<IMarkdownReportGenerator, MarkdownReportGenerator>();
builder.Services.AddSingleton<IHtmlReportGenerator, HtmlReportGenerator>();
builder.Services.AddScoped<ISelectedFileService, SelectedFileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapCarter();

app.Run();
