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
   public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Client elem = model.ID.HasValue ? null : new Client();
                if (model.ID.HasValue)
                {
                    elem = context.Clients.FirstOrDefault(rec => rec.ID == model.ID);
                    if (elem == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    elem = new Client();
                    context.Clients.Add(elem);
                }
                elem.FIO = model.FIO;
                elem.Year = model.Year;
                elem.Login = model.Login;
                elem.Password = model.Password;
                elem.UserType = model.UserType;
                elem.BlockStatus = model.BlockStatus;
                elem.Phone = model.Phone;
                elem.Email = model.Email;
                elem.DateRegister = model.DateRegister;
                context.SaveChanges();
            }
        }
        public void Delete(ClientBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Client elem = context.Clients.FirstOrDefault(rec => rec.ID == model.ID);
                if (elem != null)
                {
                    context.Clients.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                return context.Clients
                 .Where(rec => model == null
                   || rec.ID == model.ID
                 || (rec.Login == model.Login || rec.Email == model.Email)
                        && (model.Password == null || rec.Password == model.Password))
               .Select(rec => new ClientViewModel
               {
                   ID = rec.ID,
                   FIO = rec.FIO,
                   Year = rec.Year,
                   Login = rec.Login,
                   Password = rec.Password,
                   UserType = rec.UserType,
                   BlockStatus = rec.BlockStatus,
                   Phone = rec.Phone,
                   Email = rec.Email,
                   DateRegister = rec.DateRegister
               })
                .ToList();
            }
        }
    }
}