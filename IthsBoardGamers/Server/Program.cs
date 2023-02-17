using IthsBoardGamers.DataAccess;
using IthsBoardGamers.DataAccess.ChatDB;
using IthsBoardGamers.DataAccess.Services;
using IthsBoardGamers.Server.Extensions;
using IthsBoardGamers.Server.Hubs;
using IthsBoardGamers.Shared.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddRazorPages();

builder.Services.AddOptions<DatabaseOptions>().Bind(builder.Configuration.GetSection(DatabaseOptions.SectionName));
builder.Services.AddScoped<IRepository<ChatMessageDto>, ChatRepository>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapEndpoints();

app.MapHub<ChatHub>("/hubs/chatHub").AllowAnonymous();

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.Run();
