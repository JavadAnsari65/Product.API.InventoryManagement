using AutoMapper;
using Product.API.InventoryManagement.DTO.InternalAPI.Request;
using Product.API.InventoryManagement.DTO.InternalAPI.Response;
using Product.API.InventoryManagement.Extensions;
using Product.API.InventoryManagement.Infrastructure.Entities;
using Product.API.InventoryManagement.Infrastructure.Repository;
using System.Collections.Generic;

namespace Product.API.InventoryManagement.Application
{
    public class InventoryRepo:IInventoryRepo
    {
        private readonly CRUDService _crudService;
        private readonly IMapper _mapper;
        public InventoryRepo(CRUDService crudService, IMapper mapper)
        {
            _crudService = crudService;
            _mapper = mapper;
        }



        public ApiResponse<DTO.ExternalAPI.Response.InventoryResponse> AddFactor(DTO.ExternalAPI.Request.InventoryDetailsRequest factor)
        {
            try
            {
                if(!factor.IsBuy && factor.IsSell || factor.IsBuy && !factor.IsSell)
                {
                    var internalFactor = _mapper.Map<InventoryDetailsRequest>(factor);
                    var entityFactor = _mapper.Map<InventoryDetailsEntity>(internalFactor);
                    var addFactor = _crudService.AddFactorInDB(entityFactor);
                    var inventoryProduct = _mapper.Map<DTO.ExternalAPI.Response.InventoryResponse>(addFactor.Data);

                    return new ApiResponse<DTO.ExternalAPI.Response.InventoryResponse>
                    {
                        Result = true,
                        Data = inventoryProduct,
                    };
                }
                else
                {
                    return new ApiResponse<DTO.ExternalAPI.Response.InventoryResponse>
                    {
                        Result = false,
                        ErrorMessage = "Buy and Sell fields cannot be both 'true' or 'false'."
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new ApiResponse<DTO.ExternalAPI.Response.InventoryResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    
        //public ApiResponse<DTO.ExternalAPI.Response.InventoryDetailsResponse> FindFactorByFactorId(DTO.ExternalAPI.Request.InventoryDetailsFactorIdRequest factorId)
        public ApiResponse<DTO.ExternalAPI.Response.InventoryDetailsResponse> FindFactorByFactorId(int factorId)
        {
            try
            {
                //var internalDetailsFactor = _mapper.Map<InventoryDetailsFactorIdRequest>(factorId);
                //var entityDetailsFactor = _crudService.FindFactorByFactorIdInDB(internalDetailsFactor);

                var entityDetailsFactor = _crudService.FindFactorByFactorIdInDB(factorId);

                var factorResult = _mapper.Map<DTO.ExternalAPI.Response.InventoryDetailsResponse>(entityDetailsFactor.Data);

                return new ApiResponse<DTO.ExternalAPI.Response.InventoryDetailsResponse>
                {
                    Result = true,
                    Data = factorResult,
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<DTO.ExternalAPI.Response.InventoryDetailsResponse>
                {
                    Result = true,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public ApiResponse<List<DTO.ExternalAPI.Response.InventoryDetailsResponse>> FindFactorsByProductId(Guid productId)
        {
            try
            {
                var lstInventoryDetails = _crudService.FindFactorsByProductIdInDB(productId);

                var mapLstInventoryDetails = _mapper.Map<List<DTO.ExternalAPI.Response.InventoryDetailsResponse>>(lstInventoryDetails.Data);

                if (lstInventoryDetails.Result)
                {
                    return new ApiResponse<List<DTO.ExternalAPI.Response.InventoryDetailsResponse>>
                    {
                        Result = true,
                        Data = mapLstInventoryDetails
                    };
                }
                else
                {
                    return new ApiResponse<List<DTO.ExternalAPI.Response.InventoryDetailsResponse>>
                    {
                        Result = false,
                        ErrorMessage = lstInventoryDetails.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<DTO.ExternalAPI.Response.InventoryDetailsResponse>>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<DTO.ExternalAPI.Response.InventoryResponse> DeleteFactorByFactorId(int factorId)
        {
            try
            {
                var inventoryResult = _crudService.DeleteFactorByFactorIdOfDB(factorId);

                var inventoryResponse = _mapper.Map<DTO.ExternalAPI.Response.InventoryResponse>(inventoryResult.Data);

                if (inventoryResult.Result)
                {
                    return new ApiResponse<DTO.ExternalAPI.Response.InventoryResponse>
                    {
                        Result = true,
                        Data = inventoryResponse
                    };
                }
                else
                {
                    return new ApiResponse<DTO.ExternalAPI.Response.InventoryResponse>
                    {
                        Result = false,
                        ErrorMessage = inventoryResult.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<DTO.ExternalAPI.Response.InventoryResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
