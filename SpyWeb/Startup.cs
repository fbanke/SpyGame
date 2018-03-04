using System;
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

            Setup();
        }

        public static CloudQueue SpyQueue { get; private set; }

        public void Setup()
        {
            string connection = "";
            try
            {
                connection = CloudConfigurationManager.GetSetting("AzureStorageConnectionString");
            }
            catch (Exception e)
            {
                connection = "UseDevelopmentStorage=true";
            }
            var storageAccount = CloudStorageAccount.Parse(connection);
            var queueClient = storageAccount.CreateCloudQueueClient();
            
            SpyQueue = queueClient.GetQueueReference("spy-queue");
            SpyQueue.CreateIfNotExistsAsync();
        }
    }
}