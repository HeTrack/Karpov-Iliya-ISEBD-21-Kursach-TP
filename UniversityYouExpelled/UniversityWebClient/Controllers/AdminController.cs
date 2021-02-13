using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;

namespace UniversityWebClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly IClientLogic _client;

        public AdminController(IClientLogic client)
        {
            _client = client;
        }
        public IActionResult Index()
        {
            ViewBag.Clients = _client.Read(null).Where(rec => rec.UserType != "Admin");
            return View();
        }
        public ActionResult Block(int id)
        {
            if (ModelState.IsValid)
            {
                var existClient = _client.Read(new ClientBindingModel
                {
                    ID = id
                }).FirstOrDefault();
                _client.CreateOrUpdate(new ClientBindingModel
                {
                    ID = id,
                    FIO = existClient.FIO,
                    Login = existClient.Login,
                    Password = existClient.Password,
                    Email = existClient.Email,
                    Phone = existClient.Phone,
                    BlockStatus = 1
                }) ;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}