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

namespace UniversityDataBaseImplement.Implements
{
   public class CourseLogic: ICourseLogic
    {    
        private readonly string CourseFileName = "C://Users//iliya//Sourse//Kursach TP//UniversityYouExpelled//UniversityData//Courses.xml";
        public List<Course> Courses { get; set; }
        public CourseLogic()
        {
            Courses = LoadCourses();
        }
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
                        CourseName = elem.Element("CourseName").Value,
                        LecturerFIO = elem.Element("LecturerFIO").Value,
                        StartCourse = Convert.ToDateTime(elem.Element("StartCourse").Value),
                        Cost = Convert.ToInt32(elem.Element("Cost").Value),
                    });
                }
            }
            return list;
        }
        public void SaveToDatabase()
        {
            var courses = LoadCourses();
            using (var context = new UniversityDatabase())
            {
                foreach (var course in courses)
                {
                    Course element = context.Courses.FirstOrDefault(rec => rec.Id == course.Id);
                    if (element != null)
                    {
                        break;
                    }
                    else
                    {
                        element = new Course();
                        context.Courses.Add(element);
                    }
                    element.CourseName = course.CourseName;
                    element.LecturerFIO = course.LecturerFIO;
                    element.StartCourse = course.StartCourse;                 
                    element.Cost = course.Cost;
                    context.SaveChanges();
                }
            }
        }
        public List<CourseViewModel> Read(CourseBindingModel model)
        {
            SaveToDatabase();
            return Courses
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new CourseViewModel
            {
                Id = rec.Id,
                CourseName = rec.CourseName,
                LecturerFIO = rec.LecturerFIO,             
                StartCourse = rec.StartCourse,
                Cost = rec.Cost,
            })
            .ToList();
        }
    }
}
