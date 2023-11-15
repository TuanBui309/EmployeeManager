using Entity.Models;
using Entity.Services;
using Microsoft.AspNetCore.Mvc;
using Entity.Services.Interface;
using Entity.Services.ViewModels;

namespace Entity.Controllers
{
	[Route("api/[controller]")]
	public class JobController : Controller
	{
		IJobService _jobService;
		public JobController(IJobService jobService)
		{
			_jobService = jobService;
		}

		[HttpGet("GetAllJob")]
		public async Task<IActionResult> GetAllJob()
		{
			return await _jobService.GetAllJob();
		}

		[HttpGet("GetJobById")]
		public async Task<IActionResult> GetJobById(int id)
		{
			return await _jobService.GetSingleJobById(id);
		}

		[HttpPost("InsertJob")]
		public async Task<IActionResult> InsertJob(InsertJob model)
		{
			return await _jobService.InsertJob(model);
		}

		[HttpPut("UpdateJob")]
		public async Task<IActionResult> UpdateJob(UpdateJob model)
		{
			return await _jobService.UpdateJob(model);
		}

		[HttpDelete("DeleteJob")]
		public async Task<IActionResult> DeleteJob(int id)
		{
			return await _jobService.DeleteJob(id);
		}
	}
}
