using Entity.Constants;
using Entity.Models;
using Entity.Services;
using Entity.Services.Interface;
using Entity.Services.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Entity.Controllers
{
	[Route("api/[controller]")]
	public class DegreeController : Controller
	{
		IDegreeService _degreeService;
		private IValidator<DegreeViewModel> _validator;
		public DegreeController(IDegreeService degreeService, IValidator<DegreeViewModel> validator)
		{
			_validator = validator;
			_degreeService = degreeService;
		}
		[HttpGet("GetAllDegree")]
		public async Task<IActionResult> GetAllDegree()
		{
			return await _degreeService.GetAllDegree();
		}

		[HttpGet("GetDegreeById")]
		public async Task<IActionResult> GetDegreeById(int id)
		{
			return await _degreeService.GetDegreeById(id);
		}

		[HttpPost("InserDegree")]
		public async Task<IActionResult> InsertDegree(DegreeViewModel model)
		{
			ValidationResult result = await _validator.ValidateAsync(model);
			if (result.IsValid)
			{
				return await _degreeService.InsertDegree(model);
			}
			return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, result.ToDictionary(), MessageConstants.INSERT_ERROR);

		}

		[HttpPut("UpdateDegree")]
		public async Task<IActionResult> UpdateDegree( DegreeViewModel model)
		{
			return await _degreeService.UpdateDegree( model);

		}

		[HttpDelete("DeleteDegree")]
		public async Task<IActionResult> DeleteDegree(int id)
		{
			return await _degreeService.DeleteDegree(id);
		}
	}
}
