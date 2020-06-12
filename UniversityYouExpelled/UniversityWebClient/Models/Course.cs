using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityWebClient.Models
{
    public class Course
    {
        public int CourseYear { get; set; }
        public string CourseName { get; set; }
        public string LectureFIO { get; set; }
        public DateTime StartCourse { get; set; }      
        public decimal Cost { get; set; }
    }
}
