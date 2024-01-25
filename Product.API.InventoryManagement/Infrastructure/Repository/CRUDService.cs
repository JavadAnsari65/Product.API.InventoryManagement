using Product.API.InventoryManagement.DTO.InternalAPI.Request;
using Product.API.InventoryManagement.Extensions;
using Product.API.InventoryManagement.Infrastructure.Configuration;
using Product.API.InventoryManagement.Infrastructure.Entities;
using System.Numerics;

namespace Product.API.InventoryManagement.Infrastructure.Repository
{
    public class CRUDService
    {
        private readonly InventoryDbContext _dbContext;
        public CRUDService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ApiResponse<InventoryEntity> FindInventoryProductByProductId(Guid productId)
        {
            try
            {
                var inventoryProduct = _dbContext.InventoryProducts.FirstOrDefault(x => x.ProductId == productId);

                if (inventoryProduct != null)
                {
                    return new ApiResponse<InventoryEntity>
                    {
                        Result = true,
                        Data = inventoryProduct
                    };
                }
                else
                {
                    return new ApiResponse<InventoryEntity>
                    {
                        Result = false,
                        ErrorMessage = "NotFound"
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }


        private ApiResponse<InventoryEntity> BuyProduct(InventoryDetailsEntity factor)
        {
            try
            {
                var existInventoryProduct = FindInventoryProductByProductId(factor.ProductId);


                if (!existInventoryProduct.Result)
                {
                    return new ApiResponse<InventoryEntity>
                    {
                        Result = false,
                        ErrorMessage = existInventoryProduct.ErrorMessage
                    };
                }

                existInventoryProduct.Data.StockQuantity += factor.Quantity;
                existInventoryProduct.Data.LastDateUpdate = DateTime.Now;
                _dbContext.SaveChanges();

                return new ApiResponse<InventoryEntity>
                {
                    Result = true,
                    Data = existInventoryProduct.Data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private ApiResponse<InventoryEntity> SellProduct(InventoryDetailsEntity factor)
        {
            try
            {
                var existInventoryProduct = FindInventoryProductByProductId(factor.ProductId);

                if(existInventoryProduct.Data.StockQuantity >= factor.Quantity)
                {
                    existInventoryProduct.Data.StockQuantity -= factor.Quantity;
                    existInventoryProduct.Data.LastDateUpdate = DateTime.Now;
                    _dbContext.SaveChanges();

                    return new ApiResponse<InventoryEntity>
                    {
                        Result = true,
                        Data = existInventoryProduct.Data
                    };
                }
                else
                {
                    return new ApiResponse<InventoryEntity>
                    {
                        Result = false,
                        ErrorMessage = "The product stock is less than the requested quantity"
                    };
                }
                

                
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private ApiResponse<InventoryEntity> RegisterFirstFactorForProduct(InventoryDetailsEntity factor)
        {
            try
            {
                if (factor.IsBuy == true && factor.IsSell == false)
                {
                    var inventoryProduct = new InventoryEntity()
                    {
                        ProductId = factor.ProductId,
                        StockQuantity = factor.Quantity,
                        LastDateUpdate = DateTime.Now,
                    };
                    _dbContext.InventoryProducts.Add(inventoryProduct);
                    _dbContext.SaveChanges();

                    return new ApiResponse<InventoryEntity>
                    {
                        Result = true,
                        Data = inventoryProduct
                    };
                }
                else //It Means if(factor.IsBuy == false && factor.IsSell == true)
                {
                    return new ApiResponse<InventoryEntity>
                    {
                        Result = false,
                        ErrorMessage = "The previous stock of this product is 0 and you cannot register a sales invoice for it."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<InventoryEntity> AddFactorInDB(InventoryDetailsEntity factor)
        {
            var addResult = new ApiResponse<InventoryEntity>();
            try
            {
                //افزودن رکورد در جدول توضیحات
                _dbContext.InventoryDetails.Add(factor);

                //جستجو برای محصول مورد نظر در جدول موجودیها
                var existInventoryProduct = FindInventoryProductByProductId(factor.ProductId);

                if(existInventoryProduct.Result == true)
                {
                    //اگر فاکتور خرید است
                    if (factor.IsBuy == true && factor.IsSell == false)
                    {
                        addResult = BuyProduct(factor);

                        return new ApiResponse<InventoryEntity>
                        {
                            Result = true,
                            Data = addResult.Data
                        };
                    }
                    //اگر فاکتور فروش است
                    else   //It Means : if(factor.IsSell == true && factor.IsBuy == false)
                    {
                        addResult = SellProduct(factor);

                        //اگرموجودی کافی است
                        if(addResult.Result == true)
                        {
                            return new ApiResponse<InventoryEntity>
                            {
                                Result = true,
                                Data = addResult.Data
                            };
                        }
                        //درغیر اینصورت
                        else
                        {
                            return new ApiResponse<InventoryEntity>
                            {
                                Result = false,
                                ErrorMessage = addResult.ErrorMessage
                            };
                        }  
                    }
                }
                else if (existInventoryProduct.Result == false && existInventoryProduct.ErrorMessage == "NotFound")
                {
                    var register = RegisterFirstFactorForProduct(factor);
                    addResult.Result = register.Result;
                    addResult.Data = register.Data;
                    addResult.ErrorMessage = register.ErrorMessage;

                    return new ApiResponse<InventoryEntity>
                    {
                        Result = true,
                        Data = addResult.Data
                    };
                }
                else
                {
                    return new ApiResponse<InventoryEntity>
                    {
                        Result = false,
                        ErrorMessage = "Input data is Invalid."
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        //public ApiResponse<InventoryDetailsEntity> FindFactorByFactorIdInDB(InventoryDetailsFactorIdRequest factorId)
        public ApiResponse<InventoryDetailsEntity> FindFactorByFactorIdInDB(int factorId)
        {
            try
            {
                //var factor = _dbContext.InventoryDetails.FirstOrDefault(d => d.FactorId == factorId.FactorId);
                var factor = _dbContext.InventoryDetails.FirstOrDefault(d => d.FactorId == factorId);

                if (factor != null)
                {
                    return new ApiResponse<InventoryDetailsEntity>
                    {
                        Result = true,
                        Data = factor
                    };
                }
                else
                {
                    return new ApiResponse<InventoryDetailsEntity>
                    {
                        Result = false,
                        ErrorMessage = "NotFound"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryDetailsEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<List<InventoryDetailsEntity>> FindFactorsByProductIdInDB(Guid productId)
        {
            try
            {
                var lstInventoryDetails = _dbContext.InventoryDetails
                    .Where(d => d.ProductId == productId)
                    .ToList();

                if(lstInventoryDetails.Count > 0)
                {
                    return new ApiResponse<List<InventoryDetailsEntity>>
                    {
                        Result = true,
                        Data = lstInventoryDetails
                    };
                }
                else
                {
                    return new ApiResponse<List<InventoryDetailsEntity>>
                    {
                        Result = false,
                        ErrorMessage = "NotFound"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<InventoryDetailsEntity>>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<InventoryEntity> DeleteFactorByFactorIdOfDB(int factorId)
        {
            int productQuantity = 0;
            try
            {
                var inventoryDetail = _dbContext.InventoryDetails.FirstOrDefault(x => x.FactorId == factorId);

                if(inventoryDetail != null)
                {
                    _dbContext.InventoryDetails.Remove(inventoryDetail);
                    
                    var productId = inventoryDetail.ProductId;
                    var product = _dbContext.InventoryProducts.FirstOrDefault(p=>p.ProductId == productId);

                    //برای تغییر موجودی محصول بعد از حذف فاکتور
                    if (inventoryDetail.IsBuy)
                    {
                        product.StockQuantity -= inventoryDetail.Quantity;
                        product.LastDateUpdate = DateTime.Now;
                        _dbContext.SaveChanges();
                    }
                    else  //It Means: if(inventoryDetail.IsSell==true)
                    {
                        product.StockQuantity += inventoryDetail.Quantity;
                        product.LastDateUpdate = DateTime.Now;
                        _dbContext.SaveChanges();
                    }

                    return new ApiResponse<InventoryEntity>
                    {
                        Result = true,
                        Data = product
                    };
                }
                else
                {
                    return new ApiResponse<InventoryEntity>
                    {
                        Result = false,
                        ErrorMessage = "NotFound"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
