namespace DataStructuresFinalPierce.Repositories
{
    public class CourseRepository
    {
        private static readonly List<Course> Courses = new List<Course>();
        private string CsvFilePath { get; set; } = "courses.csv";

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
                    var studentList = values[2].Split(';').Select(int.Parse).ToList();
                    var course = new Course
                    {
                        CourseId = values[0],
                        Name = values[1],
                        StudentList = studentList
                    };

                    Courses.Add(course);
                }
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

        }
    }
