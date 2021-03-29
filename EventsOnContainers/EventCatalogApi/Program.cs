using EventCatalogApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //It creates the hostmachine
            //We are asking host to build the containers, When you start this project.
            //so after the successful build we need to seed the data
            var host = CreateHostBuilder(args).Build();

            //Now lets call the host and ask for the services.
            //CreateScope() mean get me the access to all the services that are available with in my host.
            //scope is a huge memory variable.             
            using (var scope = host.Services.CreateScope())
            {
                //The variable scope is wrapped by "using" statement 
                //What ever we write the code inside this, is the only code that can use this varible "scope"
                //the scope of the varible is limited in this using block.
                /*WHat is the purpose of using statement??  It guarantee that WHen you get out of this curly brackets
                  It is going to destroy scope variable, which means your memory will be released*/

                /*Now we can ask the scope that, can you tell me all the service providers ,that is available 
                 * for each scope*/
                var serviceProviders = scope.ServiceProvider;//we are getting all the serviceproviders

                /*Out of those service providers, can you get the acces to this requiredService which is 
                provided by catalog context*/
                var context = serviceProviders.GetRequiredService<EventContext>();

                //Now we know that database is created
                //we can now call the seed
                EventSeed.Seed(context);
            }
            host.Run();
                
        }
        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //We rae asking to create a builder 
            Host.CreateDefaultBuilder(args)
                // and configure the web service using the startup file(StartUp.cs)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //This calls the startup class
                    webBuilder.UseStartup<Startup>();
                });
    }
}
