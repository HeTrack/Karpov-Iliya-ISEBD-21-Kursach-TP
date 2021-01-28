using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDataBaseImplement.Models
{
  public  class Pay
    {
        public int ID { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int EducationID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public DateTime DatePay { get; set; }
        [Required]
        public decimal SumPay { get; set; }
        public virtual Education Education { get; set; }
        public virtual Client Client { get; set; }
    }
}
