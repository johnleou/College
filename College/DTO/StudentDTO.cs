﻿using System.Text.Json.Serialization;

namespace College.DTO
{
    public class StudentDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Semester {  get; set; }
    }
}
