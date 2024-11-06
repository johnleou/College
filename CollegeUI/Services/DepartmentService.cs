using Shared.DTO;
using System.Net.Http.Json;

namespace CollegeUI.Services
{
    public class DepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DepartmentDetailDTO> GetDepartmentById(int id)
        {
            return await _httpClient.GetFromJsonAsync<DepartmentDetailDTO>($"department/{id}");
        }

        public async Task<List<DepartmentDTO>> GetAllDepartments()
        {
            return await _httpClient.GetFromJsonAsync<List<DepartmentDTO>>($"department");
        }
    }
}
