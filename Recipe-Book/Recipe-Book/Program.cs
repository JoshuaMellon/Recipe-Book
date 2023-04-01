using Recipe_Book.Models;
using Recipe_Book.Services;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recipe_Book.Support;
using System.Net;

using Microsoft.IdentityModel.Logging;


var builder = WebApplication.CreateBuilder(args);

//To use MVC we have to explicitly declare we are using it. Doing so will prevent a System.InvalidOperationException.
builder.Services.AddControllersWithViews();

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

// Add services to the container.
builder.Services.Configure<RecipeBookDatabaseSettings>(
    builder.Configuration.GetSection("RecipeBookDatabase"));

// Add RecipeService to DI for contructor injection for the consuming classes - https://mongodb.github.io/mongo-csharp-driver/2.14/reference/driver/connecting/#re-use
builder.Services.AddSingleton<RecipeService>();

// Add services to the container.


builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the HTTP request pipeline.
builder.Services.ConfigureSameSiteNoneCookies();
var app = builder.Build();

app.UseCookiePolicy();

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();