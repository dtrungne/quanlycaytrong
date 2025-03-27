using CayTrongMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CayTrongMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Cấu hình DbContext với SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Thêm dịch vụ MVC và Session
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session (30 phút)
                options.Cookie.HttpOnly = true; // Bảo mật: Cookie chỉ có thể truy cập qua HTTP
                options.Cookie.IsEssential = true; // Đảm bảo session hoạt động ngay cả khi tắt tracking
            });

            var app = builder.Build();

            // Middleware xử lý lỗi
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Cấu hình Middleware
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession(); // 🔥 Kích hoạt Session (QUAN TRỌNG)
            app.UseAuthorization();

            // Định tuyến
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=TrangChu}/{action=Index}/{ma?}");

            app.Run();
        }
    }
}
