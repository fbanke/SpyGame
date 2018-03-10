using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SpyWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region snippet_ConfigureServices
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }
        #endregion

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            SetupCloudQueue();
        }

        public static CloudQueue SpyQueue { get; private set; }

        public void SetupCloudQueue()
        {
            string connection = "";
            try
            {
                connection = CloudConfigurationManager.GetSetting("AzureStorageConnectionString");
            }
            catch (Exception)
            {
                connection = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;";
            }
            var storageAccount = CloudStorageAccount.Parse(connection);
            var queueClient = storageAccount.CreateCloudQueueClient();

            SpyQueue = queueClient.GetQueueReference("spy-queue");
            SpyQueue.CreateIfNotExistsAsync().GetAwaiter().GetResult();

        }
    }
}