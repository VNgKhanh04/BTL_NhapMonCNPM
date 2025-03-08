using System.ComponentModel.DataAnnotations;

namespace BTL_NhapMonCNPM.Models;
public class Playlist_baihat
{
    public int Id { get; set; }
    public int FK_Playlist { get; set; }
    public int FK_Baihat { get; set; }
    public bool Deleted { get; set; }
}