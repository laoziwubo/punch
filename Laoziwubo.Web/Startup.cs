using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laoziwubo.BLL;
using Laoziwubo.DAL;
using Laoziwubo.IBLL;
using Laoziwubo.IDAL;
using Laoziwubo.Model.Common;
using Laoziwubo.Model.Content;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Laoziwubo.Web
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

            //DataBase
            services.AddScoped<Connection>();

            //BaseD
            services.AddScoped<GenericM>();
            services.AddScoped<Dictionary<string, object>>();

            //Hero
            services.AddScoped<IBaseB<HeroM>, BaseB<HeroM>>();
            services.AddScoped<IBaseD<HeroM>, BaseD<HeroM>>();

            //Crawler
            services.AddScoped<IHeroB<HeroM>, HeroB<HeroM>>();
            services.AddScoped<IHeroD<HeroM>, HeroD<HeroM>>();

            //Sticker
            services.AddScoped<IBaseB<StickerM>, BaseB<StickerM>>();
            services.AddScoped<IBaseD<StickerM>, BaseD<StickerM>>();

            //Punch
            services.AddScoped<IBaseB<PunchM>, BaseB<PunchM>>();
            services.AddScoped<IBaseD<PunchM>, BaseD<PunchM>>();

            //Passionate
            services.AddScoped<IBaseB<PassionateM>, BaseB<PassionateM>>();
            services.AddScoped<IBaseD<PassionateM>, BaseD<PassionateM>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Index/Index");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}
