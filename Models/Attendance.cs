namespace DataStructuresFinalPierce.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public string CourseId { get; set; }
        public List<int> StudentId { get; set; }
        public List<bool> StudentAttendance { get; set; }
        public DateOnly Date { get; set; }
        public bool IsOpen { get; set; }
        public Attendance()
        {
            //Date = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
