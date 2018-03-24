using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SpyWeb.Pages
{
    public class AddRequestModel : PageModel
    {
        public async Task<IActionResult> OnPost()
        {
            var n = Int32.Parse(Request.Form["n"]);
            string message = n.ToString();
            
            var msg = new CloudQueueMessage(message);
            await Program.SpyQueue.AddMessageAsync(msg);

            var messages = await Program.SpyQueue.PeekMessagesAsync(10);
            foreach(var nn in messages)
            {
                Console.WriteLine(nn.AsString);
            }


            return RedirectToPage("/Solutions");
        }
    }
}