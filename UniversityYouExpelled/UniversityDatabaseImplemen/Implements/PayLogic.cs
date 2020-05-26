using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
   public class PayLogic:  IPayLogic
    {
        public void CreateOrUpdate(PayBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Pay element = context.Pays.FirstOrDefault(rec => rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть платеж  с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Pays.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Pay();
                    context.Pays.Add(element);
                }
                element.EducationId = model.EducationId;
                element.Sum = model.Sum;
                element.PayDate = model.PayDate;
                context.SaveChanges();
            }
        }
        public void Delete(PayBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Pay element = context.Pays.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Pays.Remove(element);
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
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new PayViewModel
                {
                    Id = rec.Id,
                    PayDate = rec.PayDate,
                    EducationId = rec.EducationId,
                    Sum = rec.Sum
                })
                .ToList();
            }
        }
    }
}