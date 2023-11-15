using Azure.Messaging;
using Entity.Constants;
using Entity.Models;
using Entity.Pagination;
using Entity.Repository.Repositories;
using Entity.Repository.Respositories;
using Entity.Respository.Respositories;
using Entity.Services.Interface;
using Entity.Services.Utilities;
using Entity.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Security.Cryptography.Xml;
using System.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Entity.Services
{

	public class EmployeeService : IEmployeeService
	{
		IEmployeeRepository _employeeRepository;
		IJobRepository _jobRepository;
		INationRepository _nationRepository;
		ICityRepository _cityRepository;
		IDistrictRespository _districtRespository;
		IWardRepository _wardRepository;
		public string[] FormatDate = new string[] { "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy" };

		//contructor injection
		public EmployeeService(IEmployeeRepository employeeRepository, IJobRepository jobRepository, INationRepository nationRepository, ICityRepository cityRepository, IDistrictRespository districtRespository, IWardRepository wardRepository) : base()
		{

			
			_employeeRepository = employeeRepository;
			_jobRepository = jobRepository;
			_cityRepository = cityRepository;
			_districtRespository = districtRespository;
			_wardRepository = wardRepository;
			_nationRepository = nationRepository;

		}
		public async Task<ResponseEntity> DeleteEmployee(int id)
		{
			try
			{
				var employee = await _employeeRepository.GetSingleByIdAsync(c => c.Id == id);
				if (employee == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
				}
				await _employeeRepository.DeleteAsync(employee);
				return new ResponseEntity(StatusCodeConstants.OK, employee, MessageConstants.DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.DELETE_ERROR);
			}
		}

		public byte[] DownloadReport(string keyWord)
		{

			string reportname = $"User_Wise_{Guid.NewGuid():N}.xlsx";
			var entity = GetEmployeeByKeyWord(keyWord);
			var exportbytes = _employeeRepository.ExporttoExcel<EmployeeViewExport>(entity, reportname);
			return exportbytes;
		}

		private List<EmployeeViewExport> GetEmloyee()
		{
			var lsteEmlpoyee = _employeeRepository.GetAllAsync().Result;
			var listResult = new List<EmployeeViewExport>();
			foreach (var n in lsteEmlpoyee)
			{
				var result = new EmployeeViewExport
				{
					Id = n.Id,
					Name = n.Name,
					DateOfBirth = FuncUtilities.ConvertDateToString(n.DateOfBirth)
					,
					Age = n.Age,
					JobName = _jobRepository.GetSingleByIdAsync(x => x.Id == n.JobId).Result.JobName
					,
					NationName = _nationRepository.GetSingleByIdAsync(x => x.Id == n.NationId).Result.NationName
					,
					IdentityCardNumber = n.IdentityCardNumber
					,
					PhoneNumber = n.PhoneNumber
					,
					CityName = _cityRepository.GetSingleByIdAsync(x => x.Id == n.CityId).Result.CityName
					,
					DistrictName = _districtRespository.GetSingleByIdAsync(x => x.Id == n.DistrictId).Result.DistictName
					,
					WardName = _wardRepository.GetSingleByIdAsync(x => x.Id == n.WardId).Result.WardName
				};
				listResult.Add(result);
			}
			return listResult;
		}

		public async Task<ResponseEntity> GetAllEmployee(string keyWord = "", int soTrang = 1, int soPhanTuTrenTrang = 10)
		{
			var employees = GetEmployeeByKeyWord(keyWord);
			PaginationSet<EmployeeViewExport> result = new PaginationSet<EmployeeViewExport>();
			result.CurrentPage = soTrang;
			result.TotalPages = (employees.Count() / soPhanTuTrenTrang) + 1;
			result.Items = employees.Skip((soTrang - 1) * soPhanTuTrenTrang).Take(soPhanTuTrenTrang);
			result.TotalCount = employees.Count();
			return new ResponseEntity(StatusCodeConstants.OK, result, MessageConstants.MESSAGE_SUCCESS_200);

		}
		public async Task<ResponseEntity> GetEmployeeById(int id)
		{
			try
			{
				var employee = await _employeeRepository.GetSingleByIdAsync(c => c.Id == id);
				if (employee == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
				}
				return new ResponseEntity(StatusCodeConstants.OK, employee, MessageConstants.DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.DELETE_ERROR);
			}
		}
		public async Task<ResponseEntity> InsertEmployee(EmployeeViewModel model)
		{
			try
			{
				Employee employee = new Employee();
				employee.Name = model.Name;
				employee.DateOfBirth = FuncUtilities.ConvertStringToDate(model.DateOfBirth);
				employee.Age = model.Age;
				employee.JobId = model.JobId;
				employee.NationId = model.NationId;
				employee.PhoneNumber = model.PhoneNumber;
				employee.IdentityCardNumber = model.IdentityCardNumber;
				employee.CityId = model.CityId;
				employee.DistrictId = model.DistrictId;
				employee.WardId = model.WardId;
				await _employeeRepository.InsertAsync(employee);
				if (employee == null)
				{
					return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, employee, MessageConstants.INSERT_ERROR);
				}
				return new ResponseEntity(StatusCodeConstants.OK, employee, MessageConstants.INSERT_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.INSERT_ERROR);
			}
		}
		public async Task<ResponseEntity> UpdateEmployee(EmployeeViewModel model)
		{
			try
			{
				var employee = await _employeeRepository.GetSingleByIdAsync(c => c.Id == model.Id);
				if (employee == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
				}
				employee.Name = model.Name;
				employee.DateOfBirth = FuncUtilities.ConvertStringToDate(model.DateOfBirth);
				employee.Age = model.Age;
				employee.JobId = model.JobId;
				employee.NationId = model.NationId;
				employee.PhoneNumber = model.PhoneNumber;
				employee.IdentityCardNumber = model.IdentityCardNumber;
				employee.CityId = model.CityId;
				employee.DistrictId = model.DistrictId;
				employee.WardId = model.WardId;
				await _employeeRepository.UpdateAsync(employee, employee);
				return new ResponseEntity(StatusCodeConstants.OK, employee, MessageConstants.INSERT_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.INSERT_ERROR);
			}
		}
		public List<EmployeeViewModel> ReadEmployeeFromExcel(string fullPath)
		{
			try
			{
				using (var package = new ExcelPackage(new FileInfo(fullPath)))
				{
					var currentSheet = package.Workbook.Worksheets;
					var workSheet = currentSheet.First();
					List<EmployeeViewModel> listEmployee = new List<EmployeeViewModel>();

					for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
					{
						EmployeeViewModel employeeView = new EmployeeViewModel();

						employeeView.Name = workSheet.Cells[i, 2].Value.ToString();
						employeeView.DateOfBirth = workSheet.Cells[i, 3].Value.ToString();
						int.TryParse(workSheet.Cells[i, 4].Value.ToString(), out int age);
						employeeView.Age = age;
						int.TryParse(workSheet.Cells[i, 5].Value.ToString(), out int jobId);
						employeeView.JobId = jobId;
						int.TryParse(workSheet.Cells[i, 6].Value.ToString(), out int nationId);
						employeeView.NationId = nationId;
						employeeView.IdentityCardNumber = workSheet.Cells[i, 7].Value.ToString();
						employeeView.PhoneNumber = workSheet.Cells[i, 8].Value.ToString();
						int.TryParse(workSheet.Cells[i, 9].Value.ToString(), out int cityId);
						employeeView.CityId = cityId;
						int.TryParse(workSheet.Cells[i, 10].Value.ToString(), out int districtId);
						employeeView.DistrictId = districtId;
						int.TryParse(workSheet.Cells[i, 11].Value.ToString(), out int wardId);
						employeeView.WardId = wardId;

						listEmployee.Add(employeeView);
					}
					return listEmployee;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private IEnumerable<EmployeeViewExport> GetEmployeeByKeyWord(string keyWord)
		{

			IEnumerable<EmployeeViewExport> entity = GetEmloyee();

			if (entity.Count() != 0)
			{

				if (keyWord != null)
				{
					List<EmployeeViewExport> lstGetByEmployeeName = entity.Where(n => n.Name.Trim().ToLower().Contains(keyWord.Trim().ToLower())).ToList();
					List<EmployeeViewExport> lstGetByNationName = entity.Where(n => n.NationName.Trim().ToLower().Contains(keyWord.Trim().ToLower())).ToList();
					List<EmployeeViewExport> lstGetByJobName = entity.Where(n => n.JobName.Trim().ToLower().Contains(keyWord.Trim().ToLower())).ToList();
					List<EmployeeViewExport> lstGetByCityName = entity.Where(n => n.CityName.Trim().ToLower().Contains(keyWord.Trim().ToLower())).ToList();
					List<EmployeeViewExport> lstGetByDistrictName = entity.Where(n => n.DistrictName.Trim().ToLower().Contains(keyWord.Trim().ToLower())).ToList();
					List<EmployeeViewExport> lstGetByWardName = entity.Where(n => n.WardName.Trim().ToLower().Contains(keyWord.Trim().ToLower())).ToList();
					IEnumerable<EmployeeViewExport> result = new List<EmployeeViewExport>();
					result = result.Union(lstGetByEmployeeName);
					result = result.Union(lstGetByNationName);
					result = result.Union(lstGetByJobName);
					result = result.Union(lstGetByCityName);
					result = result.Union(lstGetByDistrictName);
					result = result.Union(lstGetByWardName);
					return result;
				}
				return entity;

			}
			return entity;

		}
	}
}

