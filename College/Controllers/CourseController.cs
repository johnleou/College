using College.Data;
using College.Models;
using College.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace College.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseById(id);
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            var new_course = await _courseService.CreateCourse(course);
            return Ok(new_course);
        }

        [HttpPut]       
        public async Task<IActionResult> UpdateCourse(int id, Course course)
        {
            await _courseService.UpdateCourse(id, course);
            return Ok(course);
        }

        [HttpDelete("{id}")]        
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _courseService.DeleteCourse(id);
            return Ok();
        }
    }
}
