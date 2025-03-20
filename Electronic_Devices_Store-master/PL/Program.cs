using DAL.Database;
using Microsoft.EntityFrameworkCore;
using DAL.Reposatory.Abstraction;
using DAL.Reposatory.Implementation;
using BLL.Services.Absraction;
using BLL.Services.Implementation;
using BLL.Services.Abstraction; // Make sure this exists
using BLL.Services;




namespace PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("projectConnectionString"));
            });

            // Register dependencies
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();

            // Add Cart services and repository
            builder.Services.AddScoped<ICardRepo, CardRepo>();
            builder.Services.AddScoped<ICardServices, CardServices>();

            // Add Order services and repository
            builder.Services.AddScoped<IOrderRepo, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
