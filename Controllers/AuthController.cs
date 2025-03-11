using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL_NhapMonCNPM.Models;

namespace BTL_NhapMonCNPM.Controllers;

public class AuthController : Controller
{
    private readonly ITaiKhoanRepository _taiKhoanRepository;

    public AuthController(ITaiKhoanRepository taiKhoanRepository)
    {
        _taiKhoanRepository = taiKhoanRepository;
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("auth")]
    public IActionResult Auth(string username, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["icon"] = "error";
                TempData["Message"] = "Vui lòng nhập đầy đủ thông tin!";
                return RedirectToAction("Index", "Home");
            }

            var taiKhoan = _taiKhoanRepository.AuthenticateAsync(username, password).Result;
            if (taiKhoan != null)
            {
                SetSession(taiKhoan.TenTaiKhoan, taiKhoan.Id, taiKhoan.Quyen);
                TempData["icon"] = "success";
                TempData["Message"] = "Đăng nhập thành công!";
                return RedirectToAction("Index", "Home");
            }

            TempData["icon"] = "error";
            TempData["Message"] = "Đăng nhập không thành công!";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            TempData["icon"] = "error";
            TempData["Message"] = "Đăng nhập không thành công!";
            return RedirectToAction("Index", "Home");
        }

    }

    [HttpPost]
    [Route("register")]
    public IActionResult RegisterPost(string username, string password_regis, string email)
    {
        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password_regis) || string.IsNullOrEmpty(email))
        {
            TempData["icon"] = "error";
            TempData["Message"] = "Vui lòng nhập đầy đủ thông tin!";
            return RedirectToAction("Index", "Home");
        }

        if(_taiKhoanRepository.GetByUsernameAsync(username).Result != null)
        {
            TempData["icon"] = "error";
            TempData["Message"] = "Tên tài khoản đã tồn tại!";
            return RedirectToAction("Index", "Home");
        }

        try
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password_regis) || string.IsNullOrEmpty(email))
            {
                TempData["icon"] = "error";
                TempData["Message"] = "Vui lòng nhập đầy đủ thông tin!";
                return RedirectToAction("Index", "Home");
            }

            var taiKhoan = new Taikhoan
            {
                TenTaiKhoan = username,
                MatKhau = password_regis,
                Email = email,
                Quyen = "user"
            };

            var result = _taiKhoanRepository.CreateAsync(taiKhoan).Result;
            Console.WriteLine(result);
            if (result)
            {
                TempData["icon"] = "success";
                TempData["Message"] = "Đăng ký thành công!";
                return RedirectToAction("Index", "Home");
            }

            TempData["icon"] = "error";
            TempData["Message"] = "Đăng ký không thành công!";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            TempData["icon"] = "error";
            TempData["Message"] = "Đăng ký không thành công!";
            return RedirectToAction("Index", "Home");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult SetSession(string username, int id, string quyen)
    {
        HttpContext.Session.SetString("UserName", username);
        HttpContext.Session.SetString("UserQuyen", quyen);
        HttpContext.Session.SetInt32("UserId", id);
        return Content("Session đã được thiết lập!");
    }

    [Route("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["icon"] = "success";
        TempData["Message"] = "Đăng xuất thành công!";
        return RedirectToAction("Index", "Home");
    }
}
