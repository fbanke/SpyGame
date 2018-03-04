using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SpyWeb.Pages
{
    public class SolutionsModel : PageModel
    {
        public IEnumerable<Solution> Solutions { get; private set; }

        public void OnGetAsync() 
        {
            Solutions = new List<Solution>();
            var solution = new Solution();
            solution.n = 11;
            solution.solution = "1 2 3 4 5";
            Solutions.Append(solution);
        }
        /*
        public IEnumerable<CloudQueueMessage> Messages { get; private set; }
        public string Message { get; private set; } = "Message from backend";

        public async Task OnGetAsync() // initializer, only runs once
        {
            Messages = await Startup.SpyQueue.PeekMessagesAsync(10);
        }
        */
    }
}