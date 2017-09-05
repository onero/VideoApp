﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoRestAPI
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var bllFacade = new BLLFacade();

                #region SeedDBData
                // Videos
                bllFacade.VideoService.Create(
                    new VideoBO()
                    {
                        Title = "Die Hard",
                        Genre = Genre.Action
                    });
                bllFacade.VideoService.Create(
                    new VideoBO()
                    {
                        Title = "Titanic",
                        Genre = Genre.Romance
                    });

                // Profiles
                bllFacade.ProfileService.Create(
                    new ProfileBO()
                    {
                        FirstName = "Adamino",
                        LastName = "Hansen",
                        Address = "Home"
                    });

                #endregion
            }

            app.UseMvc();
        }
    }
}
