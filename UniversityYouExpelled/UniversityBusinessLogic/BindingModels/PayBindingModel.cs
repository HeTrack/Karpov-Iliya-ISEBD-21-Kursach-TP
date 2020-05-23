using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
   public class PayBindingModel
    {
        public int Id { get; set; }
        public int EducationId { get; set; }
        public DateTime DateOfPay { get; set; }
        public decimal Sum { get; set; }
    }
}
