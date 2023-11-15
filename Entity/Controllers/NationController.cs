using Entity.Models;
using Entity.Services;
using Microsoft.AspNetCore.Mvc;
using Entity.Services.Interface;
using Entity.Services.ViewModels;

namespace Entity.Controllers
{
	[Route("api/[controller]")]
	public class NationController : Controller
	{
		INationService _nationService;
		public NationController(INationService nationService)
		{
			_nationService = nationService;
		}

		[HttpGet("GetAllNation")]
		public async Task<IActionResult> GetAllNation()
		{
			return await _nationService.GetAllNation();
		}

		[HttpGet("GetNationById")]
		public async Task<IActionResult> GetNationById(int id)
		{
			return await _nationService.GetSingleNationById(id);
		}

		[HttpPost("InsertNation")]
		public async Task<IActionResult> InsertNation(InsertNation model)
		{
			return await _nationService.InsertNation(model);
		}

		[HttpPut("UpdateNation")]
		public async Task<IActionResult> UpdateNation(UpdateNation model)
		{
			return await _nationService.UpdateNation(model);
		}

		[HttpDelete("DeleteNation")]
		public async Task<IActionResult> DeleteNation(int id)
		{
			return await _nationService.DeleteNation(id);
		}
	}
}
