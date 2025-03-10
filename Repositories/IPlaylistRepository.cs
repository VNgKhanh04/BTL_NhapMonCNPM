using BTL_NhapMonCNPM.Models;

public interface IPlaylistRepository : IRepository<Playlist>
{
    Task<List<Playlist>> GetPlaylistByUser(int id);
    Task<bool> AddNewPlaylist(Playlist playlist);
}