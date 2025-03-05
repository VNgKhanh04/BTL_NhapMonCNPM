using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BTL_NhapMonCNPM.Data;
using BTL_NhapMonCNPM.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;

namespace BTL_NhapMonCNPM.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentController(ICommentRepository commentRepository, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var comments = await _commentRepository.GetComments();
            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Content))
                return BadRequest("Nội dung bình luận không được để trống!");

            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
                return StatusCode(500, "Session chưa được thiết lập!");

            string? userIdSession = session.GetString("UserId");
            var userId = userIdSession ?? "guest";

            User? userFromDb = null;
            if (userId != "guest")
            {
                userFromDb = _context.Users.FirstOrDefault(u => u.Id == userId);
                if (userFromDb == null)
                    return Unauthorized("Người dùng không tồn tại!");
            }
            else
            {
                userFromDb = _context.Users.FirstOrDefault(u => u.Id == "guest");
                if (userFromDb == null)
                {
                    userFromDb = new User { Id = "guest", UserName = "Guest User", AvatarUrl = "/images/user.png"};
                    _context.Users.Add(userFromDb);
                    await _context.SaveChangesAsync();
                }
            }

            comment.UserId = userFromDb.Id;
            comment.CreatedAt = DateTime.UtcNow;
            comment.User = userFromDb;

            var result = await _commentRepository.AddComment(comment);
            if (!result)
                return StatusCode(500, "Lỗi khi lưu bình luận!");

            var comments = await _commentRepository.GetComments();
            return PartialView("_CommentList", comments);
        }
    }
}
