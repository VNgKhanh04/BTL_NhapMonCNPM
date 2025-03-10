using BTL_NhapMonCNPM.Models;
using Microsoft.EntityFrameworkCore;

public class BinhLuanRepository : Repository<Binhluan>, IBinhLuanRepository
{
    private readonly ApplicationDbContext _context;

    public BinhLuanRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Binhluan>> GetBinhLuanByBaiHatAsync(int baihatId)
    {
        return await _context.BinhLuans.Where(t => t.FK_parent == baihatId).ToListAsync();
    }

    public async Task<bool> AddNewBinhLuan(Binhluan binhluan){
        try
        {
            await _context.BinhLuans.AddAsync(binhluan);
            await _context.SaveChangesAsync();
            return true; // Thêm thành công
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding new cmt: {ex.Message}");
            return false; // Thêm thất bại
        }
    }
}
