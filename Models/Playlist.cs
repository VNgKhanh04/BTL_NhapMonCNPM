using System.ComponentModel.DataAnnotations;

namespace BTL_NhapMonCNPM.Models;
public class Playlist
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên play list không được để trống")]
    public string tenPlaylist { get; set; }

    public bool Deleted { get; set; }
}