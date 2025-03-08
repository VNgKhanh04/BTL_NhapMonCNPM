
using System.ComponentModel.DataAnnotations;
namespace BTL_NhapMonCNPM.Models;

public class Theloai {
    public int Id { get; set; } 
    
    [Required(ErrorMessage = "Tên thể loại không được để trống")]
    public string tentheloai { get; set; } 
    public bool Deleted { get; set; } 
}