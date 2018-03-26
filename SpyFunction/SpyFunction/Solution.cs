using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace SpyFunction
{
    public class Solution : TableEntity
    {
        public int N { get; set; }
        public string Generator { get; set; }
        public string Validator { get; set; }

        public string BoardSolution { get; set; } = "";
        public DateTime TimeRequested { get; set; }
        public Nullable<DateTime> TimeStarted { get; set; }
        public Nullable<DateTime> TimeSolved { get; set; }

        public Solution() { }

        public Solution(int n, string generator, string validator)
        {
            this.PartitionKey  = "boardSolutions";
            this.RowKey        = n.ToString();

            this.N             = n;
            this.Generator     = generator;
            this.Validator     = validator;
            this.TimeRequested = DateTime.Now;
        }
    }
}