using BTL_NhapMonCNPM.Models;

public interface ITaiKhoanRepository : IRepository<Taikhoan>
{
    Task<Taikhoan> GetByUsernameAsync(string username);
    Task<Taikhoan> AuthenticateAsync(string username, string password);
    Task<bool> CreateAsync(Taikhoan taiKhoan);
}
