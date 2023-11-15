using Entity.Models;
using Entity.Services;
using Microsoft.AspNetCore.Mvc;
using Entity.Services.Interface;
using Entity.Services.ViewModels;

namespace Entity.Controllers
{
	[Route("api/[controller]")]
	public class WardController : Controller
	{
		IWardService _wardService;
		public WardController(IWardService WardService)
		{
			_wardService = WardService;
		}

		[HttpGet("GetAllWard")]
		public async Task<IActionResult> GetAllWard()
		{
			return await _wardService.GetAllWard();
		}

		[HttpGet("GetSingleWardById")]
		public async Task<IActionResult> GetSingleWardById(int id)
		{
			return await _wardService.GetSingleWardById(id);
		}

		[HttpGet("GetMultiWardByCondition")]
		public async Task<IActionResult> GetMultiWardByCondition(int districtId)
		{
			return await _wardService.GetMultiWardByCondition(districtId);
		}

		[HttpPost("InsertWard")]
		public async Task<IActionResult> InsertWard(WardViewModel model)
		{
			return await _wardService.InsertWard(model);
		}

		[HttpPut("UpdateWard")]
		public async Task<IActionResult> UpdateWard( int id,WardViewModel model)
		{
			return await _wardService.UpdateWard(model, id);
		}

		[HttpDelete("DeleteWard")]
		public async Task<IActionResult> DeleteWard(int id)
		{
			return await _wardService.DeleteWard(id);
		}
	}
}
