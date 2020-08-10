using AutoMapper;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.DataTransferObjects.Product;
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
            CreateMap<TicketDto , TicketModel>();

            CreateMap<CategoryCreateDto, CategoryModel>();
            CreateMap<CategoryModel , CategoryCreateDto>();
            CreateMap<CategoryModel, CategoryDto>();
            CreateMap<CategoryDto , CategoryModel >();

            CreateMap<ModuleCreateDto, ModuleModel>();
            CreateMap<ModuleModel , ModuleCreateDto >();
            CreateMap<ModuleModel, ModuleDto>();
            CreateMap<ModuleDto , ModuleModel>();

            CreateMap<ProductModel, ProductCreateDto>();
            CreateMap<ProductCreateDto ,ProductModel>();
            CreateMap<ProductDto ,ProductModel>();
            CreateMap<ProductModel , ProductDto>();



            CreateMap<BrandCreateDto, CompanyBrandModel>();
            CreateMap<CompanyBrandModel , BrandCreateDto>();
            CreateMap<CompanyBrandModel, BrandDto>();
            CreateMap<BrandDto , CompanyBrandModel>();
            CreateMap<BrandUpdateDto, CompanyBrandModel>();
        }
    }
}
