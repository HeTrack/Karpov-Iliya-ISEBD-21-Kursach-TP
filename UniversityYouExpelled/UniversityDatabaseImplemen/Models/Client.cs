using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDatabaseImplement.Models
{
   public class Client
    {
        public int? Id { get; set; }
        [Required]
        public string ClientFIO { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]     
        public string Password { get; set; }
        [Required]
        public bool BlockStatus { get; set; }
        public virtual Education Education { get; set; }
    }
}
