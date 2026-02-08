using Api_Project.DTOs.ContactDtos;
using Api_Project.DTOs.FeatureDtos;
using Api_Project.DTOs.MessageDtos;
using Api_Project.Entities;
using AutoMapper;
using System.Reflection;

namespace Api_Project.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //Contact Mapping 
            CreateMap<Contact,ResultContactDto>().ReverseMap(); 
            CreateMap<Contact,CreateContactDto>().ReverseMap(); 
            CreateMap<Contact,UpdateContactDto>().ReverseMap(); 
            CreateMap<Contact,GetByIdContactDto>().ReverseMap(); 

            //Feature Mapping
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();

            //Message Mapping
            CreateMap<Message,ResultMessageDto>().ReverseMap(); 
            CreateMap<Message,CreateMessageDto>().ReverseMap(); 
            CreateMap<Message,UpdateMessageDto>().ReverseMap(); 
            CreateMap<Message,GetByIdMessageDto>().ReverseMap();
            


        }
    }
}
