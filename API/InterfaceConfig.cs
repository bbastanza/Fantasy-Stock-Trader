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
            services.AddScoped<IHandlePurchaseService, HandlePurchaseService>();
            services.AddScoped<IHandleSaleService, HandleSaleService>();
            services.AddScoped<ISetAllocatedFundsService, SetAllocatedFundsService>();
            services.AddScoped<IAddUserService, AddUserService>();
            services.AddScoped<IDeleteUserService, DeleteUserService>();
            services.AddScoped<IGetUserDataService, GetUserDataService>();
            services.AddScoped<INHibernateSession, NHibernateSession>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICheckExpiration, CheckExpiration>();
            services.AddScoped<IQueryDb, QueryDb>();
        }
    }
}