using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StackApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StackApp.Controllers
{
    public class PostController : Controller
    {
        private readonly StackAppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(UserManager<ApplicationUser> userManager, StackAppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Questions.ToList());
        }

        public IActionResult Detail(int id)
        {
            var thisPost = _db.Questions
                .Include(posts => posts.Answers)
                .ThenInclude(answers => answers.User)
                .FirstOrDefault(posts => posts.Id == id);
            return View(thisPost);
        }

        [Authorize]
        public IActionResult CreateQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(Question question)
        {
            var currentQuestion = question;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            question.User = currentUser;
            _db.Questions.Add(question);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult CreateAnswer(int id)
        {
            var parentQuestion = _db.Questions.Include(questions => questions.User).FirstOrDefault(posts => posts.Id == id);
            return View(new Answer { Question = parentQuestion });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer(Answer answer)
        {
            var parentQuestion = _db.Questions.FirstOrDefault(questions => questions.Id == answer.Question.Id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            answer.Question = parentQuestion;
            var currentUser = await _userManager.FindByIdAsync(userId);
            answer.User = currentUser;
            answer.Id = 0;
            _db.Answers.Add(answer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
