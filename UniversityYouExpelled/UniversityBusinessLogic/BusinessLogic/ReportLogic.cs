using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.HelperModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly ICourseLogic courseLogic;
        private readonly IEducationLogic edLogic;
        private readonly IPayLogic payLogic;
        public ReportLogic(ICourseLogic courseLogic, IEducationLogic edLogic, IPayLogic payLogic)
        {
            this.courseLogic = courseLogic;
            this.edLogic = edLogic;
            this.payLogic = payLogic;
        }
        public List<CourseViewModel> GetEducationCourses(EducationViewModel ed)
        {
            var courses = new List<CourseViewModel>();

            foreach (var course in ed.EducationCourses)
            {
                courses.Add(courseLogic.Read(new CourseBindingModel
                {
                    ID = course.CourseID
                }).FirstOrDefault());

            }
            return courses;
        }
        public Dictionary<int, List<PayViewModel>> GetEducationPays(EducationBindingModel model)
        {
            var educations = edLogic.Read(model).ToList();
            Dictionary<int, List<PayViewModel>> pays = new Dictionary<int, List<PayViewModel>>();
            foreach (var education in educations)
            {
                var EdPays = payLogic.Read(new PayBindingModel { EducationID = education.ID }).ToList();
                pays.Add(education.ID, EdPays);
            }
            return pays;
        }
        public void SaveEducationPaysToPdfFile(string fileName, EducationBindingModel education, string email)
        {
            string title = "Список обучений с оплатами с " + education.From.ToString() + " по " + education.To.ToString() + " год ";
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = fileName,
                Title = title,
                Educations = edLogic.Read(education).ToList(),
                Pays = GetEducationPays(education)
            });
            SendMail(email, fileName, title);
        }
        public void SaveEducationCoursesToWordFile(string fileName, EducationViewModel education, string email)
        {
            string title = "Список курсов по " + education.YearED + " году обучения";
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = title,
                Courses = GetEducationCourses(education)
            });
            SendMail(email, fileName, title);
        }
        public void SaveEducationCoursesToExcelFile(string fileName, EducationViewModel education, string email)
        {
            string title = "Список курсов по " + education.YearED + " году обучения";
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = title,
                Courses = GetEducationCourses(education)
            });
            SendMail(email, fileName, title);
        }
        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("iliyalabatp@gmail.com", "Государственный Технический Университет «Все отчислены»");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to)
            {
                Subject = subject
            };
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("iliyalabatp@gmail.com", "amene1488");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}