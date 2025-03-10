using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BTL_NhapMonCNPM.Models;

namespace BTL_NhapMonCNPM.Data{
    public class CommentRepository : ICommentRepository{
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context){
            _context = context;
        }
        public async Task<List<Comment>> GetComments(){
            return await _context.Comments
                .Include(c => c.User) 
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> AddComment(Comment comment){
            try
            {
                if (string.IsNullOrWhiteSpace(comment.Content))
                {
                    throw new ArgumentException("Nội dung bình luận không được để trống");
                }

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm bình luận: " + ex.Message);
                return false;
            }
        }
    }
}