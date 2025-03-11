using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL_NhapMonCNPM.Models;
using System.IO;

namespace BTL_NhapMonCNPM.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBaiHatRepository _baiHatRepository;
    private readonly IPlaylistRepository _playlistRepository;

    public HomeController(ILogger<HomeController> logger, IBaiHatRepository BaiHatRepository, IPlaylistRepository PlaylistRepository)
    {
        _logger = logger;
        _baiHatRepository = BaiHatRepository;
        _playlistRepository = PlaylistRepository;
    }

    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var listBH = _baiHatRepository.GetAllBaiHat().Result;
        if (userId.HasValue)
        {
            var playList = _playlistRepository.GetPlaylistByUser(userId.Value).Result;
            ViewBag.PlayList = playList;
        }
        else
        {
            ViewBag.PlayList = null;
        }
        return View(listBH);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    [Route("createPlaylist")]
    public async Task<IActionResult> CreatePlaylistAsync(string pl_name)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            TempData["icon"] = "error";
            TempData["Message"] = "Vui lòng đăng nhập để tạo playlist!";
            return RedirectToAction("Index", "Home");
        }
        if (string.IsNullOrEmpty(pl_name))
        {
            TempData["icon"] = "error";
            TempData["Message"] = "Vui lòng nhập tên playlist!";
            return RedirectToAction("Index", "Home");
        }

        try
        {
            var Playlist = new Playlist
            {
                tenPlaylist = pl_name,
                FK_nguoitao = userId.Value
            };
            bool result = await _playlistRepository.AddNewPlaylist(Playlist);
            if (!result)
            {
                TempData["icon"] = "error";
                TempData["Message"] = "Tạo playlist không thành công!";
                return RedirectToAction("Index", "Home");
            }


            TempData["icon"] = "success";
            TempData["Message"] = "Tạo playlist thành công!";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            TempData["icon"] = "error";
            TempData["Message"] = "Tạo playlist không thành công!";
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search(string q, int limit = 5)
    {
        if (string.IsNullOrEmpty(q))
        {
            return Json(new { results = new List<object>(), totalCount = 0 });
        }

        try
        {
            var results = await _baiHatRepository.SearchBaiHatAsync(q, limit);
            var formattedResults = results.Select(bh => new
            {
                id = bh.Id,
                title = bh.tenbaihat, 
                imageUrl = "https://th.bing.com/th/id/OIP.9RTAxkK_IjRInuyIWn0dWAAAAA?rs=1&pid=ImgDetMain", 
                fileUrl = bh.file
            }).ToList();

            var totalCount = await _baiHatRepository.CountSearchResultsAsync(q);

            return Json(new { results = formattedResults, totalCount });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching songs with query: {SearchQuery}", q);
            return Json(new { error = "Đã xảy ra lỗi khi tìm kiếm" });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
