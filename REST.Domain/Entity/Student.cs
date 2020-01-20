namespace REST.Domain.Entity
{
    public class Student
    {
        public int Id { get; set; }

        public string Faculty { get; set; }
        public string Course { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        public StatusEnum Status { get; set; }

        public Result<bool> ChangeStudentStatus(StatusEnum status)
        {
            Status = status;
            return Result<bool>.Ok(true);
        }

        public Result<bool> ChangeUserYear(int year)
        {
            Year = year;
            return Result<bool>.Ok(true);
        }
    }
}