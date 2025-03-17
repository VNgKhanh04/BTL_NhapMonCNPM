public class Playlist
{
    public int Id { get; set; }

    public string TenPlaylist { get; set; } = string.Empty; 

    public bool Deleted { get; set; } = false; 

    public List<PlaylistSong> PlaylistSongs { get; set; } = new();
}
