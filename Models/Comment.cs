using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_NhapMonCNPM.Models{
     public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nội dung bình luận không được để trống")]
        [StringLength(500, ErrorMessage = "Bình luận không được vượt quá 500 ký tự")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ?UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}