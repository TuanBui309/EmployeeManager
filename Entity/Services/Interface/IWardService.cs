using Entity.Constants;
using Entity.Models;
using Entity.Services.ViewModels;

namespace Entity.Services.Interface
{
	public interface IWardService
	{
		Task<ResponseEntity> GetAllWard();
		Task<ResponseEntity> GetSingleWardById(int id);
		Task<ResponseEntity> GetMultiWardByCondition(int DistrictId);
		Task<ResponseEntity> InsertWard(WardViewModel model);
		Task<ResponseEntity> UpdateWard(WardViewModel model, int id);
		Task<ResponseEntity> DeleteWard(int id);
	}
}
