using College.Data;
using College.DTO;
using College.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Services
{
    public class CourseService : ICourseService
    {
        private readonly CollegeDbContext _collegeDbContext;

        public CourseService(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }

        public async Task<List<CourseDTO>> GetAllCourses()
        {
            try
            {
                var courses = await _collegeDbContext.Course.ToListAsync();
                if (courses is null)
                    return null;

                var coursesDTO = courses.Select(c => new CourseDTO
                {
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

        public async Task<CourseDetailDTO> GetCourseById(int id)
        {
            try
            {
                var course = await _collegeDbContext.Course
                    .Include(s => s.Students)
                    .ThenInclude(d => d.Department)
                    .AsSplitQuery().FirstAsync(c => c.Id == id);



                var courseDTO = new CourseDetailDTO
                {
                    Title = course.Title,
                    Hours = course.Hours,
                    Department = new DepartmentDTO
                    {
                        Id = course.Department.Id,
                        Title = course.Department.Title,
                        Years = course.Department.Years
                    },
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
                throw new Exception($"Error assigning course with id {courseId} to department with id {departmentId} : " + ex.Message);
            }
        }
    }
}
