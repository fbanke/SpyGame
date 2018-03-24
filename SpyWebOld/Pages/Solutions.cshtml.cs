using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SpyWeb.Pages
{
    public class SolutionsModel : PageModel
    {
        public List<Solution> Solutions { get; private set; } = new List<Solution>();

        public void OnGet()
        {
            var messages = Program.SpyQueue.PeekMessagesAsync(10).GetAwaiter().GetResult();

            foreach (var message in messages)
            {
                var solution = new Solution();
                solution.n = 11;
                solution.solution = message.AsString+" From QUEUE";
                Solutions.Add(solution);
            }

            foreach( var sol in Program.GetAllSolutions())
            {
                Solutions.Add(sol);
            }
        }
    }
}