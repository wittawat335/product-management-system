﻿using Ecommerce.Domain.RepositoryContracts;
using Ecommerce.Infrastructure.DBContext;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure
{
    public static class ServiceExtentions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ECommerceContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection") 
                    ?? throw new InvalidOperationException("Connection string not found"),
                    sqlServerOptionsAction: options =>
                    {
                        options.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                    });
            });
            services.AddSingleton<DapperContext>(); // Singleton : ถูกสร้างแค่ครั้งแรกที่ถูกเรียก หลังจากนั้น Instance จะคงอยู่ตลอดไปจนกว่าเราจะปิดและเปิดระบบขึ้นมาใหม่
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Transient : ถูกสร้างใหม่ทุกครั้งที่มีการเรียกใช้งานภายในระบบ
            services.AddScoped<IStoredRespository, StoredRespository>(); // Scoped : จะถูกสร้างใหม่ทุกครั้งที่ Client Request (1 Connection = 1 Client Request)
        }
    }
}
