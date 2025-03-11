using BTL_NhapMonCNPM.Models;
using Microsoft.EntityFrameworkCore;

public class BaiHatRepository : Repository<Baihat>, IBaiHatRepository
{
    private readonly ApplicationDbContext _context;

    public BaiHatRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Baihat> GetBaihatAsync(int id)
    {
        return await _context.BaiHats.FirstOrDefaultAsync(t => t.Id == id);
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

    public async Task<List<Baihat>> SearchBaiHatAsync(string keyword, int limit = 0)
    {
        try
        {
            keyword = keyword.ToLower().Trim();

            var query = _context.BaiHats
                .Where(b => b.tenbaihat.ToLower().Contains(keyword));

            if (limit > 0)
            {
                query = query.Take(limit);
            }

            var results = await query.ToListAsync();

            return results;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Có lỗi khi tìm kiếm bài hát: " + ex.Message);
            return new List<Baihat>();
        }
    }

    public async Task<int> CountSearchResultsAsync(string keyword)
    {
        try
        {
            keyword = keyword.ToLower().Trim();

            return await _context.BaiHats
                .Where(b => b.tenbaihat.ToLower().Contains(keyword))
                .CountAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Có lỗi khi tìm kiếm bài hát: " + ex.Message);
            return 0;
        }
    }
}
