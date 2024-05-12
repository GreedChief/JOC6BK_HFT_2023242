using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Endpoint
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
            services.AddTransient<GameDbContext>();

            services.AddTransient<IRepository<Game>, GameRepository>();
            services.AddTransient<IRepository<Role>, RoleRepository>();
            services.AddTransient<IRepository<Player>, PlayerRepository>();
            services.AddTransient<IRepository<Developer>, DeveloperRepository>();

            services.AddTransient<IGameLogic, GameLogic>();
            services.AddTransient<IRoleLogic, RoleLogic>();
            services.AddTransient<IPlayerLogic, PlayerLogic>();
            services.AddTransient<IDeveloperLogic, DeveloperLogic>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JOC6BK_HFT_2023242.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JOC6BK_HFT_2023242.Endpoint v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
