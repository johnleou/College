using College.Data;
using College.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DTO;

namespace College.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly CollegeDbContext _collegeDbContext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collegeDbContext"></param>
        public DepartmentService(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }

        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<DepartmentDTO>> GetAllDepartments()
        {
            try
            {
                var departments = await _collegeDbContext.Department.ToListAsync();

                if (departments is null)
                    return null;

                var departmentsDTO = departments.Select(d => new DepartmentDTO
                {
                    Id = d.Id,
                    Title = d.Title,
                    Years = d.Years
                }).ToList();

                return departmentsDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DepartmentDetailDTO> GetDepartmentById(int id)
        {
            try
            {
                var department = await _collegeDbContext.Department
                    .Include(s => s.Students)
                    .ThenInclude(c => c.Courses)
                    .AsSplitQuery().FirstAsync(d => d.Id == id);

                if (department is null)
                    return null;

                var departmentDTO = new DepartmentDetailDTO
                {
                    Title = department.Title,
                    Years = department.Years,
                    Students = department.Students.Select(s => new StudentDTO
                    {
                        Id = s.Id,
                        First_Name = s.First_Name,
                        Last_Name = s.Last_Name
                    }).ToList(),
                    Courses = department.Courses.Select(c => new CourseDTO
                    {
                        Id = c.Id,
                        Title = c.Title
                    }).ToList()
                };

                return departmentDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Create department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DepartmentDTO> CreateDepartment(DepartmentDTO department)
        {
            try
            {
                var new_department = new Department
                {
                    Title = department.Title,
                    Years = department.Years
                };

                _collegeDbContext.Department.Add(new_department);
                await _collegeDbContext.SaveChangesAsync();

                return new DepartmentDTO
                {
                    Title = new_department.Title,
                    Years = new_department.Years
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating department : " + ex.Message);
            }

        }

        /// <summary>
        /// Update department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departmentDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DepartmentDTO> UpdateDepartment(int id, DepartmentDTO departmentDTO)
        {
            try
            {
                var department = await _collegeDbContext.Department.FindAsync(id);
                if (department is null)
                    return null;
                else
                {
                    department.Title = departmentDTO.Title;
                    department.Years = departmentDTO.Years;

                    await _collegeDbContext.SaveChangesAsync();

                    var updated_department = new DepartmentDTO
                    {
                        Title = department.Title,
                        Years = department.Years
                    };
                    return updated_department;
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating department with id {id} : " + ex.Message);
            }
        }

        /// <summary>
        /// Delete department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteDepartment(int id)
        {
            try
            {
                var department = await _collegeDbContext.Department.FindAsync(id);
                if (department is null)
                    return false;
                else
                {
                    _collegeDbContext.Department.Remove(department);
                    await _collegeDbContext.SaveChangesAsync();

                    return true;
                }                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting department with id {id} : " + ex.Message);
            }

        }

    }
}
