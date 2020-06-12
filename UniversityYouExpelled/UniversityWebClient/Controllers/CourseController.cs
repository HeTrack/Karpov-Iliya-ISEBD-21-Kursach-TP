using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityWebClient.Models;

namespace UniversityWebClient.Controllers
{
    public class CourseController : Controller
    {   
            private readonly ICourseLogic _course;
            public CourseController(ICourseLogic course)
            {
                _course = course;
            }
            public IActionResult Course()
            {
                ViewBag.Courses = _course.Read(null);
                return View();
            }
        }
    }
