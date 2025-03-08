using BTL_NhapMonCNPM.Models;
using Microsoft.EntityFrameworkCore;

public class TaiKhoanRepository : Repository<Taikhoan>, ITaiKhoanRepository
{
    private readonly ApplicationDbContext _context;

    public TaiKhoanRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Taikhoan> GetByUsernameAsync(string username)
    {
        return await _context.TaiKhoans.FirstOrDefaultAsync(t => t.TenTaiKhoan == username);
    }

    public async Task<Taikhoan> AuthenticateAsync(string username, string password)
    {
        var taiKhoan = await _context.TaiKhoans.FirstOrDefaultAsync(t => t.TenTaiKhoan == username);
    
    if (taiKhoan != null && taiKhoan.MatKhau == password)
    {
        return taiKhoan;
    }
    
    return null;
    }

    public async Task<bool> CreateAsync(Taikhoan taiKhoan)
    {
        try
        {
            await _context.TaiKhoans.AddAsync(taiKhoan);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
