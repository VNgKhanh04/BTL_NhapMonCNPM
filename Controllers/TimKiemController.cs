using BTL_NMCNPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_NMCNPM.Controllers
{
    public class TimKiemController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TimKiemController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return Json(new { results = new string[] { } });
            }

            var results = await _context.tbl_Baihat
                .Where(b => EF.Functions.Like(b.TenBaiHat, $"%{keyword}%") && b.Deleted == false)
                .Select(b => b.TenBaiHat)
                .Take(10) // Giới hạn 10 kết quả
                .ToListAsync();

            return Json(new { results });
        }
        public async Task<IActionResult> ChiTiet(string tenBaiHat)
        {
            if (string.IsNullOrEmpty(tenBaiHat))
            {
                return NotFound();
            }

            var baiHat = await _context.tbl_Baihat
                .Where(b => b.TenBaiHat == tenBaiHat && b.Deleted == false)
                .FirstOrDefaultAsync();

            if (baiHat == null)
            {
                return NotFound();
            }

            return View(baiHat);
        }
    }
}
