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
        public IActionResult CurrentEducation()
        {
            ViewBag.Education = edLogic.Read(new EducationBindingModel
            {
                ClientID = Program.Client.ID,
                YearED = Program.Client.Year
            });
            return View();
        }
        public IActionResult Education()
        {
            ViewBag.Educations = edLogic.Read(new EducationBindingModel
            {
                ClientID = Program.Client.ID        
            });
            return View();
        }
        [HttpPost]
        public IActionResult Education(ReportModel model)
        {
            var payList = new List<PayViewModel>();
            var educations = edLogic.Read(new EducationBindingModel
            {
                ClientID = Program.Client.ID,
                From = model.EdFrom,
                To = model.EdTo
            });
            var pays = payLogic.Read(null);
            foreach (var ed in educations)
            {
                foreach (var pay in pays)
                {
                    if (pay.ClientID == Program.Client.ID && pay.EducationID == ed.ID)
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
                    ClientID = Program.Client.ID,
                    From = model.EdFrom,
                    To = model.EdTo
                }, Program.Client.Email);
            }
            return View();
        }
        public IActionResult ChangeEducation()
        {
            ViewBag.Educations = edLogic.Read(new EducationBindingModel
            {
                ClientID = Program.Client.ID,
                YearED = Program.Client.Year
            }).FirstOrDefault();
            ViewBag.EducationCourses = courseLogic.Read(null).Where(rec => rec.Year == Program.Client.Year);
            ViewBag.User = Program.Client;
            return View();
        }
        [HttpPost]
        public ActionResult ChangeEducation(CreateEducation model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EducationCourses = courseLogic.Read(null).Where(rec => rec.Year == Program.Client.Year);
                return View(model);
            }

            var edCourses = new List<EducationCourseBindingModel>();

            foreach (var course in model.EducationCourses)
            {
                if (course.Value == true)
                {
                    edCourses.Add(new EducationCourseBindingModel
                    {
                        CourseID = course.Key
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
                ClientID = Program.Client.ID,
                DateCreate = DateTime.Now,
                PayStatus = PayStatus.Принят,
                CostED = CalculateSum(edCourses),
                YearED = Program.Client.Year,
                EducationCourses = edCourses
            });
            return RedirectToAction("CurrentEducation");
            
        }

        public IActionResult DelEducation()
        {
         
                edLogic.Delete(new EducationBindingModel
                {
                    ClientID = Program.Client.ID,
                    YearED = Program.Client.Year
                });
                     
            return RedirectToAction("CurrentEducation");
        }
        
        public IActionResult CreateEducation()
        {
            ViewBag.EducationCourses = courseLogic.Read(null).Where(rec => rec.Year == Program.Client.Year);
            ViewBag.User = Program.Client;
            return View();
        }
        [HttpPost]
        public ActionResult CreateEducation(CreateEducation model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EducationCourses = courseLogic.Read(null).Where(rec => rec.Year == Program.Client.Year);
                return View(model);
            }

            var edCourses = new List<EducationCourseBindingModel>();
            foreach (var course in model.EducationCourses)
            {
                if (course.Value == true) 
                {
                    edCourses.Add(new EducationCourseBindingModel
                    {                                    
                        CourseID = course.Key              
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
                ClientID = Program.Client.ID,
                DateCreate = DateTime.Now,
                PayStatus = PayStatus.Принят,
                CostED = CalculateSum(edCourses),
                YearED = Program.Client.Year,
                EducationCourses = edCourses
            });
            return RedirectToAction("Education");
        }
        private decimal CalculateSum(List<EducationCourseBindingModel> edCourses)
        {
            decimal sum = 0;

            foreach (var course in edCourses)
            {
                var courseData = courseLogic.Read(new CourseBindingModel { ID = course.CourseID }).FirstOrDefault();

                if (courseData != null)
                {                    
                        sum += courseData.Cost;
                }
            }
            return sum;
        }
        [HttpPost]
        public ActionResult Pay(Pay model)
        {
            EducationViewModel education = edLogic.Read(new EducationBindingModel
            {
                ID = model.EducationID
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
                EducationID = education.ID,
                ClientID = Program.Client.ID,
                DatePay = DateTime.Now,
                SumPay = model.PaySum
            });
            Remain -= model.PaySum;
            edLogic.CreateOrUpdate(new EducationBindingModel
            {
                ID = education.ID,
                ClientID = education.ClientID,
                YearED = education.YearED,
                DateCreate = education.DateCreate,             
                PayStatus = Remain > 0 ? PayStatus.Частично_Оплачен : PayStatus.Оплачен,
                CostED = education.CostED,
                EducationCourses = education.EducationCourses.Select(rec => new EducationCourseBindingModel
                {
                    ID = rec.ID,
                    EducationID = rec.EducationID,
                    CourseID = rec.CourseID,
                }).ToList()
            });
            return RedirectToAction("Education");
        }

        private decimal CalculateRemain(EducationViewModel education)
        {
            decimal sum = education.CostED;
            decimal paidSum = payLogic.Read(new PayBindingModel
            {
               EducationID = education.ID
            }).Select(rec => rec.SumPay).Sum();

            return sum - paidSum;
        }
        public IActionResult SendWordReport(int id)
        {
            var education = edLogic.Read(new EducationBindingModel { ID = id }).FirstOrDefault();
            string fileName = "D:\\Kursach TP\\" + education.ID + ".docx";
            reportLogic.SaveEducationCoursesToWordFile(fileName, education, Program.Client.Email);
            return RedirectToAction("Education");
        }
        public IActionResult SendExcelReport(int id)
        {
            var education = edLogic.Read(new EducationBindingModel { ID = id }).FirstOrDefault();
            string fileName = "D:\\Kursach TP\\" + education.ID + ".xlsx";
            reportLogic.SaveEducationCoursesToExcelFile(fileName, education, Program.Client.Email);
            return RedirectToAction("Education");
        }
        public IActionResult Pay(int id)
        {
            var education = edLogic.Read(new EducationBindingModel
            {
                ID = id
            }).FirstOrDefault();
            ViewBag.Education = education;
            ViewBag.EducationCourse = education.EducationCourses;
            ViewBag.Remain = CalculateRemain(education);
            return View();
        }
    }
}
