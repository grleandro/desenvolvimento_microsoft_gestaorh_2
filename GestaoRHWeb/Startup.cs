﻿using GestaoRHWeb.DAL;
using GestaoRHWeb.Models;
using GestaoRHWeb.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GestaoRHWeb
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
            services.AddScoped<FuncionarioDAO>();
            services.AddScoped<CaixaDAO>();
            services.AddScoped<ProntuarioDAO>();
            services.AddScoped<SolicitacaoDAO>();
            services.AddScoped<ItemSolicitacaoDAO>();
            services.AddScoped<Sessao>();
            services.AddHttpContextAccessor();

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("Connection")));

            services.AddSession();
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Funcionario}/{action=Index}/{id?}");
            });
        }
    }
}
