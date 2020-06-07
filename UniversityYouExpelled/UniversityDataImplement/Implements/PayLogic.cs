using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplement.Implements
{
   public class PayLogic:  IPayLogic
    {
        public void CreateOrUpdate(PayBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Pay elem = model.Id.HasValue ? null : new Pay();
                if (model.Id.HasValue)
                {
                    elem = context.Pays.FirstOrDefault(rec => rec.Id ==
                       model.Id);
                    if (elem == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    elem = new Pay();
                    context.Pays.Add(elem);
                }
                elem.EducationId = model.EducationId;
                elem.ClientId = model.ClientId;
                elem.Sum = model.Sum;
                elem.PayDate = model.PayDate;
                context.SaveChanges();
            }
        }
        public void Delete(PayBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Pay elem = context.Pays.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (elem != null)
                {
                    context.Pays.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<PayViewModel> Read(PayBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                return context.Pays
                .Where(rec => model == null || rec.Id == model.Id || rec.EducationId.Equals(model.EducationId))
                .Select(rec => new PayViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    PayDate = rec.PayDate,
                    EducationId = rec.EducationId,
                    Sum = rec.Sum
                })
                .ToList();
            }
        }
    }
}