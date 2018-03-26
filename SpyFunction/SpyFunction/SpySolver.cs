using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace SpyFunction
{
    public static class SpySolver
    {
        [FunctionName("SpySolver")]
        public static void Run([QueueTrigger("spy-queue", Connection = "")]string spySolutionRequest, TraceWriter log)
        {
            Solution solution = JsonConvert.DeserializeObject<Solution>(spySolutionRequest);


        }
    }
}
