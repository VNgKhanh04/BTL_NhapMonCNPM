using BTL_NhapMonCNPM.Models;

public interface IBaiHatRepository : IRepository<Baihat>
{
    Task<Baihat> GetBaihatAsync(string msname);
    Task<bool> AddNewBaiHat(Baihat baihat);
    Task<List<Baihat>> GetBaiHatByUserAsync(int userId);
    Task<List<Baihat>> GetAllBaiHat();
}
