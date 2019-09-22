using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LabReport2.Models;
using LabReport2.Dtos;

namespace LabReport2.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
             Mapper.CreateMap<Patient,PatientDto>();
            Mapper.CreateMap<PatientDto,Patient>();
        }
       
    }
}