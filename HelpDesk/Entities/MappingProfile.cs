using AutoMapper;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.DataTransferObjects.ResTemplate;
using HelpDesk.Entities.DataTransferObjects.Ticket;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // format: CreateMap<Source, Destination>();

            CreateMap<CompanyModel, CompanyDto>();
            CreateMap<CompanyModel, CompanyDetailDto>();
            CreateMap<CompanyCreateDto, CompanyModel>();
            CreateMap<CompanyUpdateDto, CompanyModel>();

            CreateMap<UserRegistrationDto, UserModel>();
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>();

            CreateMap<ResTemplateModel, ResTemplateDto>();
            CreateMap<ResTemplateCreateDto, ResTemplateModel>();

            CreateMap<NotificationModel, NotificationDto>();
            CreateMap<NotificationMarkDto, NotificationModel>();

            CreateMap<TicketOperatorModel, TicketOperatorDto>();

            CreateMap<TicketTimelineModel, TicketTimelineDto>();

            CreateMap<CreateTicketDto, TicketModel>();
            CreateMap<TicketModel, TicketDto>();

            CreateMap<CategoryCreateDto, CategoryModel>();
            CreateMap<CategoryModel, CategoryDto>();
        }
    }
}
