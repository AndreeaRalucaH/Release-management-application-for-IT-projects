using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using Relmonitor.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor
{
    public class Startup
    {
        readonly string MyAllowRigths = "_myAllow";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddScoped<IAplicatii, AplicatiiRepo>();
            services.AddScoped<IDetaliiRelease, DetaliiReleaseRepo>();
            services.AddScoped<IDurateRelease, DurateReleaseRepo>();
            services.AddScoped<IImpulse, ImpulseRepo>();
            services.AddScoped<IMedii, MediiRepo>();
            services.AddScoped<IStatus, StatusRepo>();
            services.AddScoped<IRelease, ReleaseRepo>();
            services.AddScoped<IUtilizatori, UtilizatoriRepo>();
            services.AddTransient<IDocuments, DocumentsRepo>();
            services.AddTransient<IEmail, EmailRepo>();
            services.AddHttpClient<IJira, JiraRepo>();
            services.AddHttpClient<IImpulseCreate, ImpulseCreateChange>();
            services.Configure<DocumentsSettings>(Configuration.GetSection("DocumentsSettings"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddDbContext<postgresContext>(options => options.UseNpgsql(Configuration.GetConnectionString("RelmonitorConnection")));
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowRigths, builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});
            app.UseCors("_myAllow");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=detaliirelease}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
