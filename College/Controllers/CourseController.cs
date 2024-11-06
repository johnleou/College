using Shared.DTO;
using College.Services;
using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    [Route("api")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        /// <summary>
        /// Constructor for Course Controller
        /// </summary>
        /// <param name="courseService"></param>
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Get a list of courses
        /// </summary>
        /// <returns></returns>
        [HttpGet("courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCourses();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("courses/{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            try
            {
                var course = await _courseService.GetCourseById(id);
                return Ok(course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create new course
        /// </summary>
        /// <param name="courseDTO"></param>
        /// <returns></returns>
        [HttpPost("courses")]
        public async Task<IActionResult> CreateCourse(CourseDTO courseDTO)
        {
            try
            {
                var new_course = await _courseService.CreateCourse(courseDTO);
                return Ok(new_course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a course by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseDTO"></param>
        /// <returns></returns>
        [HttpPut("courses/{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseDTO courseDTO)
        {
            try
            {
                await _courseService.UpdateCourse(id, courseDTO);
                return Ok(courseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("courses/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _courseService.DeleteCourse(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Assign course to department
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("assignCourseToDepartment")]
        public async Task<IActionResult> AssignCourseToDepartment([FromQuery] int courseId, int departmentId)
        {
            bool result = await _courseService.AssignCourseToDepartment(courseId, departmentId);
            if (result is true)
                return Ok();
            else
                return BadRequest();
        }
    }
}
