using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UniversityBusinessLogic.Enums;

namespace UniversityWebClient.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public int Year { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int BlockStatus { get; set; }
        public string Phone { get; set; }
        public DateTime DateRegister { get; set; }    
    }
}
