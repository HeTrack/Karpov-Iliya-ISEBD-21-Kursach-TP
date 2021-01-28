using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace UniversityDataBaseImplement.Implements
{
    public class CourseLogic : ICourseLogic
    {
        private readonly string CourseFileName = "D://CourseData//Course.xml";
        //public List<Course> Courses { get; set; }
        
        private List<Course> LoadCourses()
        {
            var list = new List<Course>();
            if (File.Exists(CourseFileName))
            {
                XDocument xDocument = XDocument.Load(CourseFileName);
                var xElements = xDocument.Root.Elements("Course").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Course
                    {
                        ID = Convert.ToInt32(elem.Attribute("Id").Value),
                        Year = Convert.ToInt32(elem.Element("CourseYear").Value),
                        Name = elem.Element("CourseName").Value,
                        Lecturer = elem.Element("LecturerFIO").Value,
                        DateStart = Convert.ToDateTime(elem.Element("StartCourse").Value).Date,
                        Cost = Convert.ToDecimal(elem.Element("Cost").Value),
                    });
                }
            }
            return list;
        }
        public void Database()
        {
            var courses = LoadCourses();
            using (var context = new UniversityDatabase())
            {
                foreach (var course in courses)
                {
                    Course element = context.Courses.FirstOrDefault(rec => rec.Name == course.Name.ToString());
                    if (element != null)
                    {
                        break;
                    }
                    else
                    {
                        element = new Course();
                        context.Courses.Add(element);
                    }
                    element.Year = course.Year;
                    element.Name = course.Name;
                    element.Lecturer = course.Lecturer;
                    element.DateStart = course.DateStart.Date;
                    element.Cost = course.Cost;
                    context.SaveChanges();
                }
            }
        }
        public List<CourseViewModel> Read(CourseBindingModel model)
        {
            Database();
            using (var context = new UniversityDatabase())
            {
                return context.Courses.Where(rec => model == null || rec.ID == model.ID)
            .Select(rec => new CourseViewModel
            {
                ID = rec.ID,
                Year = rec.Year,
                Name = rec.Name,
                Lecturer = rec.Lecturer,
                DateStart = rec.DateStart.Date,
                Cost = rec.Cost,
            })
            .ToList();
            }
        }
    }
}
