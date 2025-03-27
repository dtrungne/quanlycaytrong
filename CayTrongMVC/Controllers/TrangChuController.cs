using Microsoft.AspNetCore.Mvc;
using CayTrongMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Diagnostics;

namespace CayTrongMVC.Controllers
{
    public class TrangChuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrangChuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrangChu/Index
        public IActionResult Index(string search)
        {
            var filteredCayTrongs = string.IsNullOrEmpty(search)
                ? _context.CayTrong.ToList()
                : _context.CayTrong
                    .Where(c => c.Ten.ToLower().Contains(search.ToLower()))
                    .ToList();
            return View(filteredCayTrongs);
        }

        // GET: TrangChu/Chitiet/5
        public IActionResult Chitiet(int? ma) // Sử dụng kiểu int? cho tham số ma
        {
            if (ma == null)
            {
                return NotFound(); // Kiểm tra nếu ma là null
            }

            var cay = _context.CayTrong.FirstOrDefault(c => c.Ma == ma); // Kiểm tra với ma kiểu int
            if (cay == null)
            {
                return NotFound();
            }
            return View(cay);
        }

        // GET: TrangChu/Sua/5
        public IActionResult Sua(int? ma) // Sử dụng kiểu int? cho tham số ma
        {
            if (ma == null)
            {
                return NotFound();
            }

            var cay = _context.CayTrong.FirstOrDefault(c => c.Ma == ma);
            if (cay == null)
            {
                return NotFound();
            }
            return View(cay);
        }

        // POST: TrangChu/Sua/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sua(int? ma, [Bind("Ma,Ten,MoTa,HinhAnh")] CayTrong cay)
        {
            if (ma == null || !ModelState.IsValid)
            {
                return View(cay);
            }

            var caySua = _context.CayTrong.FirstOrDefault(c => c.Ma == ma);
            if (caySua != null)
            {
                caySua.Ten = cay.Ten;
                caySua.MoTa = cay.MoTa;
                caySua.HinhAnh = cay.HinhAnh;

                try
                {
                    _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Log lỗi chi tiết
                    Console.WriteLine(ex.InnerException?.Message);
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            }
            return NotFound();
        }

        // GET: TrangChu/Xoa/5
        public IActionResult Xoa(int? ma)
        {
            if (ma == null)
            {
                return NotFound();
            }

            var cay = _context.CayTrong.FirstOrDefault(c => c.Ma == ma);
            if (cay != null)
            {
                _context.CayTrong.Remove(cay);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: TrangChu/ThemCay
        public IActionResult ThemCay()
        {
            return View();
        }

        // POST: TrangChu/ThemCay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemCay(CayTrong cay)
        {
            if (ModelState.IsValid)
            {
                // Không cần gán giá trị cho Ma, vì Ma sẽ được tự động tăng dần
                _context.CayTrong.Add(cay);
                try
                {
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Log lỗi chi tiết nếu có
                    Console.WriteLine(ex.InnerException?.Message);
                    // Trả về trang lỗi với thông tin chi tiết hơn
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            }
            return View(cay);
        }
    }
}
