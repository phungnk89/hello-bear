using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace HelloBear.Support
{
    public class AzureBlobHelper
    {
        private static string connectionString = string.Empty;
        private static string containerName = string.Empty;

        public AzureBlobHelper()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = configuration["azureblob:connectionstring"];
            containerName = configuration["azureblob:container"];
        }

        /// <summary>
        /// UploadImageToAzure
        /// </summary>
        /// <param name="path"></param>
        public void UploadImageToAzure(string path)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobName = path.Split('\\');
            BlobClient blobClient = containerClient.GetBlobClient(blobName[blobName.Length - 1].Replace(" ", ""));

            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    blobClient.Upload(fs, true);
                }

                Console.WriteLine("Image uploaded successfully!");
            }
            catch
            {
                Console.WriteLine("Image upload unsuccessfully!");
            }
        }

        /// <summary>
        /// GetBlobgUrl
        /// </summary>
        /// <param name="context"></param>
        /// <returns>BlobURL</returns>
        public string GetBlobgUrl(ScenarioContext context)
        {
            string blobName = context.ScenarioInfo.Title.Replace(" ","") + ".png";

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            return string.Format("{0}{1}/{2}", blobServiceClient.Uri.ToString(), containerName, blobName);
        }
    }
}
