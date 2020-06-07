using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityWebClient.Models
{
    public class ReportModel
    {
        public int YearFrom { get; set; }
        public int CourseTo { get; set; }
        public bool SendMail { get; set; }
    }
}
