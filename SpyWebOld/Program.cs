using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

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
            SetupCloudTable();

            host.Run();
        }

        public static CloudTable SpyTable{ get; private set; }

        public static void SetupCloudTable()
        {
            //string connection = CloudConfigurationManager.GetSetting("AzureStorageConnectionString");
            string connection = "UseDevelopmentStorage=true";

            var storageAccount = CloudStorageAccount.Parse(connection);
            var tableClient = storageAccount.CreateCloudTableClient();

            SpyTable = tableClient.GetTableReference("spytable");
            SpyTable.CreateIfNotExistsAsync().GetAwaiter().GetResult();    
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

        /// <summary>
        /// The Table Service supports two main types of insert operations.
        ///  1. Insert - insert a new entity. If an entity already exists with the same PK + RK an exception will be thrown.
        ///  2. Replace - replace an existing entity. Replace an existing entity with a new entity.
        ///  3. Insert or Replace - insert the entity if the entity does not exist, or if the entity exists, replace the existing one.
        ///  4. Insert or Merge - insert the entity if the entity does not exist or, if the entity exists, merges the provided entity properties with the already existing ones.
        /// </summary>
        /// <param name="entity">The entity to insert or merge</param>
        /// <returns>A Task object</returns>
        public static async Task<Solution> InsertOrMergeEntityAsync(Solution entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await SpyTable.ExecuteAsync(insertOrMergeOperation);
                Solution insertedCustomer = result.Result as Solution;

                return insertedCustomer;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public static List<Solution> GetAllSolutions()
        {
            var solutions = new List<Solution>();
            TableQuery<Solution> tableQuery = new TableQuery<Solution>();

            // Initialize the continuation token to null to start from the beginning of the table.
            TableContinuationToken continuationToken = null;
            do
            {
                // Retrieve a segment (up to 1,000 entities).
                TableQuerySegment<Solution> tableQueryResult =
                    SpyTable.ExecuteQuerySegmentedAsync(tableQuery, continuationToken).GetAwaiter().GetResult();

                // Assign the new continuation token to tell the service where to
                // continue on the next iteration (or null if it has reached the end).
                continuationToken = tableQueryResult.ContinuationToken;

                // Print the number of rows retrieved.
                foreach(var solution in tableQueryResult.Results)
                {
                    solutions.Add(solution);
                }

                // Loop until a null continuation token is received, indicating the end of the table.
            } while (continuationToken != null);

            return solutions;
        }
    }
}