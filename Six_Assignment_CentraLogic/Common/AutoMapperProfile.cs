using AutoMapper;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;

namespace EmployeeManagementSystem.Comman
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeBasicDetailsEntity, EmployeeBasicDetailsDTO>().ReverseMap();
            CreateMap<EmployeeAdditionalDetailsEntity, EmployeeAdditionDetailsDTO>().ReverseMap();
        }
    }

    
}
