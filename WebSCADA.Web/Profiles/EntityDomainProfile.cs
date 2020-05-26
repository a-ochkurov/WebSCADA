using AutoMapper;
using WebSCADA.DAL.Entities;
using WebSCADA.Domain.Models;

namespace WebSCADA.Web.Profiles
{
    public class EntityDomainProfile : Profile
    {
        public EntityDomainProfile()
        {
            CreateMap<Schema, SchemaDomain>().ReverseMap();
            CreateMap<Log, LogDomain>().ReverseMap();
            CreateMap<User, UserDomain>()
                .ForMember(user => user.Role, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
