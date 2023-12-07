using System.Collections.Generic;

namespace DataStructuresFinalPierce.Models
{
    public class CourseDetailViewModel
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public List<Student> StudentListView { get; set; }

        public CourseDetailViewModel()
        {
            StudentListView = new List<Student>();
        }
    }
}
