using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.EF
{
    public partial class product
    {
        public String StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=hqdata;AccountKey=CgxaGhJQgLLaqNNghR8nPAdwaNHRjjS2465f8p2UnnB3h1cNHaNa3/BXYU+PSyIseJMtyOQ7KdS1Q5SeYrW/xA==;EndpointSuffix=core.windows.net";
        public string GetImageUrlFromAzureStorage()
        {
            string blobUrl = "";

            // Update file lên thư mục Storage Azure
            // Create Reference to Azure Storage Account
            String strorageconn = this.StorageConnectionString;
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer container = blobClient.GetContainerReference("uploadfile");

            //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(this.image); // hoahong.jpg
            blobUrl = blockBlob.Uri.AbsoluteUri; //https://azure.storage.com/kellyfire/democontainer/hoahong.jpg
            return blobUrl;
        }

    }
}