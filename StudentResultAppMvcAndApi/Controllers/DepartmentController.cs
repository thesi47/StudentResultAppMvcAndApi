using Microsoft.AspNetCore.Mvc;
using StudentResultAppMvcAndApi.Data;

namespace StudentResultAppMvcAndApi.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
