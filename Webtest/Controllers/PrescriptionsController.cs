using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Webtest.DAL;
using Webtest.Models;
using Webtest.Provider;
using Webtest.Repository;

namespace Webtest.Controllers
{
    public class PrescriptionsController : ApiController
    {
        PrescriptionRepository _repository = new PrescriptionRepository();
        public PrescriptionsController()
        {

        }
        // GET api/prescriptions
        public IEnumerable<Prescription> Get()
        {
            return _repository.GetPrescriptions();
        }

        // GET api/prescriptions/{id}
        public Prescription Get(string id)
        {
            return _repository.GetPrescription(id);
        }

        // POST api/prescriptions 
        [BasicHttpAuthorizeAttribute] // username=ombori / pass=ombori
        public Prescription Post([FromBody] Prescription pPrescription)
        {
            if (pPrescription == null) throw new Exception("Please complete prescription information");
            try
            {
                if(!validatePrescription(pPrescription)) throw new Exception("Please complete prescription information");
               return _repository.SavePrescription(pPrescription);
            } catch(Exception e)
            {
                throw e;
            }
        }

        private bool validatePrescription(Prescription pPrescription)
        {
            if (string.IsNullOrEmpty(pPrescription.Description) ||
                string.IsNullOrEmpty(pPrescription.ProductName) ||
                string.IsNullOrEmpty(pPrescription.PatientId) || pPrescription.ExpirationDate == null
                ) return false;
            return true;
        }


        // DELETE api/prescriptions/{id}
        [BasicHttpAuthorizeAttribute] // username=ombori / pass=ombori
        public void Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Required parameter is empty.");
                _repository.DeletePrescription(id);
            } catch(Exception e)
            {
                throw e;
            }
        }
    }
}
