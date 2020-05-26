using System;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Enums;
using UniversityBusinessLogic.Interfaces;

namespace UniversityBusinessLogic
{
    public class MainLogic
    {

        private readonly IEducationLogic EdLogic;
        private readonly object locker = new object();
        public MainLogic(IEducationLogic EdLogic)
        {
            this.EdLogic = EdLogic;
        }
        public void CreateEducation(CreateEducationBindingModel model)
        {
            EdLogic.CreateOrUpdate(new EducationBindingModel
            {
                EdCreate = DateTime.Now,
                Id = model.EducationId,
                ClientId = model.ClientId,
                EdCost = model.EdCost,
                Status = PayStatus.Принят,
            });
        }
    }
}