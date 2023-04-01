using Recipe_Book.Models;
using Recipe_Book.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RecipeBookDatabaseSettings>(
    builder.Configuration.GetSection("RecipeBookDatabase"));

// Add RecipeService to DI for contructor injection for the consuming classes - https://mongodb.github.io/mongo-csharp-driver/2.14/reference/driver/connecting/#re-use
builder.Services.AddSingleton<RecipeService>();

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();