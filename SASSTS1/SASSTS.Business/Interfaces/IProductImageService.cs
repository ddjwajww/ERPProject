using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Product;
using SASSTS.Model.RequestModels.ProductImagesVM;

namespace SASSTS.Business.Interfaces
{
    public interface IProductImageService
    {
        
        Task<ApiResponse<List<ProductImageDto>>> GetAllImagesByProduct(GetAllProductImageByProductVM getByProductVM);
        Task<ApiResponse<int>> CreateProductImage(CreateProductImageVM createProductImageVM);
        Task<ApiResponse<int>> DeleteProductImage(DeleteProductImageVM deleteProductImageVM);

    }
}
