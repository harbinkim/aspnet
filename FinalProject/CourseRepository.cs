using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject
{
    public interface ICourseRepository
    {
        IEnumerable<Class> Courses { get; }
    }

    public class CourseRepository : ICourseRepository
    {
        public IEnumerable<Class> Courses
        {
            get
            {
                var items = new[]
                    {
                    new Class{ ClassId=101, ClassName = "Baseball", ClassDescription="balls", ClassPrice=250.00m},
                    new Class{ ClassId=102, ClassName="Football", ClassDescription="nfl", ClassPrice=200.00m},
                    new Class{ ClassName="Tennis ball"} ,
                    new Class{ ClassName="Golf ball"},
                };
                return items;
            }
        }
    }
}