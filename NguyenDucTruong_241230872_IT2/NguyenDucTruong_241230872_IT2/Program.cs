using NguyenDucTruong_241230872_IT2.Models;
using Microsoft.EntityFrameworkCore;
namespace NguyenDucTruong_241230872_IT2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("QltaiSanContext") ?? throw new InvalidOperationException("Connection string 'QltaiSanContext' not found.");

            builder.Services.AddDbContext<QltaiSanContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews( o =>
            {
                o.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(_ => "Nguyen gia phai la so 1 - 2000");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
