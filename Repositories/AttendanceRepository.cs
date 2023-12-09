using DataStructuresFinalPierce.Models;

namespace DataStructuresFinalPierce.Repositories
{
    public class AttendanceRepository
    {

        public string CsvFilePath = "attendances.csv";
        private static List<Attendance> Attendances = new List<AttendanceRepository>();

        public AttendanceRepository()
        {
            LoadDataFromCsv();
        }

        public void LoadDataFromCsv()
        {
            Attendances.Clear();
            if (File.Exists(CsvFilePath))
            {
                Attendances.Clear();
                var lines = File.ReadAllLines(CsvFilePath);
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    var attendance = new Attendance
                    {
                        AttendanceId = int.Parse(values[0]),
                        CourseId = values[1],

                    };
                }
            }
        }
    }
}
