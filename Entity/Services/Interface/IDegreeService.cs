using Entity.Constants;
using Entity.Models;
using Entity.Services.ViewModels;

namespace Entity.Services.Interface
{
	public interface IDegreeService
	{
		Task<ResponseEntity> GetAllDegree();
		Task<ResponseEntity> GetDegreeById(int id);

		Task<ResponseEntity> InsertDegree(DegreeViewModel model);
		Task<ResponseEntity> UpdateDegree( DegreeViewModel model);
		Task<ResponseEntity> DeleteDegree(int id);
	}
}
