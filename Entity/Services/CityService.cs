using Entity.Constants;
using Entity.Models;
using Entity.Respository.Respositories;
using Entity.Services.Interface;
using Entity.Services.ViewModels;
using System.Text.RegularExpressions;
namespace Entity.Services
{
	
	public class CityService : ICityService
	{
		ICityRepository _cityRepository;
		IWardRepository _wardRepository;
		IDistrictRespository _districtRepository;
		public CityService(ICityRepository cityRespository, IDistrictRespository districtRespository, IWardRepository wardRepository) : base()
		{
			_cityRepository = cityRespository;
			_wardRepository = wardRepository;
			_districtRepository = districtRespository;
		}
		public async Task<ResponseEntity> GetAllCity()
		{
			var cities = await _cityRepository.GetAllAsync();
			return new ResponseEntity(StatusCodeConstants.OK, cities, MessageConstants.MESSAGE_SUCCESS_200);
		}
		public async Task<ResponseEntity> GetSingleCityById(int id)
		{
			var city = await _cityRepository.GetSingleByIdAsync(c => c.Id == id);
			return new ResponseEntity(StatusCodeConstants.OK, city, MessageConstants.MESSAGE_SUCCESS_200);
		}
		public async Task<ResponseEntity> InsertCity(CityViewModel model)
		{
			try
			{
				City Cities = new City();
				Cities.CityName = model.CityName;
				await _cityRepository.InsertAsync(Cities);
				return new ResponseEntity(StatusCodeConstants.OK, Cities, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, model, MessageConstants.INSERT_ERROR);
			}
		}
		public async Task<ResponseEntity> UpdateCity(int id, CityViewModel model)
		{
			try
			{
				var singleCity = await _cityRepository.GetSingleByIdAsync(c => c.Id == id);
				if (singleCity == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "aaa", MessageConstants.MESSAGE_ERROR_400);
				}
				singleCity.CityName = model.CityName;
				await _cityRepository.UpdateAsync(singleCity, singleCity);
				return new ResponseEntity(StatusCodeConstants.OK, model, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "", MessageConstants.UPDATE_ERROR);
			}
		}
		public async Task<ResponseEntity> DeleteCity(int id)
		{
			try
			{
				var singleCity = await _cityRepository.GetSingleByIdAsync(c => c.Id == id);
				if (singleCity == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_400);
				}
				await _cityRepository.DeleteAsync(singleCity);
				return new ResponseEntity(StatusCodeConstants.OK, singleCity, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "", MessageConstants.DELETE_ERROR);
			}
		}
		public async Task<ResponseEntity> GetAllCityByCondition(int id, string name)
		{
			try
			{
				var cities = await _cityRepository.GetMultiBycondition(c => c.Id == id && c.CityName == name);
				return new ResponseEntity(StatusCodeConstants.OK, cities, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_400);
			}
		}
		public async Task<ResponseEntity> AddAdrees(InsertAddress model)
		{
			try
			{
				City cites = new City();
				cites.CityName = model.CityName;
				await _cityRepository.InsertAsync(cites);
				if (cites == null)
				{
					return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, cites, MessageConstants.INSERT_ERROR);
				}
				District districts = new District();
				districts.CityId = cites.Id;
				districts.DistictName = model.DistrictName;
				await _districtRepository.InsertAsync(districts);
				if (districts == null)
				{
					return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, districts, MessageConstants.INSERT_ERROR);
				}
				Ward wards = new Ward();
				wards.DistrictId = districts.Id;
				wards.WardName = model.WardName;
				await _wardRepository.InsertAsync(wards);
				if (wards == null)
				{
					return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, wards, MessageConstants.INSERT_ERROR);
				}
				return new ResponseEntity(StatusCodeConstants.OK, model, MessageConstants.INSERT_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.INSERT_ERROR);
			}
		}
	}
}
