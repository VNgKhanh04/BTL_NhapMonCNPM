using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BTL_NhapMonCNPM.Models;

namespace BTL_NhapMonCNPM.Controllers;

public class UploadController : Controller
{

    private readonly IBaiHatRepository _baiHatRepository;

    public UploadController(IBaiHatRepository baiHatRepository)
    {
        _baiHatRepository = baiHatRepository;
    }
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    [Route("upFile")]
    public async Task<IActionResult> TaiFile(string ms_name, IFormFile ms_file, string ms_theloai, string ms_mota)
    {
        try 
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["icon"] = "error";
                TempData["Message"] = "Vui lòng đăng nhập để tải file lên!";
                return RedirectToAction("Index", "Home");
            }
            
            if (ms_file == null || ms_file.Length == 0)
            {
                TempData["icon"] = "error";
                TempData["Message"] = "Vui lòng chọn file nhạc!";
                return RedirectToAction("Index", "Home");
            }

            // Tạo thư mục uploads nếu chưa tồn tại
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Tạo tên file độc nhất để tránh trùng lặp
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + ms_file.FileName;
            
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            Console.WriteLine($"Saving file to: {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ms_file.CopyToAsync(stream);
            }

            // Lưu thông tin file vào cơ sở dữ liệu (nếu cần)
            var baiHat = new Baihat
            {
                tenbaihat = ms_name,
                file = "/uploads/" + uniqueFileName, // Đường dẫn tương đối
                FK_theloai = int.Parse(ms_theloai),
                FK_nguoiup = userId.Value, 
                thoigian_up = DateTime.Now
            };

            bool result = await _baiHatRepository.AddNewBaiHat(baiHat);
            if (!result)
            {
                // Nếu không thành công, xóa file đã tải lên
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                TempData["icon"] = "error";
                TempData["Message"] = "Lỗi khi lưu bài hát vào cơ sở dữ liệu!";
                return RedirectToAction("Index", "Home");
            }

            TempData["icon"] = "success";
            TempData["Message"] = "Tải file lên thành công!";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading file: {ex.Message}");
            TempData["icon"] = "error";
            TempData["Message"] = "Lỗi khi tải file lên: " + ex.Message;
            return RedirectToAction("Index", "Home");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}