using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoAppBLL;
using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL;
using VideoAppDAL.Interfaces;

namespace VideoRestAPI
{
    public class Startup
    {
        private const string Localhost = "http://localhost:4200";
        private const string Azurehost = "";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc();

            // CORS for individual activation on controller
            //services.AddCors(o => o.AddPolicy("LocalPolicy", builder =>
            //{
            //    builder.WithOrigins("http://localhost:4200")
            //        .AllowAnyMethod()
            //        .AllowAnyHeader();
            //}));

            // HTTPS
            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute());
            //});

            services.AddScoped<IDALFacade, DALFacade>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IGenreService, GenreService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Use Redirect
            //var options = new RewriteOptions().AddRedirectToHttps();
            //app.UseRewriter(options);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Setup CORS
            app.UseCors(builder => builder.
            WithOrigins(Localhost, Azurehost)
            .AllowAnyMethod()
            .AllowCredentials());

            app.UseMvc();

        }
    }
}