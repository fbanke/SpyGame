using Microsoft.WindowsAzure.Storage.Table;

namespace SpyWeb
{
    public class Solution : TableEntity
    {
        public int n { get; set; }
        public string solution { get; set; }

        public Solution() { }

        public Solution(int n, string solution)
        {
            this.PartitionKey = n.ToString();
            this.RowKey = n.ToString();
            this.n = n;
            this.solution = solution;
        }
    }
}