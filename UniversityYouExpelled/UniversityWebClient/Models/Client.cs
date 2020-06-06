using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityWebClient.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string ClientFIO { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool BlockStatus { get; set; }
    }
}
