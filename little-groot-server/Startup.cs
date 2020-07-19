using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using LittleGrootServer.Data;
using LittleGrootServer.Services;

namespace LittleGrootServer {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddScoped<IPlantsService, PlantsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddDbContext<LittleGrootDbContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("LittleGrootDbConnection")));
            services.AddSwaggerGen(opt => {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Little Groot API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(opt => {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Little Groot API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
