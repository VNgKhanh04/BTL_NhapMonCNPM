public class PlaylistSong
{
    public int Id { get; set; }
    public int FK_Playlist { get; set; }
    public Playlist Playlist { get; set; }
    public int FK_Baihat { get; set; }
    public Song Song { get; set; }
    public bool Deleted { get; set; } = false; 
}