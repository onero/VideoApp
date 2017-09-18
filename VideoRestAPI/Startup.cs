using System;
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

                //#region SeedDBData

                //// Genres
                //var action = bllFacade.GenreService.Create(
                //    new GenreBO()
                //    {
                //        Name = "Action"
                //    });
                //var romance = bllFacade.GenreService.Create(
                //    new GenreBO()
                //    {
                //        Name = "Romance"
                //    });
                //// Videos
                //var dieHard = bllFacade.VideoService.Create(
                //    new VideoBO()
                //    {
                //        Title = "Die Hard",
                //        PricePerDay = 15,
                //        GenreIds = new List<int>() {action.Id}
                //    });
                //bllFacade.VideoService.Create(
                //    new VideoBO()
                //    {
                //        Title = "Titanic",
                //        GenreIds = new List<int>() { romance.Id }
                //    });


                //// Profiles
                //bllFacade.ProfileService.Create(
                //    new ProfileBO()
                //    {
                //        FirstName = "Adamino",
                //        LastName = "Hansen",
                //        Address = "Home"
                //    });

                //// Roles
                //var admin = bllFacade.RoleService.Create(
                //    new RoleBO()
                //    {
                //        Name = "Admin"
                //    });

                //// Users
                //var adamino = bllFacade.UserService.Create(
                //    new UserBO()
                //    {
                //        Username = "Adamino",
                //        Password = "Secret",
                //        RoleId = admin.Id
                //    });

                //// Rentals
                //bllFacade.RentalService.Create(
                //    new RentalBO()
                //    {
                //        VideoId = dieHard.Id,
                //        UserId = adamino.Id
                //    });

                //#endregion
            }

            app.UseMvc();
        }
    }
}