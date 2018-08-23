using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveUploadedFileToAzure
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] contents = ReadFileContentsInByteArray();
            SaveUploadedFileToAzure(contents);
        }

        public static byte[] ReadFileContentsInByteArray()
        {
            byte[] bytes = System.IO.File.ReadAllBytes("C:/Users/tbhatt/Downloads/VSDFSampleApp.zip");
            return bytes;
        }

        public static void SaveUploadedFileToAzure(byte[] bytes)
        {
            try
            {
                CloudStorageAccount storageAccount = null;
                CloudBlobContainer cloudBlobContainer = null;
                string storageConnectionString = "UseDevelopmentStorage=true";
                if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
                {
                     // Get a reference to the blob address, then upload the file to the blob.
                    // Use the value of localFileName for the blob name.
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    cloudBlobContainer = cloudBlobClient.GetContainerReference("quickstartblobs" + Guid.NewGuid().ToString());
                    cloudBlobContainer.Create();
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference("VSDFSampleApp.zip");
                    cloudBlockBlob.UploadFromByteArray(bytes,0,bytes.Length);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}