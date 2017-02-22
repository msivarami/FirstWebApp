using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
using SampleWebApiAspNetCore.Models;
using SampleWebApiAspNetCore.Repositories;
using SampleWebApiAspNetCore.Services;
[assembly: OwinStartup(typeof(FirstWebAPI.Startup))]

namespace FirstWebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddSingleton<IHouseRepository, HouseRepository>();
        //    services.AddTransient<IHouseMapper, HouseMapper>();

        //    // Add framework services.
        //    services.AddMvcCore().AddJsonFormatters();
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    }
}
