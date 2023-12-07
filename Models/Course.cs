namespace DataStructuresFinalPierce.Models
{
    public class Course
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public List<int> StudentList { get; set; }
        public Course()
        {
            StudentList = new List<int>();
        }

    }
}
