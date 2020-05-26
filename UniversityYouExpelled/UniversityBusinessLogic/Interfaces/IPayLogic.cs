using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
   public interface IPayLogic
    {
        List<PayViewModel> Read(PayBindingModel model);
        void CreateOrUpdate(PayBindingModel model);
        void Delete(PayBindingModel model);
    }
}
