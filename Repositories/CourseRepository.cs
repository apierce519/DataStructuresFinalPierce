using DataStructuresFinalPierce.Models;

namespace DataStructuresFinalPierce.Repositories
{
    public class CourseRepository
    {
        private static List<Course> Courses = new List<Course>();
        public string CsvFilePath { get; set; } = "courses.csv";

        public CourseRepository()
        {
            LoadDataFromCsv();
        }
        private void LoadDataFromCsv()
        {
            if (File.Exists(CsvFilePath))
            {
                Courses.Clear();
                var lines = File.ReadAllLines(CsvFilePath);
                foreach (var line in lines.Skip(1)) // Skip header
                {
                    var values = line.Split(',');
                    var studentList = new List<int>();
                    if (values[2].Equals("") || values[2] != null)
                    { studentList = values[2].Split(';').Select(int.Parse).ToList(); }
                    var course = new Course
                    {
                        CourseId = values[0],
                        Name = values[1],
                        StudentList = studentList
                    };

                    Courses.Add(course);
                }
                Courses = Courses.OrderBy(c => c.CourseId).ToList();
            }
        }

        private void SaveDataToCsv()
        {
            var csvLines = new List<string> { "CourseId,Name,StudentsList" }; // Add other headers

            foreach (var course in Courses)
            {
                var students = course.StudentList != null ? string.Join(";", course.StudentList) : "";
                var line = $"{course.CourseId},{course.Name},{students}";
                csvLines.Add(line);
            }
            // Clear the CSV file
            File.WriteAllText(CsvFilePath, string.Empty);

            // Write the updated students list to the CSV file
            File.WriteAllLines(CsvFilePath, csvLines);

            LoadDataFromCsv();
        }
        public Course GetById(string courseId)
        {
            return Courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public List<Course> GetAllCourses()
        {
            return Courses;
        }
        public void AddCourse(Course course)
        {
            Courses.Add(course);
            SaveDataToCsv();
        }
        public void UpdateCourse(string existingCourseId, Course updatedCourse)
        {
            Course existingCourse = Courses.FirstOrDefault(c => c.CourseId == existingCourseId);
            if (existingCourse != null)
            {
                // Remove the existing course
                Courses.Remove(existingCourse);

                // Update the properties, including CourseId
                existingCourse.CourseId = updatedCourse.CourseId;
                existingCourse.Name = updatedCourse.Name;
                existingCourse.StudentList = updatedCourse.StudentList;

                // Add the updated course back
                Courses.Add(existingCourse);

                SaveDataToCsv();
            }
            else
            {
                throw new InvalidOperationException("Course not found for update.");
            }
        }
        public void DeleteCourse(string courseId)
        {
            Course courseToRemove = Courses.FirstOrDefault(c => c.CourseId == courseId);

            if (courseToRemove != null)
            {
                Courses.Remove(courseToRemove);
                SaveDataToCsv();
            }
            else
            {
                throw new InvalidOperationException("Course not found for deletion.");
            }
        }

    }
}