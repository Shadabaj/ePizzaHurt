using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaHub.Services.Implementation;
using PizzaHub.Services.Interface;
using PizzaHurt.Repositories.Implementations;
using PizzaHurt.Repositories.Interfaces;
using PizzaHurt.Services.Implementation;
using PizzaHurt.Services.Interface;
using PizzaHut.Core;
using PizzaHut.Core.Entities;

namespace PizzaHurt.Services
{
    public class ConfigureDependency
    {

        public static void RegisterService(IServiceCollection services,IConfiguration configuration)
        {
            //Database 

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            //Repositories

            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IActivityLogRepository,ActivityLogRepository>();
            services.AddScoped<ICartRespository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IRepository<Item>,Repository<Item>>();
            services.AddScoped<IRepository<Cart>,Repository<Cart>>();
            services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
            services.AddScoped<IRepository<PaymentDetail>,Repository<PaymentDetail>>();
            services.AddScoped<IRepository<Order>,Repository<Order>>();
            services.AddScoped<IRepository<ItemType>, Repository<ItemType>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();

            //Services

            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IActivityLogService,ActivityLogService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICatalogService,CatalogService>();

            services.AddScoped<IService<Item>, Service<Item>>();
            services.AddScoped<IService<PaymentDetail>,Service<PaymentDetail>>();
           

        }
    }
}
