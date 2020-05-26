﻿using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    interface ICourseLogic
    {

        List<CourseViewModel> Read(CourseBindingModel model);
        void CreateOrUpdate(CourseBindingModel model);
        void Delete(CourseBindingModel model);
    }
}
