using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Enums;
using UniversityBusinessLogic.Interfaces;
using UniversityWebClient.Models;

namespace UniversityWebClient.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientLogic clientLogic;
        private readonly int passwordMinLength = 6;
        private readonly int passwordMaxLength = 20;
        private readonly int loginMinLength = 1;
        private readonly int loginMaxLength = 50;
        public ClientController(IClientLogic clientLogic)
        {
            this.clientLogic = clientLogic;
        }
        public ActionResult UserAccount()
        {
            ViewBag.User = Program.Client;
            return View();
        }
        public IActionResult Auth()
        {
                return View();
        }
        
        [HttpPost]
        public ActionResult Auth(Auth client)
        {
            var clientView = clientLogic.Read(new ClientBindingModel
            {
                Login = client.Login,
                Password = client.Password
            }).FirstOrDefault();
            if (clientView == null)
            {
                ModelState.AddModelError("", "Вы ввели неверный пароль, либо пользователь не найден");
                return View(client);
            }
            if (clientView.UserType == "Аdmin")
            {
                return RedirectToAction("AdminMain", "Client");
            }
            else
            {
                return RedirectToAction("CurrentEducation", "Education");
            }
        }
        public IActionResult Logout()
        {
            Program.Client = null;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]      
        public ViewResult Registration(Registration newClient)
        {
            if (string.IsNullOrEmpty(newClient.Login))
            {
                ModelState.AddModelError("", "Введите логин");
                return View(newClient);
            }
            if (newClient.Login.Length > loginMaxLength || newClient.Login.Length < loginMinLength)
            {
                ModelState.AddModelError("", $"Длина логина должна быть от {loginMinLength} до {loginMaxLength} символов");
                return View(newClient);
            }
            var existClient = clientLogic.Read(new ClientBindingModel
            {
                Login = newClient.Login
            }).FirstOrDefault();
            if (existClient != null)
            {
                ModelState.AddModelError("", "Данный логин уже занят");
                return View(newClient);
            }
            if (String.IsNullOrEmpty(newClient.Email))
            {
                ModelState.AddModelError("", "Введите электронную почту");
                return View(newClient);
            }
            existClient = clientLogic.Read(new ClientBindingModel
            {
                Email = newClient.Email
            }).FirstOrDefault();
            if (existClient != null)
            {
                ModelState.AddModelError("", "Данный Email уже занят");
                return View(newClient);
            }
            if (!Regex.IsMatch(newClient.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                ModelState.AddModelError("", "Email введен некорректно");
                return View(newClient);
            }
            if (newClient.Password.Length > passwordMaxLength || newClient.Password.Length < passwordMinLength)
            {
                ModelState.AddModelError("", $"Длина пароля должна быть от {passwordMinLength} до {passwordMaxLength} символов");
                return View(newClient);
            }
            if (string.IsNullOrEmpty(newClient.FIO))
            {
                ModelState.AddModelError("", "Введите ФИО");
                return View(newClient);
            }
            if (string.IsNullOrEmpty(newClient.Password))
            {
                ModelState.AddModelError("", "Введите пароль");
                return View(newClient);
            }
            if (string.IsNullOrEmpty(newClient.Phone))
            {
                ModelState.AddModelError("", "Введите номер телефона");
                return View(newClient);
            }
            if (!Regex.IsMatch(newClient.Phone, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"))
            {
                ModelState.AddModelError("", "Номер телефона введен некорректно");
                return View(newClient);
            }
            clientLogic.CreateOrUpdate(new ClientBindingModel
            {
                FIO = newClient.FIO,
                Year = 1,
                Login = newClient.Login,
                Password = newClient.Password,
                UserType = "Client",
                BlockStatus = BlockStatus.Активный,
                Phone = newClient.Phone,
                Email = newClient.Email,
                DateRegister = DateTime.Now
            });  
            ModelState.AddModelError("", "Вы успешно зарегистрированы");
            return View("Registration", newClient);
        }
    }
}