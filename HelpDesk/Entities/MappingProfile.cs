using AutoMapper;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.DataTransferObjects.ResTemplate;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyModel, CompanyDto>();
            CreateMap<CompanyModel, CompanyDetailDto>();
            CreateMap<CompanyCreateDto, CompanyModel>();
            CreateMap<CompanyUpdateDto, CompanyModel>();

            CreateMap<UserRegistrationDto, UserModel>();

            CreateMap<ResTemplateModel, ResTemplateDto>();
            CreateMap<ResTemplateCreateDto, ResTemplateModel>();

            CreateMap<NotificationModel, NotificationDto>();
            CreateMap<NotificationMarkDto, NotificationModel>();
        }
    }
}
