using Entity.Constants;
using Entity.Models;
using Entity.Respository.Respositories;
using Entity.Services.Interface;
using Entity.Services.ViewModels;

namespace Entity.Services
{
	
	public class WardService : IWardService
	{
		IWardRepository _wardRepository;
		public WardService(IWardRepository wardRepository) : base()
		{
			_wardRepository = wardRepository;
		}
		public async Task<ResponseEntity> DeleteWard(int id)
		{
			try
			{
				var ward = await _wardRepository.GetSingleByIdAsync(c => c.Id == id);
				if (ward == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, ward, MessageConstants.MESSAGE_ERROR_404);
				}
				await _wardRepository.DeleteAsync(ward);
				return new ResponseEntity(StatusCodeConstants.OK, ward, MessageConstants.DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.DELETE_ERROR);
			}
		}
		public async Task<ResponseEntity> GetAllWard()
		{
			var wards = await _wardRepository.GetAllAsync();
			return new ResponseEntity(StatusCodeConstants.OK, wards, MessageConstants.MESSAGE_SUCCESS_200);
		}
		public async Task<ResponseEntity> GetMultiWardByCondition(int DistrictId)
		{
			try
			{
				var wards = await _wardRepository.GetMultiBycondition(c => c.DistrictId == DistrictId);
				if (wards == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, wards, MessageConstants.MESSAGE_ERROR_404);
				}
				return new ResponseEntity(StatusCodeConstants.OK, wards, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.MESSAGE_ERROR_404);
			}
		}
		public async Task<ResponseEntity> GetSingleWardById(int id)
		{
			try
			{
				var ward = await _wardRepository.GetSingleByIdAsync(c => c.Id == id);
				if (ward == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, ward, MessageConstants.MESSAGE_ERROR_404);
				}
				return new ResponseEntity(StatusCodeConstants.OK, ward, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.MESSAGE_ERROR_404);
			}
		}
		public async Task<ResponseEntity> InsertWard(WardViewModel model)
		{
			try
			{
				Ward wards = new Ward();
				wards.DistrictId = model.DistrictId;
				wards.WardName = model.WardName;
				await _wardRepository.InsertAsync(wards);
				if (wards == null)
				{
					return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, wards, MessageConstants.INSERT_ERROR);
				}
				return new ResponseEntity(StatusCodeConstants.OK, wards, MessageConstants.INSERT_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.INSERT_ERROR);
			}
		}
		public async Task<ResponseEntity> UpdateWard(WardViewModel model, int id)
		{
			try
			{
				var ward = await _wardRepository.GetSingleByIdAsync(c => c.Id == id);
				if (ward == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, ward, MessageConstants.MESSAGE_ERROR_404);
				}
				ward.DistrictId = model.DistrictId;
				ward.WardName = model.WardName;
				await _wardRepository.UpdateAsync(ward, ward);
				return new ResponseEntity(StatusCodeConstants.OK, model, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "", MessageConstants.UPDATE_ERROR);
			}
		}
	}
}
