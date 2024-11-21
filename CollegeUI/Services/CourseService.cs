using Shared.DTO;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;

namespace CollegeUI.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CourseDTO>> GetAllCourses()
        {
            return await _httpClient.GetFromJsonAsync<List<CourseDTO>>($"courses");
        }
        public async Task<CourseDetailDTO> GetCourseById(int id)
        {
            return await _httpClient.GetFromJsonAsync<CourseDetailDTO>($"courses/id");
        }
    }
}
