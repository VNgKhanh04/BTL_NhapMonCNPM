using System.ComponentModel.DataAnnotations;

namespace BTL_NhapMonCNPM.Models;
public class Binhluan
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nội dung không được để trống")]
    public string noidung { get; set; }
    public int FK_nguoiviet { get; set; }
    public DateTime thoigian { get; set; }
    public int FK_parent { get; set; }
    public bool Deleted { get; set; }

    public string Username { get; set; }

}