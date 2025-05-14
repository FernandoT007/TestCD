using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SedesFunction.Persistence;
using SedesFunction.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddDbContext<SedeDbContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string not found in environment variables.");
    }
    options.UseSqlServer(connectionString);
});

builder.Services.AddSingleton( x =>
{
    var blobConnectionString = Environment.GetEnvironmentVariable("BlobStorageConnectionString");
     if (string.IsNullOrEmpty(blobConnectionString))
    {
        throw new InvalidOperationException("BlobStorageConnectionString string not found in environment variables.");
    }
    return new BlobServiceClient(blobConnectionString);
});

builder.Services.AddScoped<BlobStorageService>();
builder.Services.AddScoped<SedeService>();
   
builder.Build().Run();
