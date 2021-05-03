using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PaePae.Test.AspNetCore.WebApi.MultiplePaths.MultipleApps.V1
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
            services.AddControllers();
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "v1",
                    new OpenApiInfo() { Version = "v1", Title = "API V1" }
                );
                setupAction.DocumentFilter<PathPrefixInsertDocumentFilter>("api/v1");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class PathPrefixInsertDocumentFilter : IDocumentFilter
    {
        private readonly string _pathPrefix;

        public PathPrefixInsertDocumentFilter(string prefix)
        {
            this._pathPrefix = prefix;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.Keys.ToList();
            foreach (var path in paths)
            {
                var pathToChange = swaggerDoc.Paths[path];
                swaggerDoc.Paths.Remove(path);
                swaggerDoc.Paths.Add("/" + _pathPrefix + path, pathToChange);
            }
        }
    }
}
