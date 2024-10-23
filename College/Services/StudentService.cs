using College.Models;
using College.Data;
using Microsoft.EntityFrameworkCore;
using College.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace College.Services
{
    public class StudentService : IStudentService
    {
        private readonly CollegeDbContext _collegeDbContext;

        public StudentService(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }

        public async Task<List<StudentDTO>> GetAllStudents()
        {
            try
            {
                var students = await _collegeDbContext.Student.ToListAsync();
                if (students is null)
                    return null;
                else
                {
                    var studentsDTO = students.Select(s => new StudentDTO
                    {
                        First_Name = s.First_Name,
                        Last_Name = s.Last_Name,
                        Semester = s.Semester,
                    }).ToList();

                    return studentsDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all students : " + ex.Message);
            }
        }

        public async Task<StudentDetailDTO> GetStudentById(int id)
        {
            try
            {
                var student = await _collegeDbContext.Student
                    .Include(s=>s.Department)
                    .Include(s=>s.Courses)
                    .FirstOrDefaultAsync(s=>s.Id == id);

                if (student is null)
                    return null;
                else
                {
                    var studentDTO = new StudentDetailDTO
                    {
                        First_Name = student.First_Name,
                        Last_Name = student.Last_Name,
                        Semester = student.Semester,
                        Department = new DepartmentDTO
                        {
                            Id = student.Department.Id,
                            Title = student.Department.Title,
                            Years = student.Department.Years,
                        },
                        Courses = student.Courses.Select(c => new CourseDTO
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Hours = c.Hours
                        }).ToList()
                    };
                    return studentDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving student with id {id}: " + ex.Message);
            }
        }

        public async Task<StudentDTO> CreateStudent(StudentDTO studentDTO)
        {
            try
            {
                if (studentDTO is null)
                    return null;
                else
                {
                    var student = new Student
                    {
                        First_Name = studentDTO.First_Name,
                        Last_Name = studentDTO.Last_Name,
                        Semester = studentDTO.Semester,
                    };

                    _collegeDbContext.Student.Add(student);
                    await _collegeDbContext.SaveChangesAsync();

                    return studentDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating new student : " + ex.Message);
            }
        }

        public async Task<StudentDTO> UpdateStudent(int id, StudentDTO studentDTO)
        {
            try
            {
                var student = _collegeDbContext.Student.Find(id);
                if (student is not null)
                {
                    student.First_Name = studentDTO.First_Name;
                    student.Last_Name = studentDTO.Last_Name;
                    student.Semester = studentDTO.Semester;

                    await _collegeDbContext.SaveChangesAsync();
                }

                return studentDTO;

            }
            catch (Exception ex)
            {
                throw new Exception("Error updating student with id {id} : " + ex.Message);
            }
        }

        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                var student = await _collegeDbContext.Student.FindAsync(id);
                if (student != null)
                {
                    _collegeDbContext.Student.Remove(student);
                    await _collegeDbContext.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting student with id {id} : " + ex.Message);
            }
        }

        public async Task<bool> AssignStudentToDepartment(int studentId, int departmentId)
        {
            try
            {
                var student = await _collegeDbContext.Student.FindAsync(studentId);

                if (student is null)
                    throw new Exception($"Student with id {studentId} not found");
 
                var department = _collegeDbContext.Department.Find(departmentId);

                if (department is null)
                    throw new Exception($"Department with id {departmentId} not found");
                
                    student.Department = department;
                    await _collegeDbContext.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error assigning student with id {studentId} to department with id {departmentId} : " + ex.Message);
            }
        }

        public async Task<bool> AssignStudentToCourse(int studentId, int courseId)
        {
            try
            {
                var student = await _collegeDbContext.Student
                    .Include(s=>s.Department)
                    .FirstOrDefaultAsync(s => s.Id == studentId);

                var course = await _collegeDbContext.Course
                    .Include(c => c.Department)
                    .FirstOrDefaultAsync(c => c.Id == courseId);

                if (student.Department.Id != course.Department.Id)
                    return false;
                else
                {
                    student.Courses.Add(course);
                    await _collegeDbContext.SaveChangesAsync();
                    return true;
                }

            }catch(Exception ex)
            {
                throw new Exception($"Error assigning student with id {studentId} to course with id {courseId} : " + ex.Message);
            }
        }
    }
}
