using College.Models;
using College.Data;
using Microsoft.EntityFrameworkCore;

namespace College.Services
{
    public class StudentService : IStudentService
    {
        private readonly CollegeDbContext _collegeDbContext;

        public StudentService(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            try
            {
                var students = await _collegeDbContext.Students.ToListAsync();
                return students;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<Student> GetStudentById(int id)
        {
            try
            {
                var student = await _collegeDbContext.Students.FindAsync(id);
                return student;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<Student> CreateStudent(Student student)
        {
            try
            {
                _collegeDbContext.Students.Add(student);
                await _collegeDbContext.SaveChangesAsync();

                return student;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                var student = await _collegeDbContext.Students.FindAsync(id);
                if (student != null)
                {
                    _collegeDbContext.Students.Remove(student);
                    await _collegeDbContext.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }                

        public async Task<Student> UpdateStudent(int id, Student student)
        {
            try
            {
                var _student = _collegeDbContext.Students.Find(id);
                if(_student is not null)
                {
                    _student.First_Name = student.First_Name;
                    _student.Last_Name = student.Last_Name;
                    _student.Semester = student.Semester;

                    await _collegeDbContext.SaveChangesAsync();
                }
                
                return _student;

            }catch(Exception ex)
            {
                throw new NotImplementedException();
            }            
        }
    }
}
