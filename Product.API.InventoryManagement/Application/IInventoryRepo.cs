using Product.API.InventoryManagement.Extensions;

namespace Product.API.InventoryManagement.Application
{
    public interface IInventoryRepo
    {
        public ApiResponse<DTO.ExternalAPI.Response.InventoryResponse> AddFactor(DTO.ExternalAPI.Request.InventoryDetailsRequest factor);
        
        //public ApiResponse<DTO.ExternalAPI.Response.InventoryDetailsResponse> FindFactorByFactorId(DTO.ExternalAPI.Request.InventoryDetailsFactorIdRequest factorId);
        public ApiResponse<DTO.ExternalAPI.Response.InventoryDetailsResponse> FindFactorByFactorId(int factorId);

        public ApiResponse<List<DTO.ExternalAPI.Response.InventoryDetailsResponse>> FindFactorsByProductId(Guid productId);

        public ApiResponse<DTO.ExternalAPI.Response.InventoryResponse> DeleteFactorByFactorId(int factorId);
    }
}
