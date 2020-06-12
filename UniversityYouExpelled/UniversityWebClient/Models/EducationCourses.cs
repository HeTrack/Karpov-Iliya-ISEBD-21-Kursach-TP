using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityWebClient.Models
{
    public class EducationCourses
    {
        public string  CourseName { get; set; }
        public decimal Cost { get; set; }
        public string LecturerFIO { get; set; }
        public DateTime StartCourse { get; set; }
    }
}
