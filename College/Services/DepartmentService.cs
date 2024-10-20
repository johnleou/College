using College.Data;
using College.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using College.DTO;
using System.Linq;

namespace College.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly CollegeDbContext _collegeDbContext;

        public DepartmentService(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }

        public async Task<List<DepartmentDTO>> GetAllDepartments()
        {
            try
            {
                var departments = await _collegeDbContext.Department.ToListAsync();

                if (departments is null)
                    return null;

                var departmentsDTO = departments.Select(d => new DepartmentDTO
                {
                    Title = d.Title,
                    Years = d.Years
                }).ToList();

                return departmentsDTO;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

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
                throw new NotImplementedException(ex.Message);
            }
        }

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
                    Id = new_department.Id,
                    Title = new_department.Title,
                    Years = new_department.Years
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating department" + ex.Message);
            }

        }

        public async Task<DepartmentDTO> UpdateDepartment(int id, DepartmentDTO departmentDTO)
        {
            try
            {
                var department = await _collegeDbContext.Department.FindAsync(id);
                if (departmentDTO is not null)
                {
                    department.Title = departmentDTO.Title;
                    department.Years = departmentDTO.Years;

                    await _collegeDbContext.SaveChangesAsync();

                    var updated_department = new DepartmentDTO
                    {
                        Id = department.Id,
                        Title = department.Title,
                        Years = department.Years
                    };
                    return updated_department;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating department with id {id} : " + ex.Message);
            }
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            try
            {
                var department = await _collegeDbContext.Department.FindAsync(id);

                
                    _collegeDbContext.Department.Remove(department);
                    await _collegeDbContext.SaveChangesAsync();

                    return true;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting department with id {id} : " + ex.Message);
            }

        }

    }
}
