using BTL_NhapMonCNPM.Models;
using Microsoft.EntityFrameworkCore;

public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
{
    private readonly ApplicationDbContext _context;

    public PlaylistRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Playlist>> GetPlaylistByUser(int id)
    {
        return await _context.Playlists.Where(p => p.FK_nguoitao == id).ToListAsync();
    }

    public async Task<bool> AddNewPlaylist(Playlist playlist)
    {
        try
        {
            var existingSong = await _context.Playlists.FirstOrDefaultAsync(t => t.tenPlaylist == playlist.tenPlaylist && t.FK_nguoitao == playlist.FK_nguoitao);
            if (existingSong != null)
            {
                return false; 
            }

            await _context.Playlists.AddAsync(playlist);
            await _context.SaveChangesAsync();
            return true; // Thêm thành công
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding new Playlist: {ex.Message}");
            return false; // Thêm thất bại
        }
    }
}
