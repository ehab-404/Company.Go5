
using AutoMapper;
using Company.Go5.DAL.Models;
using Company.Go5.PLMVC.Dtos;

namespace Company.Go5.PLMVC.Mapping
{
    public class EmployeeProfile:Profile 
    {
        public EmployeeProfile()
        {

            //map between this 2 classes and reverse mapping  across same prop names
            //mapping create new object 


            CreateMap<Employee, EmployeeDto>()
                //.ForMember(d => d.Name, o => o.MapFrom(s => s.Name))

                .ReverseMap();


        }
    }
}
