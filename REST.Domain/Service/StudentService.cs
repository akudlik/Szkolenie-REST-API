using System.Collections.Generic;
using System.Linq;
using REST.Domain.Entity;

namespace REST.Domain.Service
{
    public class StudentService : IStudentService
    {
        private IList<Student> StudentList = new List<Student>
        {
            new Student
            {
                Id = 1,
                Course = "Math",
                Faculty = "Math faculty",
                Semester = 1,
                Status = StatusEnum.Active,
                Year = 2020
            },
            new Student
            {
                Id = 2,
                Course = "Math",
                Faculty = "Math faculty",
                Semester = 2,
                Status = StatusEnum.Active,
                Year = 2020
            }
        };

        public Result<Student> GetStudentInfo(int id)
        {
            var student = StudentList.Where(s => s.Id == id).FirstOrDefault();

            if (student == null)
                return Result<Student>.Fail(new List<string> {"Student not exist"});

            return Result<Student>.Ok(student);
        }

        public Result<bool> DeletionStudent(int id)
        {
            var student = StudentList.Where(s => s.Id == id).FirstOrDefault();

            if (student == null)
                return Result<bool>.Fail(new List<string> {"Student not exist"});

            student.ChangeStudentStatus(StatusEnum.Deletion);

            return Result<bool>.Ok(true);
        }
    }

    public interface IStudentService
    {
        Result<Student> GetStudentInfo(int id);

        Result<bool> DeletionStudent(int id);
    }
}