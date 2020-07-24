using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskAPI.Models;
using TasksWebService.Models;

namespace TaskAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DBServer"];
            var port = Configuration["DBPort"];
            var user = Configuration["DBUser"];
            var password = Configuration["DBPassword"];
            var database = Configuration["Database"];

            services.AddDbContext<TaskContext>(options => 
            options.UseSqlServer(
                $"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();

            PrepDB.PrepPopulation(app);
        }
    }
}
