using Entity.Constants;
using Entity.Models;
using Entity.Services;
using Entity.Services.Validation;
using FluentValidation;
using FluentValidation.Results;
using Entity.Services.Interface;

using Microsoft.AspNetCore.Mvc;
using Entity.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Text;
using OfficeOpenXml.DataValidation;

namespace Entity.Controllers
{
	
	public class EmployeeController : Controller
	{
		IEmployeeService _employeeService;
		private IValidator<EmployeeViewModel> _validator;
		public EmployeeController(IEmployeeService employeeService, IValidator<EmployeeViewModel> validations)
		{
			_employeeService = employeeService;
			_validator = validations;
		}

		[HttpGet("GetAllEmployee")]
		public async Task<IActionResult> GetAllEmployee(string keyWord = "", int soTrang = 1, int soPhanTuTrenTrang = 10)
		{
			return await _employeeService.GetAllEmployee(keyWord, soTrang, soPhanTuTrenTrang);
		}

		[HttpGet("GetEmployeeById")]
		public async Task<IActionResult> GetEmplyeeById(int id)
		{
			return await _employeeService.GetEmployeeById(id);
		}

		[HttpPost("InsertEmployee")]
		public async Task<IActionResult> InsertEmployee(EmployeeViewModel model)
		{
			ValidationResult result = await _validator.ValidateAsync(model);
			if (result.IsValid)
			{
				return await _employeeService.InsertEmployee(model);
			}
			return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, result.ToDictionary(), MessageConstants.INSERT_ERROR);
		}

		[HttpPut("UpdateEmployee")]
		public async Task<IActionResult> UpdateEmployee( EmployeeViewModel  model )
		{
			ValidationResult result = await _validator.ValidateAsync(model);
			if (result.IsValid)
			{
				return await _employeeService.UpdateEmployee(model);
			}
			return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, result.ToDictionary(), MessageConstants.INSERT_ERROR);
		}

		[HttpDelete("DeleteEmployee")]
		public async Task<IActionResult> DeleteEmployee(int id)
		{
			return await _employeeService.DeleteEmployee(id);
		}

		[HttpPost("ExportToExcel")]
		public async Task<IActionResult> Download(string keyWord)
		{

			
			var exportbytes = _employeeService.DownloadReport(keyWord);
			return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "employee.xlsx");

		}

		[HttpPost("ImportData")]
		public async Task<IActionResult> UploadExcel(IFormFile file)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			if (file != null && file.Length > 0)
			{
				var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";

				if (!Directory.Exists(uploadsFolder))
				{
					Directory.CreateDirectory(uploadsFolder);
				}

				var filePath = Path.Combine(uploadsFolder, file.FileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				var excelData = _employeeService.ReadEmployeeFromExcel(filePath);

				if (excelData.Count() > 0)
				{
					
					int errorCount = 0;
					for (int i = 0; i < excelData.Count; i++)
					{
						ValidationResult result = await _validator.ValidateAsync(excelData[i]);
						if (!result.IsValid)
						{
							errorCount++;
							
							return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, result.ToDictionary(), $"Lỗi xảy ra ở dòng thứ {i + 1}");
						}
						await _employeeService.InsertEmployee(excelData[i]);
					}
					
					//int i = 0;
					//while (i < excelData.Count)
					//{
					//	ValidationResult result = await _validator.ValidateAsync(excelData[i]);
					//	if (!result.IsValid)
					//	{
					//		

					//		return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, result.ToDictionary(), $"Lỗi xảy ra ở dòng thứ {i + 1}");
					//	}
					//	await _employeeService.InsertEmployee(excelData[i]);
					//	i++;
					//}


				}

				return new ResponseEntity(StatusCodeConstants.OK, "Added all data from the file successfully", MessageConstants.INSERT_SUCCESS);

			}
			return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "", MessageConstants.INSERT_ERROR);
		}
	}
}