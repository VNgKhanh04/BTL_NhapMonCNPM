using System.Collections.Generic;
using System.Threading.Tasks;
using BTL_NhapMonCNPM.Models;

namespace BTL_NhapMonCNPM.Data{
    public interface ICommentRepository{
        Task<List<Comment>> GetComments();
        Task<bool> AddComment(Comment comment);
    }
}