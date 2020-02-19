using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {           
            services.AddControllers();
            IdentityModelEventSource.ShowPII = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = Environment.GetEnvironmentVariable("ASPNETCORE_HOSTIP");
                options.RequireHttpsMetadata = false;

                options.Audience = "api1";
            });

        }

        public void Configure(IApplicationBuilder app)
        {

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
