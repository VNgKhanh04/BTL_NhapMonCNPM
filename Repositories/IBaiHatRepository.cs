using BTL_NhapMonCNPM.Models;

public interface IBaiHatRepository : IRepository<Baihat>
{
    Task<Baihat> GetBaihatAsync(int id);
    Task<bool> AddNewBaiHat(Baihat baihat);
    Task<List<Baihat>> GetBaiHatByUserAsync(int userId);
    Task<List<Baihat>> GetAllBaiHat();
}
