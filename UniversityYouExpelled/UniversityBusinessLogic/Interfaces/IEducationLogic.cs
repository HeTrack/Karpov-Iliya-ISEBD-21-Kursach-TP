﻿using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IEducationLogic
    {
        List<EducationViewModel> Read(EducationBindingModel model);
        void CreateOrUpdate(EducationBindingModel model);
        void Delete(EducationBindingModel model);
    }
}
