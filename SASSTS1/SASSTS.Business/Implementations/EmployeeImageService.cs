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
using SASSTS.Model.Dtos.Employee;
using SASSTS.Model.Entities;
using SASSTS.Model.RequestModels.EmployeeImagesVM;

namespace SASSTS.Business.Implementations
{
    public class EmployeeImageService : BusinessMap, IEmployeeImageService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public EmployeeImageService(IUnitWork unitWork, IMapper mapper, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _unitWork = unitWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<EmployeeImageDto>>> GetAllImagesByEmployee(GetAllEmployeeImageByEmployeeVM getByEmployeeVM)
        {
            var result = new ApiResponse<List<EmployeeImageDto>>();

            var employeeImageEntities = await _unitWork.GetRepository<Employee>().GetByFilterAsync(x => x.Id == getByEmployeeVM.EmployeeId);
            var employeeImageDtos = await employeeImageEntities.ProjectTo<EmployeeImageDto>(_mapper.ConfigurationProvider).ToListAsync();

            result.Data = employeeImageDtos;
            return ApiResponse<List<EmployeeImageDto>>.Success(StatusCodes.Status200OK, result.Data);
        }

        public async Task<ApiResponse<int>> CreateEmployeeImage(CreateEmployeeImageVM createEmployeeImageVM)
        {
            var result = new ApiResponse<int>();

            var employeeExists = await _unitWork.GetRepository<Employee>().GetSingleByFilterAsync(x => x.Id == createEmployeeImageVM.EmployeeId);
            if (employeeExists is null)
            {
                throw new NotFoundException($"{createEmployeeImageVM.EmployeeId} numaralı personel bulunamadı.");
            }
            //Dosyanın ismi belirleniyor.
            var fileName = PathUtil.GenerateFileNameFromBase64File(createEmployeeImageVM.UploadedImage, employeeExists.EmployeeName);
            var filePath = Path.Combine(_configuration["Paths:EmployeeImages"], fileName);

            //Base64 string olarak gelen dosya byte dizisine çevriliyor.
            var imageDataAsByteArray = Convert.FromBase64String(createEmployeeImageVM.UploadedImage);
            //byte dizisi FileStream'e yazmak üzere FileStream'e aktarılıyor.
            var ms = new MemoryStream(imageDataAsByteArray);
            ms.Position = 0;

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                ms.CopyTo(fs);
                fs.Close();
            }


            var employeeImageEntity = _mapper.Map<CreateEmployeeImageVM, EmployeeImage>(createEmployeeImageVM);

            employeeImageEntity.Path = $"{_configuration["Paths:EmployeeImages"]}/{fileName}";

            _unitWork.GetRepository<EmployeeImage>().Add(employeeImageEntity);
            await _unitWork.CommitAsync();

            result.Data = (int)employeeImageEntity.Id;
            return ApiResponse<int>.Success(StatusCodes.Status200OK, result.Data);
        }


        public async Task<ApiResponse<int>> DeleteEmployeeImage(DeleteEmployeeImageVM deleteEmployeeImageVM)
        {
            var result = new ApiResponse<int>();

            var existsEmployeeImage = await _unitWork.GetRepository<EmployeeImage>().GetById(deleteEmployeeImageVM.EmployeeId);
            if (existsEmployeeImage is null)
            {
                throw new NotFoundException($"{deleteEmployeeImageVM.EmployeeId} numaralı personel resmi bulunamadı.");
            }

            //Ürün resmine ait db kaydı siliniyor.
            _unitWork.GetRepository<EmployeeImage>().Delete(existsEmployeeImage);
            await _unitWork.CommitAsync();

            //Fiziksel resim dosyası siliniyor.
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, existsEmployeeImage.Path);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            result.Data = (int)existsEmployeeImage.Id;
            return ApiResponse<int>.Success(StatusCodes.Status200OK, result.Data);
        }


    }
}
