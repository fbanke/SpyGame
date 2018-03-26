using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace SpyWeb.Pages
{
    public class AddRequestModel : PageModel
    {
        public IEnumerable<Type> Validators;
        public IEnumerable<Type> Generators;

        public void OnGet()
        {
            var boardValidator = typeof(SpyLib.IBoardValidator);
            Validators = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => boardValidator.IsAssignableFrom(p) && ! p.IsInterface);

            var boardGenerator = typeof(SpyLib.IBoardGenerator);
            Generators = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => boardGenerator.IsAssignableFrom(p) && !p.IsInterface);
        }
        public async Task<IActionResult> OnPost()
        {
            var n = Int32.Parse(Request.Form["N"]);
            var generator = Request.Form["Generator"];
            var validator = Request.Form["Validator"];

            var solution = new Solution(n, generator, validator);

            await Program.InsertOrMergeEntityAsync(solution);

            string jsonString = JsonConvert.SerializeObject(solution);
            var msg = new CloudQueueMessage(jsonString);
            await Program.SpyQueue.AddMessageAsync(msg);

            return RedirectToPage("/Solutions");
        }
    }
}