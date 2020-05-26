using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDatabaseImplement.Models
{
  public  class Pay
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public int EducationId { get; set; }
        [Required]
        public DateTime PayDate { get; set; }
        [Required]
        public decimal Sum { get; set; }
        public virtual Education Education { get; set; }
    }
}
