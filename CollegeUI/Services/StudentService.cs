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
        
        public async Task<List<StudentDTO>> GetAllStudents()
        {
            return await _httpClient.GetFromJsonAsync<List<StudentDTO>>($"https://localhost:44364/api/students");
        }

        public async Task<StudentDetailDTO> GetStudentById(int id)
        {
            return await _httpClient.GetFromJsonAsync<StudentDetailDTO>($"https://localhost:44364/api/student/{id}");
        }

        public async Task CreateStudent(StudentDTO studentDTO)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:44364/api/student", studentDTO);
        }

        public async Task UpdateStudent(int id, StudentDTO studentDTO)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:44364/api/students/{id}", studentDTO);
        }

        public async Task DeleteStudent(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:44364/api/students/{id}");
        }
    }
}