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
                       Education elem = model.Id.HasValue ? null : new Education();
                        if (model.Id.HasValue)
                        {
                            elem = context.Educations.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (elem == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                            elem.ClientId = model.ClientId;
                            elem.EdCreate = model.EdCreate;
                            elem.YearEd = model.YearEd;
                            elem.StatusEducation = model.StatusEducation;
                            elem.EdCost = model.EdCost;
                            elem.Status = model.Status;
                            context.SaveChanges();
                        }
                        else
                        {
                            elem.ClientId = model.ClientId;
                            elem.EdCreate = model.EdCreate;
                            elem.YearEd = model.YearEd;
                            elem.StatusEducation = model.StatusEducation;
                            elem.EdCost = model.EdCost;
                            elem.Status = model.Status;
                            context.Educations.Add(elem);
                            context.SaveChanges();
                            var courses = model.EducationCourses
                               .GroupBy(rec => rec.CourseId)
                               .Select(rec => new
                               {
                                   CourseId = rec.Key,
                                   Count = rec.Sum(r => r.Count)
                               });

                            foreach (var course in courses)
                            {
                                context.EducationCourses.Add(new EducationCourse
                                {
                                    EducationId = elem.Id,
                                    CourseId = course.CourseId,
                                    Count = course.Count
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
                Education elem = context.Educations.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (elem != null)
                {
                    context.Educations.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<EducationViewModel> Read(EducationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                return context.Educations.Where(rec => rec.Id == model.Id || (rec.ClientId == model.ClientId))
                .Select(rec => new EducationViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ClientFIO = rec.Client.ClientFIO,
                    YearEd= rec.YearEd,
                    EdCreate = rec.EdCreate,
                    StatusEducation = rec.StatusEducation,
                    EdCost = rec.EdCost,
                    PaidSum = context.Pays.Where(recP => recP.EducationId == recP.Id).Select(recP => recP.Sum).Sum(),
                    Status = rec.Status,
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
                    .Where(rec => rec.EducationId == ed.Id)
                    .Include(rec => rec.Course)
                    .Select(rec => new EducationCourseViewModel
                    {
                        Id = rec.Id,
                        EducationId = rec.EducationId,
                        CourseId = rec.CourseId,
                        Count = rec.Count
                    }).ToList();
                foreach (var course in EducationCourses)
                {
                    var courseData = context.Courses.Where(rec => rec.Id == course.CourseId).FirstOrDefault();
                    course.LecturerFIO = courseData.LecturerFIO;
                    course.CourseName = courseData.CourseName;                  
                    course.StartCourse = courseData.StartCourse;
                    course.Cost = courseData.Cost;
                }
                return EducationCourses;
            }
        }
    }
}