using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniversityBusinessLogic.BindingModels;
using System.Runtime.Serialization.Json;
using System.IO;
using UniversityDataBaseImplement.Models;
using System.Xml;
namespace UniversityDataBaseImplement.Implements
{
        public class BackUpLogic : BackUpAbstractLogic
        {
            protected override Assembly GetAssembly()
            {
                return typeof(BackUpLogic).Assembly;
            }
            protected override List<PropertyInfo> GetFullList()
            {
                using (var context = new UniversityDatabase())
                {
                    Type type = context.GetType();
                    return type.GetProperties().Where(x =>
                   x.PropertyType.FullName.StartsWith("Microsoft.EntityFrameworkCore.DbSet")).ToList();
                }
            }
            protected override List<T> GetList<T>()
            {
                using (var context = new UniversityDatabase())
                {
                    return context.Set<T>().ToList();
                }
            }
        }
    }