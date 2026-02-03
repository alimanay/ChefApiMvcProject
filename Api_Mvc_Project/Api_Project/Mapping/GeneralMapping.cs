using Api_Project.DTOs.FeatureDtos;
using Api_Project.DTOs.MessageDtos;
using Api_Project.Entities;
using AutoMapper;

namespace Api_Project.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
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
