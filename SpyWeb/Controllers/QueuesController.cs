using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;

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
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("csb4b84ae5bd88ex4df5xbeb_AzureStorageConnectionString"));
            
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            
            CloudQueue queue = queueClient.GetQueueReference("test-queue");
            ViewBag.Success = queue.CreateIfNotExistsAsync();
            ViewBag.QueueName = queue.Name;

            return View();
        }
    }
}