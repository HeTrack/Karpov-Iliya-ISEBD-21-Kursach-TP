using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;

namespace UniversityBusinessLogic.Interfaces
{
    interface IEducationLogic
    {
        List<EducationViewModel> Read(EducationBindingModel model);
        void CreateOrUpdate(EducationBindingModel model);
        void Delete(EducationBindingModel model);
    }
}
