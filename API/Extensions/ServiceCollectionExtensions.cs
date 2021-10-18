using Domain.Companies;
using Domain.DTOs.Compaines;
using Domain.DTOs.Companies;
using Domain.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using FluentValidation;
using Infrastructre.Services.Compaines;
using Infrastructre.Services.Users;
using Infrastructre.Validators;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICompanyRepository, CompanyRepository>();

        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services
            , IConfiguration configuration)
        {
            return services.AddDbContext<EFContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<AddCompanyRequest>, CompanyAddValidator>();
            services.AddScoped<IValidator<UpdateCompanyRequest>, CompanyUpdateValidator>();
            services.AddScoped<IValidator<CompanyBaseRequest>, CompanyGetValidator>();
            services.AddScoped<IValidator<DeleteCompanyRequest>, CompanyDeleteValidator>();
            services.AddScoped<IValidator<AddUserRequest>, AddUserValidator>();
            services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserValidator>();
            services.AddScoped<IValidator<UserBaseRequest>, GetUserValidator>();


            return services;
        }


        public static IServiceCollection AddBusinessServices(this IServiceCollection services
           )
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<ICompanyService, CompanyService>();
        }
    }
}