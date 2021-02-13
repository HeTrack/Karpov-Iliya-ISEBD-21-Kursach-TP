using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplement.Implements
{
    public class EducationLogic : IEducationLogic
    {
        public void CreateOrUpdate(EducationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Education elem = model.ID.HasValue ? null : new Education();
                        if (model.ID.HasValue)
                        {
                            elem = context.Educations.FirstOrDefault(rec => rec.ID == model.ID);
                            if (elem == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                            elem.ClientID = model.ClientID;
                            elem.DateCreate = model.DateCreate;
                            elem.YearED = model.YearED;
                            elem.CostED = model.CostED;
                            elem.PayStatus = model.PayStatus;
                            context.SaveChanges();
                            context.EducationCourses.RemoveRange(context.EducationCourses.Where(rec => rec.EducationID == elem.ID));
                            var courses = model.EducationCourses
                               .GroupBy(rec => rec.CourseID)
                               .Select(rec => new
                               {
                                   CourseID = rec.Key
                               });

                            foreach (var course in courses)
                            {
                                context.EducationCourses.Add(new EducationCourse
                                {
                                    EducationID = elem.ID,
                                    CourseID = course.CourseID
                                });
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            elem = context.Educations.FirstOrDefault(rec => rec.YearED == model.YearED && rec.ClientID == model.ClientID);                      
                            if (elem != null)
                            {
                                context.EducationCourses.RemoveRange(context.EducationCourses.Where(rec => rec.EducationID == elem.ID));
                                elem.ClientID = model.ClientID;
                                elem.DateCreate = model.DateCreate;
                                elem.CostED = model.CostED;
                                elem.YearED = model.YearED;
                                elem.PayStatus = model.PayStatus;
                                context.Educations.Update(elem);
                            }
                            else
                            {
                                elem = new Education
                                {
                                    ClientID = model.ClientID,
                                    DateCreate = model.DateCreate,
                                    CostED = model.CostED,
                                    YearED = model.YearED,
                                    PayStatus = model.PayStatus
                                };
                                context.Educations.Add(elem);
                            }
                            context.SaveChanges();
                            var courses = model.EducationCourses
                               .GroupBy(rec => rec.CourseID)
                               .Select(rec => new
                               {
                                   CourseID = rec.Key
                               });

                            foreach (var course in courses)
                            {
                                context.EducationCourses.Add(new EducationCourse
                                {
                                    EducationID = elem.ID,
                                    CourseID = course.CourseID
                                });
                                context.SaveChanges();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(EducationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Education elem = context.Educations.FirstOrDefault(rec => rec.YearED == model.YearED && rec.ClientID == model.ClientID);

                        if (elem != null)
                        {
                            context.EducationCourses.RemoveRange(context.EducationCourses.Where(rec => rec.EducationID == elem.ID));
                            context.SaveChanges();
                            context.Educations.Remove(elem);
                            context.SaveChanges();

                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        //throw;
                    }
                }
            }
        }
    
        public List<EducationViewModel> Read(EducationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                return context.Educations.Where(rec => rec.ID == model.ID ||
                (rec.ClientID == model.ClientID) && (model.YearED == null || rec.YearED == model.YearED && model.From == null && model.To == null || rec.YearED >= model.From && rec.YearED <= model.To))
                .Select(rec => new EducationViewModel
                {
                    ID = rec.ID,
                    ClientID = rec.ClientID,
                    FIO = rec.Client.FIO,
                    YearED = rec.YearED,
                    DateCreate = rec.DateCreate,
                    PayStatus = rec.PayStatus,
                    CostED = rec.CostED,
                    Remain = rec.CostED - context.Pays.Where(recP => recP.EducationID == recP.ID).Select(recP => recP.SumPay).Sum(),                 
                    EducationCourses = GetEdCourse(rec)
                })
            .ToList();
            }
        }
        public static List<EducationCourseViewModel> GetEdCourse(Education ed)
        {
            using (var context = new UniversityDatabase())
            {
                var EducationCourses = context.EducationCourses
                    .Where(rec => rec.EducationID == ed.ID)
                    .Include(rec => rec.Course)
                    .Select(rec => new EducationCourseViewModel
                    {
                        ID = rec.ID,
                        EducationID = rec.EducationID,
                        CourseID = rec.CourseID                        
                    }).ToList();
                foreach (var course in EducationCourses)
                {
                    var courseData = context.Courses.Where(rec => rec.ID == course.CourseID).FirstOrDefault();
                    course.Lecturer = courseData.Lecturer;
                    course.Name = courseData.Name;                  
                    course.DateStart = courseData.DateStart;
                    course.Cost = courseData.Cost;
                }
                return EducationCourses;
            }
        }
    }
}