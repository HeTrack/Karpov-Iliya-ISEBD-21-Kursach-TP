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
                Pay elem = model.ID.HasValue ? null : new Pay();
                if (model.ID.HasValue)
                {
                    elem = context.Pays.FirstOrDefault(rec => rec.ID ==
                       model.ID);
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
                elem.EducationID = model.EducationID;
                elem.ClientID = model.ClientID;
                elem.SumPay = model.SumPay;
                elem.DatePay = model.DatePay;
                context.SaveChanges();
            }
        }
        public void Delete(PayBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Pay elem = context.Pays.FirstOrDefault(rec => rec.ID ==
               model.ID);
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
                .Where(rec => model == null || rec.ID == model.ID || rec.EducationID.Equals(model.EducationID))
                .Select(rec => new PayViewModel
                {
                    ID = rec.ID,
                    ClientID = rec.ClientID,
                    DatePay = rec.DatePay,
                    EducationID = rec.EducationID,
                    SumPay = rec.SumPay
                })
                .ToList();
            }
        }
    }
}