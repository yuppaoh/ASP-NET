using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Reference to Azure Storage Account
            String strorageconn = System.Configuration.ConfigurationSettings.AppSettings.Get("StorageConnectionString"); // trong App.config co key="StorageConnectionString"
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer container = blobClient.GetContainerReference("demo2");  //democontainer la phan vung cho moi shop (vidu)
            container.CreateIfNotExists();

            //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("DemoBlob");
            //using (var filestream = System.IO.File.OpenRead(@"E:\up\azureText.txt")) // file de up len
            using (var filestream = System.IO.File.OpenRead(@"E:\up\iphone4s.jpg"))
            {
                blockBlob.UploadFromStream(filestream);

            }



        }
    }
}
