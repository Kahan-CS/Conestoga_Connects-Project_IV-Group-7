using BlazorApp.Server.Models;
using BlazorApp.Server.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using BlazorApp.Server.Services;
using MongoDB.Driver;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DatabaseInitializer>(); // Change to scoped

// Add services to the container.
try
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDB");
    Console.WriteLine($"MongoDB Connection String: {connectionString}");

    // Add MongoDB configuration
    var mongoSettings = new MongoSettings();
    builder.Configuration.GetSection("MongoDB").Bind(mongoSettings);
    Console.WriteLine($"MongoDB Settings: {mongoSettings}");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new Exception("MongoDB connection string is empty or null.");
    }

    builder.Services.AddSingleton(mongoSettings);
    builder.Services.AddScoped<UserService>();
    builder.Services.AddSingleton<IMongoClient>(sp =>
    {
        try
        {
            MongoClient client = new MongoClient(mongoSettings.ConnectionString);
            Console.WriteLine($"MongoDB Client Created: {client != null}");
            return client;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create MongoDB client: {ex.Message}");
            Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            throw;
        }
    });
    builder.Services.AddScoped(sp =>
    {
        var client = sp.GetRequiredService<IMongoClient>();
        var database = client.GetDatabase(mongoSettings.DatabaseName);
        Console.WriteLine($"MongoDB Database Selected: {database.DatabaseNamespace.DatabaseName}");
        return database;
    });

    
    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred during service configuration: {ex.Message}");
    // Handle the exception gracefully, e.g., log it and exit the application
    Environment.Exit(1);
}

var app = builder.Build();

// Initialize database collections
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var initializer = services.GetRequiredService<DatabaseInitializer>();
        initializer.InitializeCollections();
        Console.WriteLine("Database collections initialized successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to initialize database collections: {ex.Message}");
        Environment.Exit(1);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

// Log application start
Console.WriteLine("Application started.");

app.Run();
