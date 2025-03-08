using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL_NhapMonCNPM.Models;
using System.IO;

namespace BTL_NhapMonCNPM.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBaiHatRepository _baiHatRepository;

    public HomeController(ILogger<HomeController> logger, IBaiHatRepository BaiHatRepository)
    {
        _logger = logger;
        _baiHatRepository = BaiHatRepository;
    }

    public IActionResult Index()
    {
        var listBH = _baiHatRepository.GetAllBaiHat().Result;
        return View(listBH);
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
