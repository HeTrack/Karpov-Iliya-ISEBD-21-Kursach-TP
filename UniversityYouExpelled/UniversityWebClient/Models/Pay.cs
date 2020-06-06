using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityWebClient.Models
{
    public class Pay
    {
        [Required]
        public int PaySum { get; set; }
        public int EdicationId { get; set; }
    }
}
