using API.ApiServices;
using API.Mappings;
using Core.Entities.Transactions.TransactionServices;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "client/build";
            });
            services.AddScoped<IApiHelper, ApiHelper>();
            services.AddScoped<IIexFetchService, IexFetchService>();
            services.AddScoped<IStockListService, StockListService>();
            services.AddScoped<ICheckExistingHoldingsService, CheckExistingHoldingService>();
            services.AddScoped<IPurchaseSharesService, PurchaseSharesService>();
            services.AddScoped<ITransactionMapper, TransactionMapper>();
            services.AddScoped<ISetAllocatedFundsService, SetAllocatedFundsService>();
            services.AddScoped<ISellShareService, SellSharesService>();
            services.AddScoped<IApiSellService, ApiSellService>();
            services.AddScoped<IApiPurchaseService, ApiPurchaseService>();

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
            
            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript:"start");
                }
            });
        }
    }
}
