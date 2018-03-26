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
            Solutions = Program.GetAllSolutions();
        }
    }
}