using DataStructuresFinalPierce.Models;
using DataStructuresFinalPierce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DataStructuresFinalPierce.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseRepository _courseRepository;
        //private readonly AttendanceRepository _attendanceRepository;
        private readonly StudentRepository _studentRepository;

        public CourseController(CourseRepository courseRepository, StudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult AllCourses()
        {
            var model = _courseRepository.GetAllCourses();
            return View(model);
        }
        public IActionResult Detail(string courseId)
        {

            var course = _courseRepository.GetById(courseId);
            if (course != null)
            {
                var studentList = _studentRepository.GetAllStudents().Where(student => course.StudentList.Contains(student.StudentId)).ToList();
                var model = new CourseDetailViewModel
                {
                    CourseId = course.CourseId,
                    Name = course.Name,
                    StudentListView = studentList
                };
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddCourseViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    CourseId = model.CourseId,
                    Name = model.Name,
                    StudentList = model.StudentsList
                };

                _courseRepository.AddCourse(course);

                // Redirect to the action that displays the list or details of courses
                return RedirectToAction(nameof(AllCourses));
            }

            // If ModelState is not valid, return to the Add view to display validation errors
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var course = _courseRepository.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(string CourseId, string Name, List<int> StudentList)
        {
            // Assuming you have a method to get the course from the repository
            var existingCourse = _courseRepository.GetById(CourseId);

            if (existingCourse == null)
            {
                return NotFound();
            }

            // Update properties of existingCourse with the values from the form
            existingCourse.Name = Name;
            existingCourse.StudentList = StudentList;

            if (ModelState.IsValid)
            {
                _courseRepository.UpdateCourse(CourseId, existingCourse);
                return RedirectToAction(nameof(AllCourses));
            }
            else
            {
                ModelState.AddModelError("", "Please fix the validation errors.");
            }

            return View(existingCourse);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var course = _courseRepository.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed([Bind("CourseId")] Course course)
        {
            try
            {
                _courseRepository.DeleteCourse(course.CourseId);
                TempData["SuccessMessage"] = "Course deleted successfully!";
                return RedirectToAction(nameof(AllCourses));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return RedirectToAction(nameof(AllCourses));
            }
        }


    }
}
