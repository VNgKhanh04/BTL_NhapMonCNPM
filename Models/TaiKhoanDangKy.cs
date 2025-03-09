namespace BTL_NhapMonCNPM.Models
{
    public class TaiKhoanDangKy
    {
        public int Id { get; set; }
        public string TenTaiKhoan { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Quyen { get; set; } = "User"; // Mặc định là User
        public bool Deleted { get; set; } = false;
    }
}
