using API.OutputMappings;
using Core.Mappings;
using Core.Services.DbServices;
using Core.Services.IexServices;
using Core.Services.TransactionServices;
using Core.Services.UserServices;
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
            services.AddSpaStaticFiles(config => { config.RootPath = "client/build"; });
            services.AddScoped<IApiHelper, ApiHelper>();
            services.AddScoped<IIexFetchService, IexFetchService>();
            services.AddScoped<IStockListService, StockListService>();
            services.AddScoped<ICheckExistingHoldingsService, CheckExistingHoldingService>();
            services.AddScoped<IPurchaseSharesService, PurchaseSharesService>();
            services.AddScoped<ITransactionInputMap, TransactionInputMap>();
            services.AddScoped<ISetAllocatedFundsService, SetAllocatedFundsService>();
            services.AddScoped<ISellShareService, SellSharesService>();
            services.AddScoped<IHandleSaleService, HandleSaleService>();
            services.AddScoped<IHandlePurchaseService, HandlePurchaseService>();
            services.AddScoped<IAddUserService, AddUserService>();
            services.AddScoped<IDeleteUserService, DeleteUserService>();
            services.AddScoped<IGetUserDataService, GetUserDataService>();
            services.AddScoped<INHibernateSessionService, NHibernateSessionService>();
            services.AddScoped<IDbQueryService, DbQueryService>();
            services.AddScoped<IUserOutputMap, UserOutputMap>();
            services.AddScoped<IDbAddUserService, DbAddUserService>();
            services.AddScoped<IDbDeleteUserService, DbDeleteUserService>();
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

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}