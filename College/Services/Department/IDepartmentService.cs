using Shared.DTO;
using College.Models;

namespace College.Services
{
    public interface IDepartmentService
    {
        public Task<List<DepartmentDTO>> GetAllDepartments();
        public Task<DepartmentDetailDTO> GetDepartmentById(int id);
        public Task<DepartmentDTO> CreateDepartment(DepartmentDTO Department);
        public Task<DepartmentDTO> UpdateDepartment(int id, DepartmentDTO Department);
        public Task<bool> DeleteDepartment(int id);
    }
}
