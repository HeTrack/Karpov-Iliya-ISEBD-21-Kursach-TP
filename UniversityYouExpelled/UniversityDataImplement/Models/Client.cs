using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusinessLogic.Enums;

namespace UniversityDataBaseImplement.Models
{
   public class Client
    {
        public int ID { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserType { get; set; }
        [Required]
        public BlockStatus BlockStatus { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateRegister { get; set; }
        [ForeignKey("ClientID")]
        public virtual List<Education> Educations { get; set; }
        [ForeignKey("ClientID")]
        public virtual List<Pay> Pays { get; set; }
    }
}
