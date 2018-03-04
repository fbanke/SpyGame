using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SpyWeb
{
    public class QueueController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
        
        public ActionResult CreateQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                Program.GetEnvironmentVariable("STORAGE_CONNECTION"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            
            CloudQueue queue = queueClient.GetQueueReference("spy-queue");

            ViewBag.Success = queue.CreateIfNotExistsAsync();

            ViewBag.QueueName = queue.Name;
            
            return View();
        }
    }
}

//CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
//GetEnvironmentVariable("STORAGE_CONNECTION"));
//            
//CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
//
//// Retrieve a reference to a container.
//CloudQueue queue = queueClient.GetQueueReference("spyQueue");
//
//// Create the queue if it doesn't already exist
//queue.CreateIfNotExistsAsync();