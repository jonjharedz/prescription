using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Webtest.Models;

namespace Webtest.DAL
{
    public class OmboriContext: DbContext
    {
        public OmboriContext() : base("OmboriConnection")
        {
            Database.SetInitializer<OmboriContext>(new CreateDatabaseIfNotExists<OmboriContext>());
        }

        public DbSet<Prescription> Prescription { get; set; }
        
    }
}