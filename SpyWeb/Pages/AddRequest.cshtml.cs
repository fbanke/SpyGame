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
            string message = Request.Form["queue-message"];
            
            var msg = new CloudQueueMessage(message);
            await Startup.SpyQueue.AddMessageAsync(msg);

            return RedirectToPage("/Solutions");
        }
    }
}