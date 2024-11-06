using Shared.DTO;

namespace College.Services
{
    public interface IStudentService
    {
        public Task<List<StudentDTO>> GetAllStudents(); 
        public Task<StudentDetailDTO> GetStudentById(int id);
        public Task<StudentDTO> CreateStudent(StudentDTO studentDTO);
        public Task<StudentDTO> UpdateStudent(int id, StudentDTO studentDTO);
        public Task<bool> DeleteStudent(int id);
        public Task<bool> AssignStudentToDepartment(int studentId, int departmentId);
        public Task<bool> AssignStudentToCourse(int studentId, int departmentId);
    }
}
