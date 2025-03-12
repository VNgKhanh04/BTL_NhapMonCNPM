using System;
using System.Threading.Tasks;
using BTL_NhapMonCNPM.Controllers;
using BTL_NhapMonCNPM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace BTL_NhapMonCNPM.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<ITaiKhoanRepository> _mockRepository;
        private readonly AuthController _controller;
        private readonly DefaultHttpContext _httpContext;
        private readonly TempDataDictionary _tempData;

        public AuthControllerTests()
        {
            // Khởi tạo mock repository
            _mockRepository = new Mock<ITaiKhoanRepository>();
            
            // Khởi tạo controller
            _controller = new AuthController(_mockRepository.Object);
            
            // Setup HttpContext cho session và TempData
            _httpContext = new DefaultHttpContext();
            _httpContext.Session = new MockHttpSession();
            
            var tempDataProvider = Mock.Of<ITempDataProvider>();
            _tempData = new TempDataDictionary(_httpContext, tempDataProvider);
            
            _controller.TempData = _tempData;
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContext
            };
        }

        [Fact]
        public void Auth_EmptyUsername_ReturnsRedirectWithError()
        {
            // Arrange
            string username = "";
            string password = "password123";

            // Act
            var result = _controller.Auth(username, password) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("error", _tempData["icon"]);
            Assert.Equal("Vui lòng nhập đầy đủ thông tin!", _tempData["Message"]);
        }
        
        [Fact]
        public void Auth_EmptyPassword_ReturnsRedirectWithError()
        {
            // Arrange
            string username = "testuser";
            string password = "";

            // Act
            var result = _controller.Auth(username, password) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("error", _tempData["icon"]);
            Assert.Equal("Vui lòng nhập đầy đủ thông tin!", _tempData["Message"]);
        }
        
        [Fact]
        public void Auth_ValidCredentials_ReturnsRedirectWithSuccess()
        {
            // Arrange
            string username = "testuser";
            string password = "password123";
            
            var taiKhoan = new Taikhoan
            {
                Id = 1,
                TenTaiKhoan = username,
                MatKhau = password,
                Quyen = "user"
            };

            _mockRepository
                .Setup(repo => repo.AuthenticateAsync(username, password))
                .ReturnsAsync(taiKhoan);

            // Act
            var result = _controller.Auth(username, password) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("success", _tempData["icon"]);
            Assert.Equal("Đăng nhập thành công!", _tempData["Message"]);
            
            // Kiểm tra session
            Assert.Equal(username, _httpContext.Session.GetString("UserName"));
            Assert.Equal("user", _httpContext.Session.GetString("UserQuyen"));
            Assert.Equal(1, _httpContext.Session.GetInt32("UserId"));
        }
        
        [Fact]
        public void Auth_InvalidCredentials_ReturnsRedirectWithError()
        {
            // Arrange
            string username = "testuser";
            string password = "wrongpassword";

            _mockRepository
                .Setup(repo => repo.AuthenticateAsync(username, password))
                .ReturnsAsync((Taikhoan)null);

            // Act
            var result = _controller.Auth(username, password) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("error", _tempData["icon"]);
            Assert.Equal("Đăng nhập không thành công!", _tempData["Message"]);
        }
        
        // [Fact]
        // public void Auth_ExceptionThrown_ReturnsRedirectWithError()
        // {
        //     // Arrange
        //     string username = "testuser";
        //     string password = "password123";

        //     _mockRepository
        //         .Setup(repo => repo.AuthenticateAsync(username, password))
        //         .ThrowsAsync(new Exception("Database error"));

        //     // Act
        //     var result = _controller.Auth(username, password) as RedirectToActionResult;

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.Equal("Index", result.ActionName);
        //     Assert.Equal("Home", result.ControllerName);
        //     Assert.Equal("error", _tempData["icon"]);
        //     Assert.Equal("Đăng nhập không thành công!", _tempData["Message"]);
        // }
    }
    
    // MockHttpSession để mô phỏng Session
    public class MockHttpSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStore = new Dictionary<string, byte[]>();
        
        public string Id => "TestSession";
        public bool IsAvailable => true;
        public IEnumerable<string> Keys => _sessionStore.Keys;
        
        public void Clear()
        {
            _sessionStore.Clear();
        }
        
        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
        
        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
        
        public void Remove(string key)
        {
            _sessionStore.Remove(key);
        }
        
        public void Set(string key, byte[] value)
        {
            _sessionStore[key] = value;
        }
        
        public bool TryGetValue(string key, out byte[] value)
        {
            return _sessionStore.TryGetValue(key, out value);
        }
    }
}