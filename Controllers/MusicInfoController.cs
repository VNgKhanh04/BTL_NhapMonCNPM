using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL_NhapMonCNPM.Models;
using System.IO;

namespace BTL_NhapMonCNPM.Controllers;

public class MusicInfoController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBaiHatRepository _baiHatRepository;
    private readonly IBinhLuanRepository _binhLuanRepository;

    public MusicInfoController(ILogger<HomeController> logger, IBaiHatRepository BaiHatRepository, IBinhLuanRepository BinhLuanRepository)
    {
        _logger = logger;
        _baiHatRepository = BaiHatRepository;
        _binhLuanRepository = BinhLuanRepository;
    }

    
    public IActionResult Index(int id)
    {
        var baihat = _baiHatRepository.GetBaihatAsync(id).Result;
        if(baihat == null)
        {
            TempData["icon"] = "error";
            TempData["Message"] = "Bài hát đã xóa hoặc không tồn tại!";
            return RedirectToAction("Index", "Home");
        }
        var listBl = _binhLuanRepository.GetBinhLuanByBaiHatAsync(id).Result;
        ViewBag.BinhLuan = listBl;
        return View(baihat);
    }

    [HttpPost]
    [Route("addComment")]
    public async Task<IActionResult> AddComment(int baiHatId, string commentText)
    {
        try
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");
            
            if (userId == null)
            {
                return Json(new { 
                    success = false, 
                    message = "Vui lòng đăng nhập để bình luận!" 
                });
            }

            if (string.IsNullOrEmpty(commentText))
            {
                return Json(new { 
                    success = false, 
                    message = "Nội dung bình luận không được để trống!" 
                });
            }

            var binhluan = new Binhluan
            {
                FK_parent = baiHatId,
                FK_nguoiviet = userId.Value,
                noidung = commentText,
                thoigian = DateTime.Now,
                Username = userName
            };

            bool result = await _binhLuanRepository.AddNewBinhLuan(binhluan);

            if (!result)
            {
                return Json(new { 
                    success = false, 
                    message = "Có lỗi xảy ra khi thêm bình luận!" 
                });
            }

            var listBl = await _binhLuanRepository.GetBinhLuanByBaiHatAsync(baiHatId);

            return Json(new { 
                success = true, 
                comments = listBl,
                newComment = new { 
                    id = binhluan.Id,
                    userName = userName,
                    content = commentText,
                    time = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    userId = userId.Value
                }
            });
        }
        catch (Exception ex)
        {
            return Json(new { 
                success = false, 
                message = "Có lỗi xảy ra: " + ex.Message 
            });
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
