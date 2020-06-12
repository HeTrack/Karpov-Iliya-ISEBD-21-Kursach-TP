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
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        CourseYear = Convert.ToInt32(elem.Element("CourseYear").Value),
                        CourseName = elem.Element("CourseName").Value,
                        LecturerFIO = elem.Element("LecturerFIO").Value,
                        StartCourse = Convert.ToDateTime(elem.Element("StartCourse").Value).Date,
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
                    Course element = context.Courses.FirstOrDefault(rec => rec.CourseName == course.CourseName.ToString());
                    if (element != null)
                    {
                        break;
                    }
                    else
                    {
                        element = new Course();
                        context.Courses.Add(element);
                    }
                    element.CourseYear = course.CourseYear;
                    element.CourseName = course.CourseName;
                    element.LecturerFIO = course.LecturerFIO;
                    element.StartCourse = course.StartCourse.Date;
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
                return context.Courses.Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new CourseViewModel
            {
                Id = rec.Id,
                CourseYear = rec.CourseYear,
                CourseName = rec.CourseName,
                LecturerFIO = rec.LecturerFIO,
                StartCourse = rec.StartCourse.Date,
                Cost = rec.Cost,
            })
            .ToList();
            }
        }
    }
}
