using BTL_NhapMonCNPM.Models;
using Microsoft.EntityFrameworkCore;

public class BaiHatRepository : Repository<Baihat>, IBaiHatRepository
{
    private readonly ApplicationDbContext _context;

    public BaiHatRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Baihat> GetBaihatAsync(string msname)
    {
        return await _context.BaiHats.FirstOrDefaultAsync(t => t.tenbaihat == msname);
    }

    public async Task<bool> AddNewBaiHat(Baihat baihat)
    {
        try
        {
            var existingSong = await _context.BaiHats.FirstOrDefaultAsync(t => t.tenbaihat == baihat.tenbaihat && t.FK_nguoiup == baihat.FK_nguoiup);
            if (existingSong != null)
            {
                return false; 
            }

            await _context.BaiHats.AddAsync(baihat);
            await _context.SaveChangesAsync();
            return true; // Thêm thành công
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding new song: {ex.Message}");
            return false; // Thêm thất bại
        }
    }

    public async Task<List<Baihat>> GetBaiHatByUserAsync(int userId)
    {
        return await _context.BaiHats.Where(t => t.FK_nguoiup == userId).ToListAsync();
    }

    public async Task<List<Baihat>> GetAllBaiHat()
    {
        return await _context.BaiHats.ToListAsync();
    }
}
