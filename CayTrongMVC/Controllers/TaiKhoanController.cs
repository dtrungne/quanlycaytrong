using CayTrongMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CayTrongMVC.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaiKhoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị form đăng nhập
        public IActionResult DangNhap()
        {
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public async Task<IActionResult> DangNhap(string tenDangNhap, string matKhau)
        {
            var user = await _context.TaiKhoan
                .FirstOrDefaultAsync(t => t.TenDangNhap == tenDangNhap && t.MatKhau == matKhau);

            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.TenDangNhap);
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin ? "true" : "false");

                return RedirectToAction("Index", "TrangChu");
            }

            ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }

        // Hiển thị form đăng ký
        public IActionResult DangKy()
        {
            return View();
        }

        // Xử lý đăng ký
        [HttpPost]
        public async Task<IActionResult> DangKy(TaiKhoan model, string xacNhanMatKhau)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.MatKhau != xacNhanMatKhau)
            {
                ViewBag.ThongBao = "Mật khẩu xác nhận không khớp!";
                return View(model);
            }

            // Kiểm tra tài khoản đã tồn tại chưa
            var existingUser = await _context.TaiKhoan.FirstOrDefaultAsync(t => t.TenDangNhap == model.TenDangNhap);
            if (existingUser != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại!";
                return View(model);
            }

            _context.TaiKhoan.Add(model);
            await _context.SaveChangesAsync();

            TempData["ThongBao"] = "Đăng ký thành công!";
            return RedirectToAction("DangNhap");
        }

        // Đăng xuất
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "TrangChu");
        }
    }
}
