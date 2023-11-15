using Entity.Constants;
using Entity.Models;
using Entity.Services;
using Entity.Services.Interface;
using Entity.Services.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
namespace Entity.Controllers
{
	[Route("api/[controller]")]
	public class CityController : Controller
	{
		ICityService _cityService;
		private IValidator<CityViewModel> _validator;
		public CityController(ICityService cityService, IValidator<CityViewModel> validator)
		{
			_cityService = cityService;
			_validator = validator;
		}

		[HttpGet("GetAllCity")]
		public async Task<IActionResult> GetALLCity()
		{
			var result = await _cityService.GetAllCity();
			return result;
		}

		[HttpGet("GetCityById")]
		public async Task<IActionResult> getCityById(int CityId)
		{
			return await _cityService.GetSingleCityById(CityId);
		}

		[HttpGet("GetCityByCondition")]
		public async Task<IActionResult> getCityByCondition(int CityId, string name)
		{
			return await _cityService.GetAllCityByCondition(CityId, name);
		}

		[HttpPost("InsertCity")]
		public async Task<IActionResult> InsertCity(CityViewModel city)
		{
			ValidationResult result = await _validator.ValidateAsync(city);
			if (result.IsValid)
			{
				return await _cityService.InsertCity(city);
			}
			return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, result.ToDictionary(), MessageConstants.INSERT_ERROR);
		}

		[HttpPost("InsertAddress")]
		public async Task<IActionResult> InsertAddress([FromBody] InsertAddress model)
		{
			return await _cityService.AddAdrees(model);
		}

		[HttpPut("UpdateCity")]
		public async Task<IActionResult> Put([FromBody] CityViewModel value, int id)
		{
			var result = await _cityService.UpdateCity(id, value);
			return result;
		}

		// DELETE api/<ToDoController>/5
		[HttpDelete("DeleteCity")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _cityService.DeleteCity(id);
			return result;
		}
	}
}
