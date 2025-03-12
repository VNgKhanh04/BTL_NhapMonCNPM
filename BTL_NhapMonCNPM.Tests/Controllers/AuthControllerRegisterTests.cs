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
    public class AuthControllerRegisterTests
    {
        private readonly Mock<ITaiKhoanRepository> _mockRepository;
        private readonly AuthController _controller;
        private readonly TempDataDictionary _tempData;

        public AuthControllerRegisterTests()
        {
            _mockRepository = new Mock<ITaiKhoanRepository>();
            
            _controller = new AuthController(_mockRepository.Object);
            var httpContext = new DefaultHttpContext();
            var tempDataProvider = Mock.Of<ITempDataProvider>();
            _tempData = new TempDataDictionary(httpContext, tempDataProvider);
            _controller.TempData = _tempData;
        }

        [Fact]
        public void RegisterPost_EmptyUsername_ReturnsRedirectWithError()
        {
            // Arrange
            string username = "";
            string password = "password123";
            string email = "test@example.com";

            // Act
            var result = _controller.RegisterPost(username, password, email) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("error", _tempData["icon"]);
            Assert.Equal("Vui lòng nhập đầy đủ thông tin!", _tempData["Message"]);
        }

        [Fact]
        public void RegisterPost_EmptyPassword_ReturnsRedirectWithError()
        {
            // Arrange
            string username = "newuser";
            string password = "";
            string email = "test@example.com";

            // Act
            var result = _controller.RegisterPost(username, password, email) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("error", _tempData["icon"]);
            Assert.Equal("Vui lòng nhập đầy đủ thông tin!", _tempData["Message"]);
        }

        [Fact]
        public void RegisterPost_EmptyEmail_ReturnsRedirectWithError()
        {
            // Arrange
            string username = "newuser";
            string password = "password123";
            string email = "";

            // Act
            var result = _controller.RegisterPost(username, password, email) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("error", _tempData["icon"]);
            Assert.Equal("Vui lòng nhập đầy đủ thông tin!", _tempData["Message"]);
        }

        [Fact]
        public void RegisterPost_UsernameExists_ReturnsRedirectWithError()
        {
            // Arrange
            string username = "existinguser";
            string password = "password123";
            string email = "test@example.com";

            var existingAccount = new Taikhoan
            {
                Id = 1,
                TenTaiKhoan = username,
                MatKhau = "somepassword",
                Email = "existing@example.com",
                Quyen = "user"
            };

            _mockRepository.Setup(repo => repo.GetByUsernameAsync(username))
                .ReturnsAsync(existingAccount);

            // Act
            var result = _controller.RegisterPost(username, password, email) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("error", _tempData["icon"]);
            Assert.Equal("Tên tài khoản đã tồn tại!", _tempData["Message"]);
        }

        [Fact]
        public void RegisterPost_ValidData_ReturnsRedirectWithSuccess()
        {
            // Arrange
            string username = "newuser";
            string password = "password123";
            string email = "test@example.com";

            // Username không tồn tại
            _mockRepository.Setup(repo => repo.GetByUsernameAsync(username))
                .ReturnsAsync((Taikhoan)null);

            // Đăng ký thành công
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Taikhoan>()))
                .ReturnsAsync(true);

            // Act
            var result = _controller.RegisterPost(username, password, email) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("success", _tempData["icon"]);
            Assert.Equal("Đăng ký thành công!", _tempData["Message"]);
            
            // Verify repository was called with correct parameters
            _mockRepository.Verify(repo => repo.CreateAsync(It.Is<Taikhoan>(tk => 
                tk.TenTaiKhoan == username && 
                tk.MatKhau == password && 
                tk.Email == email &&
                tk.Quyen == "user"
            )), Times.Once);
        }

        // [Fact]
        // public void RegisterPost_RepositoryError_ReturnsRedirectWithError()
        // {
        //     // Arrange
        //     string username = "newuser";
        //     string password = "password123";
        //     string email = "test@example.com";

        //     // Username không tồn tại
        //     _mockRepository.Setup(repo => repo.GetByUsernameAsync(username))
        //         .ReturnsAsync((Taikhoan)null);

        //     // Đăng ký không thành công
        //     _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Taikhoan>()))
        //         .ReturnsAsync(false);

        //     // Act
        //     var result = _controller.RegisterPost(username, password, email) as RedirectToActionResult;

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.Equal("Index", result.ActionName);
        //     Assert.Equal("Home", result.ControllerName);
        //     Assert.Equal("error", _tempData["icon"]);
        //     Assert.Equal("Đăng ký không thành công!", _tempData["Message"]);
        // }

        // [Fact]
        // public void RegisterPost_Exception_ReturnsRedirectWithError()
        // {
        //     // Arrange
        //     string username = "newuser";
        //     string password = "password123";
        //     string email = "test@example.com";

        //     // Username không tồn tại
        //     _mockRepository.Setup(repo => repo.GetByUsernameAsync(username))
        //         .ReturnsAsync((Taikhoan)null);

        //     // Đăng ký gây ra exception
        //     _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Taikhoan>()))
        //         .ThrowsAsync(new Exception("Database error"));

        //     // Act
        //     var result = _controller.RegisterPost(username, password, email) as RedirectToActionResult;

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.Equal("Index", result.ActionName);
        //     Assert.Equal("Home", result.ControllerName);
        //     Assert.Equal("error", _tempData["icon"]);
        //     Assert.Equal("Đăng ký không thành công!", _tempData["Message"]);
        // }
    }
}