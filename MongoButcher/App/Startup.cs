using System;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.ActionHistories;
using MongoDBDemoApp.Core.Workloads.Categories;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Recipes;
using MongoDBDemoApp.Core.Workloads.Resources;

namespace App
{
    public class Startup
    {
        private const string ORIGIN = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection(AppSettings.KEY));
            services.AddAutoMapper(typeof(MapperProfile));

            // configure fwk
            services.AddLeoMongo<MongoConfig>();

            // for bigger assemblies it would be alright to register those via reflection by naming convention!
            services.AddScoped<IActionHistoryRepository, ActionHistoryRepository>();
            services.AddScoped<IActionHistoryService, ActionHistoryService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IResourceService, ResourceService>();

            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeService, RecipeService>();

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            
            services.AddControllers();

            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy(ORIGIN,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5000",
                                "http://localhost:4200") // Angular CLI
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        services.AddScoped<IProductRepository, ProductRepository>();
                        services.AddScoped<IProductService, ProductService>();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); });
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            

            app.UseMiddleware<ErrorLoggingMiddleware>();

            app.UseRouting();

            app.UseCors(ORIGIN);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public sealed class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}