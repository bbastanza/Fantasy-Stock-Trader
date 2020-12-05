using API.OutputMappings;
using Core.Mappings;
using Core.Services.DbServices;
using Core.Services.IexServices;
using Core.Services.TransactionServices;
using Core.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class InterfaceConfig
    {
        public static void Configure(IServiceCollection services)
        {
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
    }
}