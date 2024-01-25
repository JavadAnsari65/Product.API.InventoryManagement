using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.InventoryManagement.Application;

namespace Product.API.InventoryManagement.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepo _inventoryRepo;
        public InventoryController(IInventoryRepo inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

        [HttpPost]
        public ActionResult<DTO.ExternalAPI.Response.InventoryResponse> AddFactor(DTO.ExternalAPI.Request.InventoryDetailsRequest factor)
        {
            var addFactor = _inventoryRepo.AddFactor(factor);
            return Ok(addFactor);
        }

        //[HttpPost]
        //public ActionResult<DTO.ExternalAPI.Response.InventoryDetailsResponse> GetFactorByFactorId(DTO.ExternalAPI.Request.InventoryDetailsFactorIdRequest factorId)
        [HttpGet]
        public ActionResult<DTO.ExternalAPI.Response.InventoryDetailsResponse> GetFactorByFactorId(int factorId)
        {
            var factor = _inventoryRepo.FindFactorByFactorId(factorId);
            return Ok(factor);
        }

        [HttpGet]
        public ActionResult<List<DTO.ExternalAPI.Response.InventoryDetailsResponse>> GetFactorsByProductId(Guid productId)
        {
            var factors = _inventoryRepo.FindFactorsByProductId(productId);
            return Ok(factors);
        }

        [HttpDelete]
        public ActionResult<DTO.ExternalAPI.Response.InventoryResponse> DeleteFactorByFactorId(int factorId)
        {
            var inventoryResult = _inventoryRepo.DeleteFactorByFactorId(factorId);
            return Ok(inventoryResult);
        }
    }
}
