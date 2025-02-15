﻿using College.Data;
using Shared.DTO;
using College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using College.Services;

namespace College.Controllers
{
    [Route("api")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;        

        /// <summary>
        /// Constructor for Department Controller
        /// </summary>
        /// <param name="departmentService"></param>
        /// <param name="collegeDbContext"></param>
        public DepartmentController(IDepartmentService departmentService, CollegeDbContext collegeDbContext)
        {
            _departmentService = departmentService;            
        }           

        /// <summary>
        /// Get a list of departments
        /// </summary>
        /// <returns></returns>
        [HttpGet("departments")]
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

        /// <summary>
        /// Get department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("department/{id}")]
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

        /// <summary>
        /// Create new department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost("department")]
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

        /// <summary>
        /// Update department by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut("department/{id}")]
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

        /// <summary>
        /// Delete department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deparment/{id}")]
        public async Task<IActionResult> DeleteDepartmentById(int id)
        {
            try
            {
                bool deleteDepartment = await _departmentService.DeleteDepartment(id);
                if(deleteDepartment)
                    return Ok($"Deleting department with id {id} has been made succesfully!");
                else
                    return NotFound($"Error! Department with id {id} not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
