using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackApp.Models;

namespace StackApp.ViewComponents
{
    public class CommentComponent: ViewComponent
    {
        private readonly StackAppDbContext _db;
        public CommentComponent(StackAppDbContext context)
        {
            _db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            var comments = await GetCommentsAsync(postId);
            //return View(comments);
            return View();
        }

        private Task<List<Comment>> GetCommentsAsync(int id)
        {
            return _db.Comments.Include(comments => comments.Post).Include(comments => comments.User).Where(comment => comment.Post.Id == id).ToListAsync();
        }
    }
}
