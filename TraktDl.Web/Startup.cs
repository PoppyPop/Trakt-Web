using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TraktDl.Business.Remote.Trakt;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc(
                options => options.EnableEndpointRouting = false
                ).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped(typeof(IDatabase), typeof(Business.Database.SqLite.NHibernateDatabase));

            services.AddScoped(typeof(ITraktApiClient), typeof(TraktDl.Business.Remote.Trakt.TraktApiClient));

            services.AddScoped(typeof(ITrackingApi), typeof(TraktDl.Business.Remote.Trakt.TraktApi));
            //services.AddTransient(typeof(ITrackingApi), typeof(TraktDl.Business.Mock.Remote.Trakt.TraktApi));

            services.AddScoped(typeof(IImageApi), typeof(TraktDl.Business.Remote.Tmdb.Tmdb));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
