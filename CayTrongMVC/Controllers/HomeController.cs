using System.Diagnostics;
using CayTrongMVC.Models;
using CayTrongMVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CayTrongMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;  // Thêm context để kết nối với cơ sở dữ liệu

        // Constructor thêm ApplicationDbContext
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Đang truy cập trang chủ (Index).");  // Ghi nhật ký khi người dùng truy cập vào trang chủ
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Đang truy cập trang Privacy.");
            return View();
        }

        // GET: Thêm Cây Trồng
        public IActionResult ThemCay()
        {
            return View();
        }

        // POST: Thêm Cây Trồng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemCay(CayTrong cay)
        {
            if (ModelState.IsValid)
            {
                _context.CayTrong.Add(cay);  // Thêm cây trồng vào database
                _context.SaveChanges();      // Lưu thay đổi
                _logger.LogInformation($"Đã thêm cây trồng: {cay.Ten} vào hệ thống."); // Ghi nhật ký thêm cây trồng thành công
                return RedirectToAction("Index");  // Quay lại danh sách cây trồng
            }
            return View(cay);  // Nếu có lỗi, giữ lại form để người dùng sửa
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Lấy ID của yêu cầu hiện tại hoặc TraceIdentifier nếu không có Activity ID
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            // Ghi nhật ký lỗi (nếu có)
            _logger.LogError($"Có lỗi xảy ra. Request ID: {requestId}");

            return View(new ErrorViewModel { RequestId = requestId });
        }
    }
}
