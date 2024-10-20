using College.Models;

namespace College.Services
{
    public interface ICourseService
    {
        public Task<List<Course>> GetAllCourses();
        public Task<Course> GetCourseById(int id);
        public Task<Course> CreateCourse(Course course);
        public Task<Course> UpdateCourse(int id, Course course);
        public Task<bool> DeleteCourse(int id);
    }
}
