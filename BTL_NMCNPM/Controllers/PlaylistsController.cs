using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class PlaylistsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PlaylistsController(ApplicationDbContext context)
    {
        _context = context;
    }

        public async Task<IActionResult> Index()
    {
        var playlists = await _context.Playlists
            .Where(p => !p.Deleted)
            .ToListAsync();
        return View(playlists);
    }

   
    public IActionResult Create()
    {
        return View();
    }

   
    [HttpPost]
    public async Task<IActionResult> Create(string tenPlaylist)
    {
        if (string.IsNullOrEmpty(tenPlaylist))
        {
            ModelState.AddModelError("", "Tên playlist không được để trống.");
            return View();
        }

        
        bool exists = await _context.Playlists.AnyAsync(p => p.TenPlaylist == tenPlaylist && !p.Deleted);
        if (exists)
        {
            ModelState.AddModelError("", "Tên playlist đã tồn tại. Vui lòng nhập tên khác.");
            return View();
        }

        var playlist = new Playlist { TenPlaylist = tenPlaylist };
        _context.Playlists.Add(playlist);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    
    public async Task<IActionResult> Details(int id)
    {
        var playlist = await _context.Playlists
            .Include(p => p.PlaylistSongs)
            .ThenInclude(ps => ps.Song)
            .FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);

        if (playlist == null) return NotFound();

        ViewBag.AvailableSongs = await _context.Songs
            .Where(s => !s.Deleted)
            .ToListAsync();

        return View(playlist);
    }

    
    [HttpPost]
public async Task<IActionResult> AddSong(int playlistId, int songId)
{
    var playlist = await _context.Playlists
        .Include(p => p.PlaylistSongs)
        .FirstOrDefaultAsync(p => p.Id == playlistId);

    var song = await _context.Songs.FindAsync(songId);

    if (playlist == null || song == null) 
        return NotFound();

   
    bool songExists = playlist.PlaylistSongs.Any(ps => ps.FK_Baihat == songId);
    if (songExists)
    {
        TempData["ErrorMessage"] = "Bài hát đã có trong playlist!";
        return RedirectToAction("Details", new { id = playlistId });
    }

    var playlistSong = new PlaylistSong { FK_Playlist = playlistId, FK_Baihat = songId };
    _context.PlaylistSongs.Add(playlistSong);
    await _context.SaveChangesAsync();

    return RedirectToAction("Details", new { id = playlistId });
}


   
    [HttpPost]
    public async Task<IActionResult> RemoveSong(int playlistSongId)
    {
        var playlistSong = await _context.PlaylistSongs.FindAsync(playlistSongId);
        if (playlistSong == null) return NotFound();

        _context.PlaylistSongs.Remove(playlistSong);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", new { id = playlistSong.FK_Playlist });
    }

    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var playlist = await _context.Playlists.FindAsync(id);
        if (playlist == null) return NotFound();

        playlist.Deleted = true;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
