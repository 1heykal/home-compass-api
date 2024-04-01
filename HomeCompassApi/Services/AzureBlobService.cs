using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace HomeCompassApi.Services
{
    public class AzureBlobService
    {
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        string azureConnectionString;
        public AzureBlobService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(azureConnectionString);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("homecompass");
            azureConnectionString = configuration.GetConnectionString("AzureBlobStorage");

        }

        public async Task<List<BlobContentInfo>> UploadFiles(List<IFormFile> files)
        {
            var azureResponse = new List<BlobContentInfo>();

            foreach (var file in files)
            {
                string fileName = file.FileName;
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(fileName, memoryStream, default);

                    azureResponse.Add(client);

                }
            }

            return azureResponse;
        }

        public async Task<List<BlobItem>> GetUploadedBlobs()
        {
            var items = new List<BlobItem>();
            var uploadedFiles = _blobContainerClient.GetBlobsAsync();
            await foreach (BlobItem file in uploadedFiles)
            {
                items.Add(file);
            }

            return items;
        }
    }
}
