using BTL_NhapMonCNPM.Controllers;
using BTL_NhapMonCNPM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BTL_NhapMonCNPM.Tests.Controllers
{
    public class AuthControllerLogoutTests
    {
        private readonly AuthController _controller;
        private readonly MockHttpSession _session;
        private readonly TempDataDictionary _tempData;

        public AuthControllerLogoutTests()
        {
            var mockRepository = new Mock<ITaiKhoanRepository>();
            
            _controller = new AuthController(mockRepository.Object);
            
            var httpContext = new DefaultHttpContext();
            _session = new MockHttpSession();
            httpContext.Session = _session;
            
            var tempDataProvider = Mock.Of<ITempDataProvider>();
            _tempData = new TempDataDictionary(httpContext, tempDataProvider);
            
            _controller.TempData = _tempData;
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            
            // Thêm dữ liệu vào session để test logout
            _session.Set("UserName", System.Text.Encoding.UTF8.GetBytes("testuser"));
            _session.Set("UserQuyen", System.Text.Encoding.UTF8.GetBytes("user"));
            _session.Set("UserId", BitConverter.GetBytes(1));
        }

        // [Fact]
        // public void Logout_ClearsSessionAndRedirects()
        // {
        //     // Verify session has data before logout
        //     Assert.True(_session.Keys.Any());
            
        //     // Act
        //     var result = _controller.Logout() as RedirectToActionResult;
            
        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.Equal("Index", result.ActionName);
        //     Assert.Equal("Home", result.ControllerName);
        //     Assert.Equal("success", _tempData["icon"]);
        //     Assert.Equal("Đăng xuất thành công!", _tempData["Message"]);
            
        //     // Verify session is empty after logout
        //     Assert.Empty(_session.Keys);
        // }
    }

    // MockHttpSession từ phần trước
}