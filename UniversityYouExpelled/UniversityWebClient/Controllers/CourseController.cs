using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.Interfaces;
using UniversityDataBaseImplement.Implements;

namespace UniversityWebClient.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseLogic course;
        public CourseController(CourseLogic course)
        {
           this.course = course;
        }
        public IActionResult Course()
        {
            ViewBag.Courses = course.Read(null);
            return View();
        }
    }
}
