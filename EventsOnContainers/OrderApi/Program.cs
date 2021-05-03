using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi
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
                var context = serviceProviders.GetRequiredService<OrdersContext>();

                //Now we know that database is created
                //we can ensure if it is created
                MigrateDatabase.EnsureCreated(context);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
