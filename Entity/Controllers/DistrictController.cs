using Entity.Models;
using Entity.Services;
using Microsoft.AspNetCore.Mvc;
using Entity.Services.Interface;
using Entity.Services.ViewModels;

namespace Entity.Controllers
{
	[Route("api/[controller]")]
	public class DistrictController : Controller
	{
		IDistrictService _districtService;
		public DistrictController(IDistrictService districtService)
		{
			_districtService = districtService;
		}

		[HttpGet("GetAllDistrict")]
		public async Task<IActionResult> GetAllDistrict()
		{
			return await _districtService.GetAllDistrict();
		}

		[HttpGet("GetSingleDistrictById")]
		public async Task<IActionResult> GetSingleDistrictById(int id)
		{
			return await _districtService.GetSingleDistirctById(id);
		}

		[HttpGet("GetMultiDistrictByCondition")]
		public async Task<IActionResult> GetMultiDistrictByCondition(int proviceId)
		{
			return await _districtService.GetMultiDistrictByCondition(proviceId);
		}

		[HttpPost("InsertDistrict")]
		public async Task<IActionResult> InsertDistrict(DistrictViewModel model)
		{
			return await _districtService.InsertDistrict(model);
		}

		[HttpPut("UpdateDistrict")]
		public async Task<IActionResult> UpdateDistrict(int id,DistrictViewModel model)
		{
			return await _districtService.UpdateDistrict(model, id);
		}

		[HttpDelete("DeleteDistrict")]
		public async Task<IActionResult> DeleteDistrict(int id)
		{
			return await _districtService.DeleteDistrict(id);
		}
	}
}
