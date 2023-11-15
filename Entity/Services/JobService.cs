using Entity.Constants;
using Entity.Models;
using Entity.Repository.Repositories;
using Entity.Respository.Respositories;
using Entity.Services.Interface;
using Entity.Services.ViewModels;
namespace Entity.Services
{

	public class JobService : IJobService
	{
		IJobRepository _JobRepository;
		public JobService(IJobRepository JobRepository) : base()
		{
			_JobRepository = JobRepository;
		}
		public async Task<ResponseEntity> DeleteJob(int id)
		{
			try
			{
				var Job = await _JobRepository.GetSingleByIdAsync(c => c.Id == id);
				if (Job == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, Job, MessageConstants.MESSAGE_ERROR_404);
				}
				await _JobRepository.DeleteAsync(Job);
				return new ResponseEntity(StatusCodeConstants.OK, Job, MessageConstants.DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.DELETE_ERROR);
			}
		}
		public async Task<ResponseEntity> GetAllJob()
		{
			var Jobs = await _JobRepository.GetAllAsync();
			return new ResponseEntity(StatusCodeConstants.OK, Jobs, MessageConstants.MESSAGE_SUCCESS_200);
		}
		public async Task<ResponseEntity> GetSingleJobById(int id)
		{
			try
			{
				var Job = await _JobRepository.GetSingleByIdAsync(c => c.Id == id);
				if (Job == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, Job, MessageConstants.MESSAGE_ERROR_404);
				}
				return new ResponseEntity(StatusCodeConstants.OK, Job, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.MESSAGE_ERROR_404);
			}
		}
		public async Task<ResponseEntity> InsertJob(InsertJob model)
		{
			try
			{
				Job Jobs = new Job();
				Jobs.JobName = model.JobName;
				await _JobRepository.InsertAsync(Jobs);
				if (Jobs == null)
				{
					return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, Jobs, MessageConstants.INSERT_ERROR);
				}
				return new ResponseEntity(StatusCodeConstants.OK, Jobs, MessageConstants.INSERT_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.INSERT_ERROR);
			}
		}
		public async Task<ResponseEntity> UpdateJob(UpdateJob model)
		{
			try
			{
				var Job = await _JobRepository.GetSingleByIdAsync(c => c.Id == model.Id);
				if (Job == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, Job, MessageConstants.MESSAGE_ERROR_404);
				}
				Job.Id = model.Id;
				Job.JobName = model.JobName;
				await _JobRepository.UpdateAsync(Job, Job);
				return new ResponseEntity(StatusCodeConstants.OK, model, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "", MessageConstants.UPDATE_ERROR);
			}
		}
	}
}
