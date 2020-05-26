using System;
using System.Collections.Generic;
using System.Linq;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
    public class EducationLogic
    {
        public void CreateOrUpdate(EducationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Education element = context.Educations.FirstOrDefault(rec =>
                            rec.YearEd == model.YearEd && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть такое обучение");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Educations.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Education();
                            context.Educations.Add(element);
                        }
                        element.Id = model.Id;
                        element.EdCreate = model.EdCreate;
                        element.StatusEducation = model.StatusEducation;
                        element.ClientId = model.ClientId.Value;
                        element.YearEd = model.YearEd;
                        element.EdCost = model.EdCost;
                        element.Status = model.Status;
                        var groupCourses = model.EducationCourses
                        .GroupBy(rec => rec.CourseId)
                        .Select(rec => new
                        {
                            CourseId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                        foreach (var groupCourse in groupCourses)
                        {
                           var EducationCourse = new EducationCourse
                            {
                                EducationId = element.Id,
                                CourseId = groupCourse.CourseId,
                                Count = groupCourse.Count
                            };
                            context.EducationCourses.Add(EducationCourse);
                            context.SaveChanges();                         
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
                        context.EducationCourses.RemoveRange(context.EducationCourses.Where(rec =>
                        rec.EducationId == model.Id));
                        Education element = context.Educations.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.Educations.Remove(element);
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
                        throw;
                    }
                }
            }
        }
        public List<EducationViewModel> Read(EducationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                return context.Educations.Where(rec => model == null
                    || rec.Id == model.Id && model.Id.HasValue)
                .Select(rec => new EducationViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ClientFIO = rec.Client.ClientFIO,
                    YearEd = rec.YearEd,
                    EdCreate = rec.EdCreate,
                    StatusEducation = rec.StatusEducation,
                    EdCost = rec.EdCost,
                    Status = rec.Status,
                    EducationCourses = context.EducationCourses
                     .Where(recEC => recEC.EducationId == rec.Id)
                     .Select(recEC => new EducationCourseViewModel
                     {
                         Id = recEC.Id,
                         EducationId = recEC.EducationId,
                         CourseId = recEC.CourseId,                       
                         Count = recEC.Count
                     })
                        .ToList()
                })
                .ToList();
            }
        }
    }
}