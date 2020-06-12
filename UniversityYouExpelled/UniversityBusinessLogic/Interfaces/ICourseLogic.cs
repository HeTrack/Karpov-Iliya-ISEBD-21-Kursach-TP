using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
   public interface ICourseLogic
    {
        List<CourseViewModel> Read(CourseBindingModel model);
        void Database();
    }
}
