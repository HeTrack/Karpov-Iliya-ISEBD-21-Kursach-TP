using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDataBaseImplement.Models
{
   public class Client
    {
        public int Id { get; set; }
        [Required]
        public string ClientFIO { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]     
        public string Password { get; set; }
        [Required]
        public DateTime DataRegistration { get; set; }
        [Required]
        public bool BlockStatus { get; set; }
        
        [ForeignKey("ClientId")]
        public virtual List<Education> Educations { get; set; }
        
        [ForeignKey("ClientId")]
        public virtual List<Pay> Pays { get; set; }
    }
}
