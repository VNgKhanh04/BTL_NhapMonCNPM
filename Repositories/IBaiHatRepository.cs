using BTL_NhapMonCNPM.Models;

public interface IBaiHatRepository : IRepository<Baihat>
{
    Task<Baihat> GetBaihatAsync(int id);
    Task<bool> AddNewBaiHat(Baihat baihat);
    Task<List<Baihat>> GetBaiHatByUserAsync(int userId);
    Task<List<Baihat>> GetAllBaiHat();

    Task<List<Baihat>> SearchBaiHatAsync(string keyword, int limit = 0);
    Task<int> CountSearchResultsAsync(string keyword);
}
