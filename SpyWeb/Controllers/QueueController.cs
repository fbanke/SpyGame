using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SpyWeb.Controllers
{
    public class QueueController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult CreateQueue()
        {
//            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
//                Program.GetEnvironmentVariable("STORAGE_CONNECTION"));
//            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
//            
//            CloudQueue queue = queueClient.GetQueueReference("spy-queue");
//
//            ViewBag.Success = queue.CreateIfNotExistsAsync();
//
//            ViewBag.QueueName = queue.Name;
            
            return View();
        }
    }
}
