using Entity.Constants;
using Entity.Models;
using Entity.Services.ViewModels;

namespace Entity.Services.Interface
{
	public interface INationService
	{
		Task<ResponseEntity> GetAllNation();
		Task<ResponseEntity> GetSingleNationById(int id);
		Task<ResponseEntity> InsertNation(InsertNation model);
		Task<ResponseEntity> UpdateNation(UpdateNation model);
		Task<ResponseEntity> DeleteNation(int id);
		
	}
}
