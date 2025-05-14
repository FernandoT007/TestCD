using SedesFunction.Models;
using SedesFunction.Persistence;

namespace SedesFunction.Services;

public class SedeService
{
    private readonly SedeDbContext _dbContext;
    private readonly BlobStorageService _blobStorageService;

    public SedeService(SedeDbContext dbContext, BlobStorageService blobStorageService)
    {
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
    }

    public async Task<Sede> CreateSedeAsync(SedeRequest request)
    {
        var ImageUrl = await _blobStorageService.UploadImageAsync(request.ImageBase64, request.Name);

        var sede = new Sede
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            ImageUrl = ImageUrl
        };

        _dbContext.Sedes.Add(sede);
        await _dbContext.SaveChangesAsync();

        return sede;
    }

    public async Task<Sede?> GetSedeByIdAsync(Guid id)
    {
        return await _dbContext.Sedes.FindAsync(id);
    }
}