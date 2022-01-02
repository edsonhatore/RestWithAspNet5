using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Business;
using RestWithAspNet5.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using RestWithAspNet5.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using RestWithAspNet5.Repository.Generic;
using System.Net.Http.Headers;
using RestWithAspNet5.Hypermedia.Filters;
using RestWithAspNet5.Hypermedia.Enricher;

namespace RestWithAspNet5
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            
            
            
        }

      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            var connection = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options =>options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            if (Environment.IsDevelopment())
            {
                Migratedatabase(connection);
            }

            //services.AddMvc(options =>{
            //    options.RespectBrowserAcceptHeader = true;
            //    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
            //    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            //}).AddXmlSerializerFormatters();


            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());

            services.AddSingleton(filterOptions);

            services.AddApiVersioning();
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
        //   services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
            services.AddScoped<IBooksBusiness, BookBusinessImplementation>();
            services.AddScoped(typeof(IRepository<>),typeof(GenericRepository<>));

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=valure}/{id?}");
            });
        }

        private void Migratedatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySqlConnector.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,

                };

                evolve.Migrate();

            }
            catch (Exception ex ) 
            {
                Log.Error("Database migration failed",ex);
                throw;
            }
        }

    }
}
