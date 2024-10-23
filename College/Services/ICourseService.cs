using College.DTO;

namespace College.Services
{
    public interface ICourseService
    {
        public Task<List<CourseDTO>> GetAllCourses();
        public Task<CourseDetailDTO> GetCourseById(int id);
        public Task<CourseDTO> CreateCourse(CourseDTO courseDTO);
        public Task<CourseDTO> UpdateCourse(int id, CourseDTO courseDTO);
        public Task<bool> DeleteCourse(int id);
        public Task<bool> AssignCourseToDepartment(int courseId, int departmentId);
    }
}
