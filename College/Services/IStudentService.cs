using College.Models;

namespace College.Services
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudents(); 
        public Task<Student> GetStudentById(int id);
        public Task<Student> CreateStudent(Student student);
        public Task<Student> UpdateStudent(int id, Student student);
        public Task<bool> DeleteStudent(int id);
    }
}
