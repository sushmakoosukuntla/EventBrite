using CartApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi
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
            services.AddControllers().AddNewtonsoftJson();
            //Transient meaning, It could come in and out. When ever we need it we will call it
            //ICartRepository is implemented by RedisCartRepository
            services.AddTransient<ICartRepository, RedisCartRepository>();
            //Singleton means there will always be one instance of the connectionmultiplexer(database)
            
            services.AddSingleton<ConnectionMultiplexer>(cm =>
            {
                /*In the below and above lines we are saying, through out the code in the microservice(CartApi),
                Where ever you asked to inject a connction multiplexer,it will inject everything that you have asked
                in every constructor of the class.*/
                /*first is to read the connection string of the Redis casche from your environment variable 
                and it gonna parse it, because redis expects you to parse the connection string*/
                //Redis casche is nothing but the Ip address, so we are resolving the Dns
                //If it fails connecting to the Ip address, we set AbortOnConnectFail false , because we gonna retry
                var configuration = ConfigurationOptions.Parse(Configuration["ConnectionString"], true);                
                configuration.ResolveDns = true;                
                configuration.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect(configuration);
            });

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
                options.Audience = "basket"; //And the client(Audience) name is Basket
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

//****Token server will only recieve the request from regestered users.*********
