using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogic;
using UniversityBusinessLogic.Enums;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityWebClient.Models;

namespace UniversityWebClient.Controllers
{
    public class EducationController : Controller
    {
        private readonly IEducationLogic edLogic;
        private readonly ICourseLogic courseLogic;
        private readonly IPayLogic payLogic;
        private readonly ReportLogic reportLogic;

        public EducationController(IEducationLogic edLogic, ICourseLogic courseLogic, IPayLogic payLogic, ReportLogic reportLogic)
        {
            this.edLogic = edLogic;
            this.courseLogic = courseLogic;
            this.payLogic = payLogic;
            this.reportLogic = reportLogic;
        }
        public IActionResult Education()
        {
            ViewBag.Educations = edLogic.Read(new EducationBindingModel
            {
                ClientId = Program.Client.Id
            });
            return View();
        }
        [HttpPost]
        public IActionResult Education(ReportModel model)
        {
            var payList = new List<PayViewModel>();
            var educations = edLogic.Read(new EducationBindingModel
            {
                ClientId = Program.Client.Id,
                YearEd = model.YearFrom
                // DateFrom = model.From,
                // DateTo = model.To
            });
            var pays = payLogic.Read(null);
            foreach (var ed in educations)
            {
                foreach (var pay in pays)
                {
                    if (pay.ClientId == Program.Client.Id && pay.EducationId == ed.Id)
                        payList.Add(pay);
                }
            }
            ViewBag.Pays = payList;
            ViewBag.Educations = educations;
            string fileName = "D:\\Kursach TP\\PdfReport.pdf";
            if (model.SendMail)
            {
                reportLogic.SaveEducationPaysToPdfFile(fileName, new EducationBindingModel
                {
                    ClientId = Program.Client.Id,
                    YearEd = model.YearFrom
                    //DateFrom = model.From,
                    // DateTo = model.To
                }, Program.Client.Email);
            }
            return View();
        }
        public IActionResult CreateEducation()
        {
            ViewBag.EducationCourses = courseLogic.Read(null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateEducation(CreateEducation model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EducationCourses = courseLogic.Read(null);
                return View(model);
            }

            var edCourses = new List<EducationCourseBindingModel>();

            foreach (var course in model.EducationCourses)
            {
                if (course.Value > 0)
                {
                    edCourses.Add(new EducationCourseBindingModel
                    {
                        CourseId = course.Key
                    });
                }
            }
            if (edCourses.Count == 0)
            {
                ViewBag.EducationCourses = courseLogic.Read(null);
                ModelState.AddModelError("", "Ни один курс не выбран");
                return View(model);
            }
            edLogic.CreateOrUpdate(new EducationBindingModel
            {
                ClientId = Program.Client.Id,
                EdCreate= DateTime.Now,
                Status = EducationStatus.Принят,
                EdCost = CalculateSum(edCourses),
                EducationCourses = edCourses
            });
            return RedirectToAction("Education");
        }
        private decimal CalculateSum(List<EducationCourseBindingModel> edCourses)
        {
            decimal sum = 0;

            foreach (var course in edCourses)
            {
                var courseData = courseLogic.Read(new CourseBindingModel { Id = course.CourseId }).FirstOrDefault();

                if (courseData != null)
                {                    
                        sum += courseData.Cost;
                }
            }
            return sum;
        }
       
        public IActionResult Pay(int id)
        {
            var education = edLogic.Read(new EducationBindingModel
            {
                Id = id
            }).FirstOrDefault();
            ViewBag.Education = education;
            ViewBag.Remain = CalculateRemain(education);
            return View();
        }
        [HttpPost]
        public ActionResult Pay(Pay model)
        {
            EducationViewModel education = edLogic.Read(new EducationBindingModel
            {
                Id = model.EducationId
            }).FirstOrDefault();
           decimal Remain = CalculateRemain(education);
            if (!ModelState.IsValid)
            {
                ViewBag.Education = education;
                ViewBag.Remain = Remain;
                return View(model);
            }
            if (Remain < model.PaySum)
            {
                ViewBag.Education = education;
                ViewBag.Remain = Remain;
                return View(model);
            }
            payLogic.CreateOrUpdate(new PayBindingModel
            {
                EducationId = education.Id,
                ClientId = Program.Client.Id,
                PayDate = DateTime.Now,
                Sum = model.PaySum
            });
            Remain -= model.PaySum;
            edLogic.CreateOrUpdate(new EducationBindingModel
            {
                Id = education.Id,
                ClientId = education.ClientId,
                EdCreate = education.EdCreate,
                StatusEducation = education.StatusEducation,
                YearEd = education.YearEd,
                Status = Remain > 0 ? EducationStatus.Частично_Оплачен : EducationStatus.Оплачен,
                EdCost = education.EdCost,
                EducationCourses = education.EducationCourses.Select(rec => new EducationCourseBindingModel
                {
                    Id = rec.Id,
                    EducationId = rec.EducationId,
                    CourseId = rec.CourseId,
                }).ToList()
            });
            return RedirectToAction("Education");
        }

        private decimal CalculateRemain(EducationViewModel education)
        {
            decimal sum = education.EdCost;
            decimal paidSum = payLogic.Read(new PayBindingModel
            {
               EducationId = education.Id
            }).Select(rec => rec.Sum).Sum();

            return sum - paidSum;
        }
        public IActionResult SendWordReport(int id)
        {
            var education = edLogic.Read(new EducationBindingModel { Id = id }).FirstOrDefault();
            string fileName = "D:\\Kursach TP\\" + education.Id + ".docx";
            reportLogic.SaveEducationCoursesToWordFile(fileName, education, Program.Client.Email);
            return RedirectToAction("Education");
        }
        public IActionResult SendExcelReport(int id)
        {
            var education = edLogic.Read(new EducationBindingModel { Id = id }).FirstOrDefault();
            string fileName = "D:\\Kursach TP\\" + education.Id + ".xlsx";
            reportLogic.SaveEducationCoursesToExcelFile(fileName, education, Program.Client.Email);
            return RedirectToAction("Education");
        }
    }
}
