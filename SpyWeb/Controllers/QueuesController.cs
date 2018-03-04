using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using Microsoft.IdentityModel.Protocols;

namespace SpyWeb.Controllers
{
    public class QueuesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
        
        public ActionResult CreateQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=csb4b84ae5bd88ex4df5xbeb;AccountKey=ERExFyCG5iTB+B2N2DtpEi0tjiTLhwgGaXKr/4xkiyXm9cNPppf8Bc3H8fGGUWOpRoU/aTfCY9e/8Zjf3rJEKA==;EndpointSuffix=core.windows.net");
                //CloudConfigurationManager.GetSetting("AzureStorageConnectionString"));
            
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            
            CloudQueue queue = queueClient.GetQueueReference("test-queue");
            ViewBag.Success = queue.CreateIfNotExistsAsync();
            ViewBag.QueueName = queue.Name;

            return View("CreateQueue");
        }
    }
}