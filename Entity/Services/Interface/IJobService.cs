using Entity.Constants;
using Entity.Models;
using Entity.Services.ViewModels;
namespace Entity.Services.Interface
{
	public interface IJobService
	{
		Task<ResponseEntity> GetAllJob();
		Task<ResponseEntity> GetSingleJobById(int id);
		Task<ResponseEntity> InsertJob(InsertJob model);
		Task<ResponseEntity> UpdateJob(UpdateJob model);
		Task<ResponseEntity> DeleteJob(int id);
	}
}
