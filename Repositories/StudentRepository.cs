using DataStructuresFinalPierce.Models;

namespace DataStructuresFinalPierce.Repositories
{
    public class StudentRepository
    {
        public string CsvFilePath = "students.csv";
        private static List<Student> Students = new List<Student>();

        public StudentRepository()
        {
            LoadDataFromCsv();
        }

        public void LoadDataFromCsv()
        {
            if (File.Exists(CsvFilePath))
            {
                Students.Clear();
                var lines = File.ReadAllLines(CsvFilePath);
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    var student = new Student
                    {
                        StudentId = int.Parse(values[0]),
                        FirstName = values[1],
                        LastName = values[2]
                    };
                    Students.Add(student);
                }
            }
            Students = Students.OrderBy(s => s.StudentId).ToList();
        }

        public void SaveDataToCsv()
        {
            var csvLines = new List<string> { "StudentId,FirstName,LastName" };

            foreach (var student in Students)
            {
                var line = $"{student.StudentId},{student.FirstName},{student.LastName}";
                csvLines.Add(line);
            }

            // Clear the CSV file
            File.WriteAllText(CsvFilePath, string.Empty);

            // Write the updated students list to the CSV file
            File.WriteAllLines(CsvFilePath, csvLines);

            // Reload data from the CSV file
            LoadDataFromCsv();
        }

        public void Add(Student student)
        {
            Students.Add(student);
            SaveDataToCsv();
        }

        public List<Student> GetAllStudents()
        {
            return Students;
        }
        public Student GetById(int studentId)
        {
            return Students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public void Update(int existingStudentId, Student updatedStudent)
        {
            Student existingStudent = Students.FirstOrDefault(s => s.StudentId == existingStudentId);
            Students.Remove(existingStudent);
            if (existingStudent != null)
            {
                existingStudent = updatedStudent;
                Students.Add(existingStudent);
                SaveDataToCsv();
            }
            else
            {
                throw new InvalidOperationException("Student not found for update.");
            }
        }
        public void Delete(int studentId)
        {
            Student studentToRemove = Students.FirstOrDefault(s => s.StudentId == studentId);

            if (studentToRemove != null)
            {
                Students.Remove(studentToRemove);
                SaveDataToCsv();
            }
            else
            {
                throw new InvalidOperationException("Student not found for deletion.");
            }
        }
        public Student GetLastStudentId()
        {
            var lastStudent = Students.OrderByDescending(s => s.StudentId).FirstOrDefault();

            return lastStudent;
        }

        public int GetNextStudentId()
        {
            if (Students.Count == 0)
            {
                return 0;
            }
            else
            {
                return GetLastStudentId().StudentId + 1;
            }
        }
    }
}
