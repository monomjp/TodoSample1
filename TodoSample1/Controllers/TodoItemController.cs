using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using TodoSample1.Data;
using TodoSample1.Models;

namespace TodoSample1.Controllers
{
    [Authorize]
    public class TodoItemController : Controller
    {
        private readonly TodoContext _todoContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoItemController(TodoContext context, UserManager<IdentityUser> userManager)
        {
            _todoContext = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var todoItems = _todoContext.TodoItems.Where(i => i.UserId == _userManager.GetUserId(User));
            var model = new ViewModel()
            {
                UserName = _userManager.GetUserName(User),
                Items = todoItems.ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create([FromForm]string todoName)
        {
            var newItem = new TodoItem()
            {
                UserId = _userManager.GetUserId(User),
                Todo = todoName
            };
            _todoContext.TodoItems.Add(newItem);
            _todoContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _todoContext.TodoItems.FirstOrDefault(i => i.Id == id);
            _todoContext.TodoItems.Remove(item);
            _todoContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
