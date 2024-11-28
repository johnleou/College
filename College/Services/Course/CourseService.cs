using College.Data;
using Shared.DTO;
using College.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Services
{
    public class CourseService : ICourseService
    {
        private readonly CollegeDbContext _collegeDbContext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collegeDbContext"></param>
        public CourseService(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }
        /// <summary>
        /// Get all courses
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<CourseDTO>> GetAllCourses()
        {
            try
            {
                var courses = await _collegeDbContext.Course.ToListAsync();
                if (courses is null)
                    return null;

                var coursesDTO = courses.Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Hours = c.Hours
                }).ToList();

                return coursesDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error by retrieving list of courses : " + ex.Message);
            }
        }
        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CourseDetailDTO> GetCourseById(int id)
        {
            try
            {
                var course = await _collegeDbContext.Course
                    .Include(c => c.Department)
                    .Include(c => c.Students)
                    .FirstOrDefaultAsync(c => c.Id == id);
                //.ThenInclude(d => d.Department)
                //.AsSplitQuery().FirstAsync(c => c.Id == id);

                var courseDTO = new CourseDetailDTO
                {
                    Title = course.Title,
                    Hours = course.Hours,
                    Department = course.Department != null
                        ? new DepartmentDTO
                        {
                            Id = course.Department.Id,
                            Title = course.Department.Title,
                            Years = course.Department.Years
                        }
                        : null,
                    Students = course.Students.Select(s => new StudentDTO
                    {
                        Id = s.Id,
                        First_Name = s.First_Name,
                        Last_Name = s.Last_Name,
                        Semester = s.Semester
                    }).ToList()
                };

                return courseDTO;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error by retrieving course with id {id} : " + ex.Message);
            }
        }
        /// <summary>
        /// Create course
        /// </summary>
        /// <param name="courseDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CourseDTO> CreateCourse(CourseDTO courseDTO)
        {
            try
            {
                var course = new Course
                {
                    Title = courseDTO.Title,
                    Hours = courseDTO.Hours
                };

                _collegeDbContext.Course.Add(course);
                await _collegeDbContext.SaveChangesAsync();

                var new_course = new CourseDTO
                {
                    Title = course.Title,
                    Hours = course.Hours
                };

                return courseDTO;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error by creating new course : " + ex.Message);
            }
        }
        /// <summary>
        /// Update course
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CourseDTO> UpdateCourse(int id, CourseDTO courseDTO)
        {
            try
            {
                var course = await _collegeDbContext.Course.FindAsync(id);
                if (course is not null)
                {
                    course.Title = courseDTO.Title;
                    course.Hours = courseDTO.Hours;

                    await _collegeDbContext.SaveChangesAsync();

                    var new_course = new CourseDTO
                    {
                        Title = course.Title,
                        Hours = course.Hours
                    };

                    return new_course;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error by updating course with id {id} : " + ex.Message);
            }
        }
        /// <summary>
        /// Delete course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteCourse(int id)
        {
            try
            {
                var course = await _collegeDbContext.Course.FindAsync(id);
                if (course is not null)
                {
                    _collegeDbContext.Remove(course);
                    await _collegeDbContext.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error by deleting course with id {id} : " + ex.Message);
            }
        }
        /// <summary>
        /// Assign course to department
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> AssignCourseToDepartment(int courseId, int departmentId)
        {
            try
            {
                var course = _collegeDbContext.Course.Find(courseId);

                if (course is null)
                    throw new Exception($"Course with id {courseId} not found");

                var department = _collegeDbContext.Department.Find(departmentId);

                if (department is null)
                    throw new Exception($"Department with id {departmentId} not found");

                course.Department = department;
                await _collegeDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : " + ex.Message);
            }
        }
    }
}
