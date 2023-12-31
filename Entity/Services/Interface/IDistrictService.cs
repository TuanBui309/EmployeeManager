﻿using Entity.Constants;
using Entity.Models;
using Entity.Services.ViewModels;
namespace Entity.Services.Interface
{
	public interface IDistrictService
	{
		Task<ResponseEntity> GetAllDistrict();
		Task<ResponseEntity> GetSingleDistirctById(int id);
		Task<ResponseEntity> GetMultiDistrictByCondition(int proviceId);
		Task<ResponseEntity> InsertDistrict(DistrictViewModel model);
		Task<ResponseEntity> UpdateDistrict(DistrictViewModel model, int id);
		Task<ResponseEntity> DeleteDistrict(int id);
	}
}
