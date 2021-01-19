using Core.Services.DbServices;
using Core.Services.IexServices;
using Core.Services.TransactionServices;
using Core.Services.UserServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class InterfaceConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApiHelper, ApiHelper>();
            services.AddScoped<IIexFetchService, IexFetchService>();
            services.AddScoped<IHandlePurchaseService, HandlePurchaseService>();
            services.AddScoped<IHandleSaleService, HandleSaleService>();
            services.AddScoped<ISetAllocatedFundsService, SetAllocatedFundsService>();
            services.AddScoped<IGetUserDataService, GetUserDataService>();
            services.AddScoped<IAddUserService, AddUserService>();
            services.AddScoped<IDeleteUserService, DeleteUserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICheckExpiration, CheckExpiration>();
            services.AddScoped<IQueryDbService, QueryDbService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<INHibernateSessionFactory>(new NHibernateSessionFactory(configuration));
        }
    }
}