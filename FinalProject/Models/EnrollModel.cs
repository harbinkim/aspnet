using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class EnrollModel
    {
        public int SelectedClassIndex { get; set; }
        public IEnumerable<Class> Classes { get; set; }
    }
}