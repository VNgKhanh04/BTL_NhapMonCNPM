public class Song
{
    public int Id { get; set; }
    public string TenBaihat { get; set; }
    public string FileUpload { get; set; } 
    public DateTime ThoiGianUp { get; set; }
    public bool Deleted { get; set; } = false; 
    public List<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
}