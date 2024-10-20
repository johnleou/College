using College.Data;
using College.DTO;
using College.Models;
using College.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly CollegeDbContext _collegeDbContext;

        public DepartmentController(IDepartmentService departmentService, CollegeDbContext collegeDbContext)
        {
            _departmentService = departmentService;
            _collegeDbContext = collegeDbContext;
        }           

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentById(id);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDTO department)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var new_department = await _departmentService.CreateDepartment(department);
                return Ok(new_department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDTO department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updated_department = await _departmentService.UpdateDepartment(id, department);
                return Ok(updated_department);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDepartmentById(int id)
        {
            try
            {
                bool deleteDepartment = await _departmentService.DeleteDepartment(id);
                return Ok(deleteDepartment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
