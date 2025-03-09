using BTL_NhapMonCNPM.Data;
using BTL_NhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Controllers
{
    [Route("tai-khoan")]
    public class TaiKhoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaiKhoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("dang-ky")]
        public IActionResult DangKy()
        {
            return View("~/Views/TaiKhoan/DangKy.cshtml");

        }

        [HttpPost("dang-ky")]
        public async Task<IActionResult> DangKy(DangKyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.TaiKhoans.Any(t => t.TenTaiKhoan == model.TenTaiKhoan || t.Email == model.Email))
            {
                ModelState.AddModelError("", "Tên tài khoản hoặc email đã tồn tại");
                return View(model);
            }

            var taiKhoan = new TaiKhoanDangKy
            {
                TenTaiKhoan = model.TenTaiKhoan,
                MatKhau = model.MatKhau,
                Email = model.Email
            };

            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            ViewBag.SuccessMessage = "Đăng ký thành công!";

            return View(model); // Không redirect
        }


    }
}
