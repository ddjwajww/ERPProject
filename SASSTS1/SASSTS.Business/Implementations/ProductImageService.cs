using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SASSTS.Business.CustomExceptions;
using SASSTS.Business.FileProcess;
using SASSTS.Business.Interfaces;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Product;
using SASSTS.Model.Entities;
using SASSTS.Model.RequestModels.ProductImagesVM;

namespace SASSTS.Business.Implementations
{
    public class ProductImageService : BusinessMap, IProductImageService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public ProductImageService(IUnitWork unitWork, IMapper mapper, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _unitWork = unitWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<ProductImageDto>>> GetAllImagesByProduct(GetAllProductImageByProductVM getByProductVM)
        {
            var result = new ApiResponse<List<ProductImageDto>>();

            var productImageEntities = await _unitWork.GetRepository<Product>().GetByFilterAsync(x => x.Id == getByProductVM.ProductId);
            var productImageDtos = await productImageEntities.ProjectTo<ProductImageDto>(_mapper.ConfigurationProvider).ToListAsync();

            result.Data = productImageDtos;
            return ApiResponse<List<ProductImageDto>>.Success(StatusCodes.Status200OK, result.Data);
        }

        public async Task<ApiResponse<int>> CreateProductImage(CreateProductImageVM createProductImageVM)
        {
            var result = new ApiResponse<int>();

            var productExists = await _unitWork.GetRepository<Product>().GetSingleByFilterAsync(x => x.Id == createProductImageVM.ProductId);
            if (productExists is null)
            {
                throw new NotFoundException($"{createProductImageVM.ProductId} numaralı ürün bulunamadı.");
            }
            //Dosyanın ismi belirleniyor.
            var fileName = PathUtil.GenerateFileNameFromBase64File(createProductImageVM.UploadedImage, productExists.ProductName);
            var filePath = Path.Combine( _configuration["Paths:ProductImages"], fileName);

            //Base64 string olarak gelen dosya byte dizisine çevriliyor.
            var imageDataAsByteArray = Convert.FromBase64String(createProductImageVM.UploadedImage);
            //byte dizisi FileStream'e yazmak üzere FileStream'e aktarılıyor.
            var ms = new MemoryStream(imageDataAsByteArray);
            ms.Position = 0;

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                ms.CopyTo(fs);
                fs.Close();
            }


            var productImageEntity = _mapper.Map<CreateProductImageVM, ProductImage>(createProductImageVM);

            productImageEntity.Path = $"{_configuration["Paths:ProductImages"]}/{fileName}";

            _unitWork.GetRepository<ProductImage>().Add(productImageEntity);
            await _unitWork.CommitAsync();

            result.Data = (int)productImageEntity.Id;
            return ApiResponse<int>.Success(StatusCodes.Status200OK, result.Data);
        }


        public async Task<ApiResponse<int>> DeleteProductImage(DeleteProductImageVM deleteProductImageVM)
        {
            var result = new ApiResponse<int>();

            var existsProductImage = await _unitWork.GetRepository<ProductImage>().GetById(deleteProductImageVM.ProductId);
            if (existsProductImage is null)
            {
                throw new NotFoundException($"{deleteProductImageVM.ProductId} numaralı ürün resmi bulunamadı.");
            }

            //Ürün resmine ait db kaydı siliniyor.
            _unitWork.GetRepository<ProductImage>().Delete(existsProductImage);
            await _unitWork.CommitAsync();

            //Fiziksel resim dosyası siliniyor.
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, existsProductImage.Path);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            result.Data = (int)existsProductImage.Id;
            return ApiResponse<int>.Success(StatusCodes.Status200OK, result.Data);
        }
    }
}