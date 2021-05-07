using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderApi.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //COntrollers are going to communicate back and fourth  using Newtonsoft.json
            services.AddControllers().AddNewtonsoftJson() ;
            //Here is where we bring in the connection strings and inject in to the database.
            /*We created a variable called connectionString and Read my ConnectionString from Configuration file.*/
            //var connectionString = Configuration["ConnectionString"]; //This connection string is for IIS Express
            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabasePassword"];
            var connectionString = $"Server={server};Database={database};User Id={user};Password={password}";

            //Now we have to inject this connection to the DB context.
            /*We are saying to services that, Hey services AddDbContext to my project, Which means, my project
            requires you to set up a DB context CatalogContext*/
            //CatalogContext is a type of DBContext
            /*options => options.UseSqlServer(), we are telling what kind of database we are using,
            which is sql server here and we are providing connection strings as a parameter. This is how we 
            officialy injected the database */
            services.AddDbContext<OrdersContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);

            // prevent from mapping "sub" claim to nameidentifier.
            //JWT stands for Json WebToken
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); //KAL will explain this later.

            var identityUrl = Configuration["IdentityUrl"];
            services.AddAuthentication(options =>
            {
                /*JwtBearer means once you login in to application, in the backend 
                 * the login tocken will be verified, it means once it is verified, you will be
                 * resposible for any action that made in your account.*/
                /*Every token that is generated in the backend is most likely a bearer token*/
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //In below line you are specifying the authority, like who your authority is.
                options.Authority = identityUrl.ToString(); //Authority comes from the identity url
                options.RequireHttpsMetadata = false; //We are mentioning whether we need https or not
                options.Audience = "order"; //And the client(Audience) name is order
              
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
