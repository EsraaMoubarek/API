using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using testwebapicore.Models;
using testwebapicore.Models.repo;

namespace testwebapicore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        string MyAllowSpecificOrigins = "m";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //Scaffold-DbContext "Server=.;Database= WasteAppDb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //
            services.AddDbContext<WasteAppDbContext>(oop => oop.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("wastcon")));
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            services.AddSwaggerDocument();
            services.AddCors(options => { options.AddPolicy(MyAllowSpecificOrigins, builder => { 
                builder.AllowAnyOrigin(); 
                builder.AllowAnyMethod();
                builder.AllowAnyHeader(); }); 
            });
            services.AddScoped<SchedulRepo>();
            services.AddScoped<RegionRepo>();
            services.AddScoped<UserRepo>();
            services.AddScoped<Sch_col_Repo>();
            services.AddScoped<RequestRepo>();





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

            app.UseAuthorization();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
