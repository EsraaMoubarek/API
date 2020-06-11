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
using testwebapicore.HubConfig;
using Hangfire;
using Hangfire.MemoryStorage;

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
            services.AddScoped<ClientRepo>();
            services.AddScoped<AddressRepo>();
            services.AddScoped<WasteRepo>();
            services.AddScoped<comp_prom_repo>();
            services.AddScoped<Promotions_repo>();
            services.AddScoped<promcodes_repo>();
            services.AddScoped<FeedbackRepo>();
            services.AddScoped<FeedbackCategoryRepo>();
            services.AddScoped<SurveyRepo>();
            services.AddSignalR();

            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            //if (env.IsDevelopment())
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
                endpoints.MapHub<ChartHub>("/charthub");
            });

            app.UseHangfireDashboard();
            //backgroundJobClient.Enqueue(() => Console.WriteLine("Hello Hanfire job!"));
            recurringJobManager.AddOrUpdate(
                "Run every minute",
                () => serviceProvider.GetService<RequestRepo>().AssignRequestsToCollectors(),
                "0 9 * * *"
            );

        }
    }
}
