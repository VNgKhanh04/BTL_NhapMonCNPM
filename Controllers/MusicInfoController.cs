using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL_NhapMonCNPM.Models;
using System.IO;

namespace BTL_NhapMonCNPM.Controllers;

public class MusicInfoController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBaiHatRepository _baiHatRepository;

    public MusicInfoController(ILogger<HomeController> logger, IBaiHatRepository BaiHatRepository)
    {
        _logger = logger;
        _baiHatRepository = BaiHatRepository;
    }

    
    public IActionResult Index(int id)
    {
        var baihat = _baiHatRepository.GetBaihatAsync(id).Result;
        return View(baihat);
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
