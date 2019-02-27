using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webtest.Models
{

    public class Prescription
    {
        public string Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ProductName { get; set; }
        public int UsesLeft { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string PatientId { get; set; }

    }
}