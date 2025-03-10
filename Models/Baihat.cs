
using System.ComponentModel.DataAnnotations;
namespace BTL_NhapMonCNPM.Models;

public class Baihat {
    public int Id { get; set; } 
    
    [Required(ErrorMessage = "Tên bài hát không được để trống")]
    public string tenbaihat { get; set; } 
    
    [Required(ErrorMessage = "Thể loại không được để trống")]
    public int FK_theloai { get; set; } 
    
    [Required(ErrorMessage = "File không được để trống")]
    public string file { get; set; } 
    
    [Required(ErrorMessage = "Thời gian upload không được để trống")]
    public DateTime thoigian_up { get; set; } 
    
    [Required(ErrorMessage = "Thông tin người upload không được để trống")]
    public int FK_nguoiup { get; set; } 
    
    public bool Deleted { get; set; } 
}