using Entity.Constants;
using Entity.Models;
using Entity.Respository.Respositories;
using Entity.Services.Interface;
using Entity.Services.ViewModels;
namespace Entity.Services
{

	public class DistrictService : IDistrictService
	{
		IDistrictRespository _districtRepository;
		public DistrictService(IDistrictRespository districtRespository) : base()
		{
			_districtRepository = districtRespository;
		}
		public async Task<ResponseEntity> DeleteDistrict(int id)
		{
			try
			{
				var district = await _districtRepository.GetSingleByIdAsync(d => d.Id == id);
				if (district == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
				}
				await _districtRepository.DeleteAsync(district);
				return new ResponseEntity(StatusCodeConstants.OK, district, MessageConstants.DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.DELETE_ERROR);
			}
		}
		public async Task<ResponseEntity> GetAllDistrict()
		{
			try
			{
				var districts = await _districtRepository.GetAllAsync();
				return new ResponseEntity(StatusCodeConstants.OK, districts, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_400);
			}
		}
		public async Task<ResponseEntity> GetMultiDistrictByCondition(int cityId)
		{
			try
			{
				var districts = await _districtRepository.GetMultiBycondition(c => c.CityId == cityId);
				return new ResponseEntity(StatusCodeConstants.OK, districts, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.MESSAGE_ERROR_404);
			}
		}
		public async Task<ResponseEntity> GetSingleDistirctById(int id)
		{
			var district = await _districtRepository.GetSingleByIdAsync(d => d.Id == id);
			return new ResponseEntity(StatusCodeConstants.OK, district, MessageConstants.MESSAGE_SUCCESS_200);
		}
		public async Task<ResponseEntity> InsertDistrict(DistrictViewModel model)
		{
			try
			{
				District districts = new District();
				districts.CityId = model.CityId;
				districts.DistictName = model.DistictName;
				await _districtRepository.InsertAsync(districts);
				if (districts == null)
				{
					return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, districts, MessageConstants.INSERT_ERROR);
				}
				return new ResponseEntity(StatusCodeConstants.OK, districts, MessageConstants.INSERT_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.INSERT_ERROR);
			}
		}
		public async Task<ResponseEntity> UpdateDistrict(DistrictViewModel model, int id)
		{
			try
			{
				var district = await _districtRepository.GetSingleByIdAsync(d => d.Id == id);
				if (district == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
				}
				district.CityId = model.CityId;
				district.DistictName = model.DistictName;
				await _districtRepository.UpdateAsync(district, district);
				return new ResponseEntity(StatusCodeConstants.OK, district, MessageConstants.UPDATE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.UPDATE_ERROR);
			}
		}
	}
}
