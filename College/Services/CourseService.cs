using College.Data;
using College.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics.Eventing.Reader;

namespace College.Services
{
    public class CourseService : ICourseService
    {
        private readonly CollegeDbContext _collegeDbContext;

        public CourseService(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }
        public async Task<List<Course>> GetAllCourses()
        {
            try
            {
                var courses = await _collegeDbContext.Courses.ToListAsync();
                return courses;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<Course> GetCourseById(int id)
        {
            try
            {
                var course = await _collegeDbContext.Courses.FindAsync(id);
                return course;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<Course> CreateCourse(Course course)
        {
            try
            {
                _collegeDbContext.Courses.Add(course);
                await _collegeDbContext.SaveChangesAsync();
                    
                return course;

            }catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<Course> UpdateCourse(int id, Course course)
        {
            try
            {
                var _course =  _collegeDbContext.Courses.Find(id);
                if(_course == null)
                {
                    _course.Title = course.Title;
                    _course.Hours = course.Hours;

                    await _collegeDbContext.SaveChangesAsync();
                }

                return _course;

            }catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<bool> DeleteCourse(int id)
        {
            try
            {
                var course = _collegeDbContext.Courses.Find(id);
                if(course is not null)
                {
                    _collegeDbContext.Remove(course);
                    await _collegeDbContext.SaveChangesAsync();
                }

                return true;
                
            }catch(Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }        
    }
}
