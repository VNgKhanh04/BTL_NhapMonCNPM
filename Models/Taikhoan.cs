using System.ComponentModel.DataAnnotations;

namespace BTL_NhapMonCNPM.Models;
public class Taikhoan
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên tài khoản không được để trống")]
    public string TenTaiKhoan { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
    public string MatKhau { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Quyền không được để trống")]
    public string Quyen { get; set; }

    public bool Deleted { get; set; }
}