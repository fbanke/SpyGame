using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Azure;

namespace SpyWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                // var services = scope.ServiceProvider;
            }

            SetupCloudQueue();

            host.Run();
        }

        public static CloudQueue SpyQueue { get; private set; }

        public static void SetupCloudQueue()
        {
            //string connection = CloudConfigurationManager.GetSetting("AzureStorageConnectionString");
            string connection = "UseDevelopmentStorage=true";
            var storageAccount = CloudStorageAccount.Parse(connection);
            var queueClient = storageAccount.CreateCloudQueueClient();

            SpyQueue = queueClient.GetQueueReference("spy-queue");
            SpyQueue.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        
        public static string GetEnvironmentVariable(string name)
        {
            return name + ": " + 
                   System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}