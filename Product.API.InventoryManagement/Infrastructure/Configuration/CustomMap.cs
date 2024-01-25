using AutoMapper;
using Product.API.InventoryManagement.DTO.InternalAPI.Request;
using Product.API.InventoryManagement.DTO.InternalAPI.Response;
using Product.API.InventoryManagement.Extensions;
using Product.API.InventoryManagement.Infrastructure.Entities;

namespace Product.API.InventoryManagement.Infrastructure.Configuration
{
    public class CustomMap:Profile
    {
        public CustomMap()
        {
            CreateMap<InventoryResponse, DTO.ExternalAPI.Response.InventoryResponse>().ReverseMap();

            CreateMap<InventoryDetailsRequest, DTO.ExternalAPI.Request.InventoryDetailsRequest>().ReverseMap();
            CreateMap<InventoryDetailsRequest, InventoryDetailsEntity>().ReverseMap();
            

            CreateMap<InventoryEntity, DTO.ExternalAPI.Response.InventoryResponse>().ReverseMap();
            CreateMap<InventoryEntity, InventoryResponse>().ReverseMap();

            CreateMap<InventoryDetailsEntity, DTO.ExternalAPI.Response.InventoryDetailsResponse>().ReverseMap();

        }
    }
}
