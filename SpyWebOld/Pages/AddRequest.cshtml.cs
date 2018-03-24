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
            
            var msg = new CloudQueueMessage("{message:'"+message+"'}");
            await Program.SpyQueue.AddMessageAsync(msg);

            var solution = new Solution(9, "abv");

            await Program.InsertOrMergeEntityAsync(solution);


            return RedirectToPage("/Solutions");
        }
    }
}