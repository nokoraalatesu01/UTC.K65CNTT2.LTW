
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TH_Lab01_QLSV.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.IO;

namespace TH_Lab01_QLSV.Controllers
{
    [Route("Admin/Student")]
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>();

        private readonly IWebHostEnvironment _environment;

        public StudentController(IWebHostEnvironment environment)
        {
            _environment = environment;

            if (listStudents.Count == 0)
            {
                listStudents = new List<Student>()
                {
                    new Student()
                    {
                        Id = 101,
                        ImagePath = "images/students/b1.png",
                        Name = "Hải Nam",
                        Branch = Branch.IT,
                        Gender = Gender.Male,
                        IsRegular = true,
                        Address = "A1-2018",
                        Email = "nam@g.com"
                    },
                    new Student()
                    {
                        Id = 102,
                        ImagePath = "images/students/b2.png",
                        Name = "Minh Tú",
                        Branch = Branch.BE,
                        Gender = Gender.Female,
                        IsRegular = true,
                        Address = "A1-2019",
                        Email = "tu@g.com"
                    },
                    new Student()
                    {
                        Id = 103,
                        ImagePath = "images/students/b3.png",
                        Name = "Hoàng Phong",
                        Branch = Branch.CE,
                        Gender = Gender.Male,
                        IsRegular = false,
                        Address = "A1-2020",
                        Email = "phong@g.com"
                    },
                    new Student()
                    {
                        Id = 104,
                        ImagePath = "images/students/b4.png",
                        Name = "Xuân Mai",
                        Branch = Branch.EE,
                        Gender = Gender.Female,
                        IsRegular = false,
                        Address = "A1-2021",
                        Email = "mai@g.com"
                    }
                };
            }
        }

        [HttpGet("List")]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet("Add")]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .ToList();

            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "0" },
                new SelectListItem { Text = "BE", Value = "1" },
                new SelectListItem { Text = "CE", Value = "2" },
                new SelectListItem { Text = "EE", Value = "3" }
            };

            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Create(Student s, IFormFile? imageFile)
        {
            if (listStudents.Count == 0)
            {
                s.Id = 101;
            }
            else
            {
                s.Id = listStudents.Max(x => x.Id) + 1;
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

                if (!allowedExtensions.Contains(extension))
                {
                    return BadRequest("Chỉ chấp nhận ảnh JPG, JPEG, PNG hoặc WEBP.");
                }

                string folder = Path.Combine(
                    _environment.WebRootPath,
                    "images",
                    "students"
                );

                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                s.ImagePath = "images/students/" + fileName;
            }

            listStudents.Add(s);

            return RedirectToAction(nameof(Index));
        }
    }
}
