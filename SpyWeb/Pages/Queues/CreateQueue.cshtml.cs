using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SpyWeb.Pages.Queues
{
    public class CreateQueue : PageModel
    {
        public string Message { get; private set; } = "PageModel in C#";

        public async void OnGet()
        {
            
            CloudQueueMessage message = await Startup.SpyQueue.PeekMessageAsync();
            
            Message = message.AsString;
        }

        public async void OnPost()
        {
            string message = Request.Form["queue-message"];
            Message = message;
            
            CloudQueueMessage msg = new CloudQueueMessage("Hello, Azure Queue Storage");
            await Startup.SpyQueue.AddMessageAsync(msg);
        }
    }
}