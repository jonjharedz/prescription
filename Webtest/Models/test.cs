using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webtest.Models
{
    public class Student
    {
     
        public int Id { get; set; }
        public string Email { get; set; }

    }
}