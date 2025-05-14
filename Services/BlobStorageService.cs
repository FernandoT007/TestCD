using Azure.Storage.Blobs;

namespace SedesFunction.Services;

public class BlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadImageAsync(string ImageBase64, string blobName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("sedes");
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient($"{blobName}.jpg");
        byte[] imageBytes = Convert.FromBase64String(ImageBase64);
        
        using (var stream = new MemoryStream(imageBytes))
        {
            await blobClient.UploadAsync(stream, overwrite : true);
        }
        
        return blobClient.Uri.ToString();
    }
}