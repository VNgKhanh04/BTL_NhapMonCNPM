using BTL_NhapMonCNPM.Models;

public interface IBinhLuanRepository : IRepository<Binhluan>
{
    Task<List<Binhluan>> GetBinhLuanByBaiHatAsync(int baihatId);
    Task<bool> AddNewBinhLuan(Binhluan binhluan);
}
