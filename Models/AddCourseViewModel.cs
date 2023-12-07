namespace DataStructuresFinalPierce.Models
{
    public class AddCourseViewModel
    {
        // Properties
        public string CourseId { get; set; }

        public string Name { get; set; }

        public List<int> StudentsList { get; set; } = new List<int>();
    }
}
