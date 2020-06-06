using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<EducationViewModel> Edications { get; set; }
        public Dictionary<int, List<PayViewModel>> Pays { get; set; }
    }
}
