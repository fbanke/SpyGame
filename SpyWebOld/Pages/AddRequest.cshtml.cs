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
            string[] lines = { Request.Form["validator"], Request.Form["generator"] };
            System.IO.File.WriteAllLines(n+".txt", lines);

            /*string message = Request.Form["queue-message"];
            
            var msg = new CloudQueueMessage(message);
            await Startup.SpyQueue.AddMessageAsync(msg);*/

            return RedirectToPage("/Solutions");
        }
    }
}