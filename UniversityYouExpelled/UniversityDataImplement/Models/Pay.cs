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
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        public int EducationId { get; set; }
        [Required]
        public DateTime PayDate { get; set; }
        [Required]
        public decimal Sum { get; set; }
        public virtual Education Education { get; set; }
        public virtual Client Client { get; set; }
    }
}
