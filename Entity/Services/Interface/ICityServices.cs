﻿using Entity.Constants;
using Entity.Services.ViewModels;
using Entity.Models;

namespace Entity.Services.Interface
{
	public interface ICityService
	{
		Task<ResponseEntity> GetAllCity();
		Task<ResponseEntity> GetSingleCityById(int id);
		Task<ResponseEntity> InsertCity(CityViewModel city);
		Task<ResponseEntity> UpdateCity(int id, CityViewModel model);
		Task<ResponseEntity> DeleteCity(int id);
		Task<ResponseEntity> GetAllCityByCondition(int id, string name);
		Task<ResponseEntity> AddAdrees(InsertAddress model);
	}
}
