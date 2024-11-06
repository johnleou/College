using Shared.DTO;
using System.Net.Http.Json;

namespace CollegeUI.Services
{
    public class StudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StudentDetailDTO> GetStudentById(int id)
        {
            return await _httpClient.GetFromJsonAsync<StudentDetailDTO>($"student/{id}");
        }
    }
}
