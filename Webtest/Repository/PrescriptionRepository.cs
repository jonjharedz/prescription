using System;
using System.Collections.Generic;
using System.Linq;
using Webtest.DAL;
using Webtest.Models;
using Webtest.Utility;

namespace Webtest.Repository
{
    public class PrescriptionRepository
    {


        public IEnumerable<Prescription> GetPrescriptions()
        {
            try
            {
                using (var context = new OmboriContext())
                {
                    return context.Prescription.ToList();
                }

            } catch(Exception e)
            {
                throw new Exception("Error getting list of Prescriptions, please contact your administrator");
            }
        }

        public Prescription GetPrescription(string pId)
        {
            try
            {
                using (var context = new OmboriContext())
                {
                    return context.Prescription.Where(w => w.Id == pId.Trim()).FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error getting a Prescription, please contact your administrator");
            }
        }
        public Prescription SavePrescription(Prescription pPrescription)
        {
            try
            {
                using (var context = new OmboriContext())
                {
                    long lNewId = 1;
                    var vPrescription= context.Prescription.OrderByDescending(o=>o.Id).FirstOrDefault();
                    if(vPrescription!=null)
                    {
                        string sLastId = vPrescription.Id;
                        lNewId = CommonHelper.ExtractSequenceId(sLastId)+1;
                    }

                    var newPrescription = new Prescription
                    {
                        Id = CommonHelper.GenerateNewId(lNewId),
                        Description = pPrescription.Description,
                        ExpirationDate = pPrescription.ExpirationDate,
                        IsActive = pPrescription.IsActive,
                        PatientId = pPrescription.PatientId,
                        ProductName = pPrescription.ProductName,
                        UsesLeft = pPrescription.UsesLeft
                    };

                    context.Set<Prescription>().Add(newPrescription);
                    context.SaveChanges();
                    return newPrescription;
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error Saving prescription, please contact your administrator");
            }
        }

        public bool DeletePrescription(string id)
        {
            try
            {
                using (var context = new OmboriContext())
                {
                    var vPrescription = context.Prescription.Where(w => w.Id == id).FirstOrDefault();
                    if (vPrescription != null)
                    {
                        context.Set<Prescription>().Remove(vPrescription);
                        context.SaveChanges();
                    } else
                    {
                        throw new Exception("Prescription does not exist.");
                    }
                    return true;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
  
        }

    }
}