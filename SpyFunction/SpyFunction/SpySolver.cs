using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace SpyFunction
{
    public static class SpySolver
    {
        [FunctionName("SpySolver")]
        public static void Run([QueueTrigger("spy-queue", Connection = "")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
