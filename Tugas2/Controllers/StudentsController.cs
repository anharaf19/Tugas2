using Microsoft.AspNetCore.Mvc;
using Tugas2.Models;
using Tugas2.Services;

namespace Tugas2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudent _student;
        public StudentsController(IStudent student)
        {
            _student = student;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            //IEnumerable<Student> model;

            var model = await _student.GetAll();

            return View(model);
            //var results = await _student.GetAll();
            //string strResult = string.Empty;
            //foreach(var result in results)
            //{
            //    strResult = result.FirstMidName + "\n";
            //}
            //return Content(strResult);
          
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _student.GetById(id);
            return View(model);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                var students = await _student.Insert(student);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data students {student.FirstMidName}</div>";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }

        }
        public async Task<IActionResult> Update(int id)
        {
            var model = await _student.GetById(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            try
            {
                var result = await _student.Update(student);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mengupdate data student {result.FirstMidName}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _student.GetById(id);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _student.Delete(id);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mendelete data student id: {id}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
