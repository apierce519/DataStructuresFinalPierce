using DataStructuresFinalPierce.Models;
using DataStructuresFinalPierce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DataStructuresFinalPierce.Controllers
{
    public class StudentController : Controller
    {
        private readonly CourseRepository _courseRepository;
        //private readonly AttendanceRepository _attendanceRepository;
        private readonly StudentRepository _studentRepository;

        public StudentController(CourseRepository courseRepository, StudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult AllStudents()
        {
            var model = _studentRepository.GetAllStudents();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new Student();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Student model)
        {
            if (ModelState.IsValid)
            {
                Student student = model;

                _studentRepository.Add(student);

                return RedirectToAction(nameof(AllStudents));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(int studentId, string firstName, string lastName)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the validation errors");
                return View(); // Return the view without trying to retrieve the entity
            }

            var existingStudent = _studentRepository.GetById(studentId);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FirstName = firstName;
            existingStudent.LastName = lastName;

            if (ModelState.IsValid)
            {
                _studentRepository.Update(studentId, existingStudent);
                return RedirectToAction(nameof(AllStudents));
            }
            else
            {
                ModelState.AddModelError("", "Please fix the validation errors");
            }

            return View(existingStudent);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int studentId)
        {
            try
            {
                _studentRepository.Delete(studentId);
                TempData["SuccessMessage"] = "Student deletion succeeded successfully.";
                return RedirectToAction(nameof(AllStudents));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return RedirectToAction(nameof(AllStudents));
            }
        }
    }
}

