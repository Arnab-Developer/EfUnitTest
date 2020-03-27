using EfUnitTest.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EfUnitTest.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _studentContext;

        public StudentController(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        public IActionResult Index()
        {
            var students = _studentContext.Students
                .OrderBy(student => student.Id)
                .ToList();
            return View(students);
        }
    }
}
