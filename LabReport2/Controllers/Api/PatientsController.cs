using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using LabReport2.Dtos;
using AutoMapper;

namespace LabReport2.Controllers.Api
{
    public class PatientsController : ApiController
    {
        private ApplicationDbContext _context;
        public PatientsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<PatientDto> GetPatients(string query=null)//get a List Of Customers
        {
            //Maping patient to the patient dto
            var patientsQuery= _context.Patients.ToList().Select(Mapper.Map<Patient, PatientDto>);
            if (!string.IsNullOrEmpty(query))
            {
                patientsQuery = patientsQuery.Where(t => t.PatientName.Contains(query));
            }

            return patientsQuery;
            //return _context.Patients.ToList().Select(Mapper.Map<Patient, PatientDto>);



        }
        

    }
}
