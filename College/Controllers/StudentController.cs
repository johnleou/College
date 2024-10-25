﻿using College.Services;
using Microsoft.AspNetCore.Mvc;
using College.DTO;

namespace College.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        /// <summary>
        /// Constructor for Student Controller
        /// </summary>
        /// <param name="studentService"></param>
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Get a list of students
        /// </summary>
        /// <returns></returns>
        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create new student
        /// </summary>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
        [HttpPost("students")]
        public async Task<IActionResult> CreateStudent(StudentDTO studentDTO)
        {
            try
            {
                var new_student = await _studentService.CreateStudent(studentDTO);
                return Ok(new_student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update student by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
        [HttpPut("students/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDTO studentDTO)
        {
            try
            {
                await _studentService.UpdateStudent(id, studentDTO);
                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("students/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _studentService.DeleteStudent(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Assign student to department
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpPost("assignStudentToDepartment")]
        public async Task<IActionResult> AssignStudentToDepartment([FromQuery] int studentId, int departmentId)
        {
            bool result = await _studentService.AssignStudentToDepartment(studentId, departmentId);
            if (result is true)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Assign course to department
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPost("assignStudentToCourse")]
        public async Task<IActionResult> AssignStudentToCourse(int studentId, int courseId)
        {
            bool result = await _studentService.AssignStudentToCourse(studentId, courseId);
            if (result is true)
                return Ok();
            else
                return BadRequest();
        }
    }
}
