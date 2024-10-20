namespace College.DTO
{
    public class DepartmentDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Years { get; set; }
        public ICollection<StudentDTO> Students { get; set; }        
        public ICollection<CourseDTO> Courses { get; set; }
    }
}
