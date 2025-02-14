using College.Models;
using College.Data;
using Microsoft.EntityFrameworkCore;
using Shared.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace College.Services
{
    public class StudentService : IStudentService
    {
        private readonly CollegeDbContext _collegeDbContext;

        public StudentService(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                        Id = s.Id,
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

        /// <summary>
        /// Get student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StudentDetailDTO> GetStudentById(int id)
        {
            try
            {
                var student = await _collegeDbContext.Student
                .Include(s => s.Department)
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == id);

                if (student is null)
                    return null;
                else
                {
                    var studentDTO = new StudentDetailDTO
                    {
                        Id = student.Id,
                        First_Name = student.First_Name,
                        Last_Name = student.Last_Name,
                        Semester = student.Semester,
                        Department = student.Department != null
                            ? new DepartmentDTO
                            {
                                Id = student.Department.Id,
                                Title = student.Department.Title,
                                Years = student.Department.Years,
                            }
                            : null,
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
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Create new student
        /// </summary>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StudentDTO> CreateStudent(StudentDTO studentDTO)
        {
            JsonSerializerSettings jsonSettingsError = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            try
            {
                
                var student = new Student
                {
                    First_Name = studentDTO.First_Name,
                    Last_Name = studentDTO.Last_Name,
                    Semester = studentDTO.Semester,
                };

                var studentJson = JsonConvert.SerializeObject(studentDTO, jsonSettingsError);
                Console.WriteLine(studentJson);
                student = JsonConvert.DeserializeObject<Student>(studentJson, jsonSettingsError);

                _collegeDbContext.Student.Add(student);
                await _collegeDbContext.SaveChangesAsync();

                return studentDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating new student : " + ex.Message);
            }
        }

        /// <summary>
        /// Update student by id, studentDTO
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StudentDTO> UpdateStudent(int id, StudentDTO studentDTO)
        {
            try
            {
                var student = await _collegeDbContext.Student.FindAsync(id);
                if (student is null)
                    return null;
                else
                {
                    student.First_Name = studentDTO.First_Name;
                    student.Last_Name = studentDTO.Last_Name;
                    student.Semester = studentDTO.Semester;

                    await _collegeDbContext.SaveChangesAsync();

                    return studentDTO;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// Assign Student to Department By Id
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

        /// Assign Student to Course By Id
        public async Task<bool> AssignStudentToCourse(int studentId, int courseId)
        {

            try
            {
                var student = await _collegeDbContext.Student
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == studentId);

                if (student is null)
                    throw new Exception($"Student with id {studentId} not found");

                var course = await _collegeDbContext.Course
                    .Include(c => c.Department)
                    .FirstOrDefaultAsync(c => c.Id == courseId);

                if (course is null)
                    throw new Exception($"Department with id {studentId} not found");

                student.Courses.Add(course);
                await _collegeDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
