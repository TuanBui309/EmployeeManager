using Entity.Models;
using Entity.Repository.Repositories;
using Entity.Repository.Respositories;
using Entity.Respository.Respositories;
using Entity.Services.Utilities;
using Entity.Services.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
namespace Entity.Services.Validation
{
	public class EmployeeValidator : AbstractValidator<EmployeeViewModel>
	{
		IJobRepository _jobRepository;
		INationRepository _nationRepository;
		IDistrictRespository _districtRespository;
		IWardRepository _wardRepository;
		ICityRepository _cityRepository;
		IEmployeeRepository _employeeRepository;
		
		
		public EmployeeValidator(IDistrictRespository districtRespository, IWardRepository wardRepository, ICityRepository cityRepository,IEmployeeRepository employeeRepository,IJobRepository jobRepository,INationRepository nationRepository)
		{
			_cityRepository = cityRepository;
			_districtRespository = districtRespository;
			_wardRepository = wardRepository;
			_employeeRepository = employeeRepository;
			_jobRepository = jobRepository;
			_nationRepository = nationRepository;


			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
				.MaximumLength(250).WithMessage("Name can not over 250 characters");
			RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required")
				.Must(FuncUtilities.BeAValidDate).WithMessage("Invaild date (MM/dd/yyyy)!");
			RuleFor(x => x.Age).NotEmpty().WithMessage("Age is required")
				.GreaterThan(0).WithMessage("Age must be greater than 0")
				.LessThan(150).WithMessage("Age must be less than 150");
                //.
                //MustAsync((model, dateOfBirth, CancellationToken) => IsValidAge(model)).WithMessage("Age is valid");
			RuleFor(x => x.JobId).NotEmpty().WithMessage("JobId is required")
				.MustAsync((model, JobId, CancellationToken) => IsValidJobId(model)).WithMessage("JobId does not exist");
			RuleFor(x => x.NationId).NotEmpty().WithMessage("NationId is required")
				.MustAsync((model, NationId, CancellationToken) => IsValidNationId(model)).WithMessage("NationId does not exist"); 
			RuleFor(x => x.PhoneNumber)/*.NotEmpty().WithMessage("Phone number í required")*/
				.Matches(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").WithMessage("Invaild phone number");
			RuleFor(x => x.IdentityCardNumber)/*.NotEmpty().WithMessage("IdentityCardNumber is required")*/
				.MustAsync((model, IdentityCardNumber, CancellationToken) =>IsValidIdentityCardNumber(model)).WithMessage("IdentityCardNumber is  already")
				.Matches(@"^\d{12}$").WithMessage("IdentityCardNumber is required");
			RuleFor(model => model.CityId)
				.MustAsync((model, CityId, CancellationToken) => IsValidCityId(model)).WithMessage("Cityid does not exist");
			RuleFor(model => model.DistrictId)
				.MustAsync((model, DistrictId, CancellationToken) => IsValidDistrictId(model, DistrictId)).WithMessage("The district must belong to the province.");
			RuleFor(model => model.WardId)
				.MustAsync((model, ward, CancellationToken) => IsValidWardId(model, ward))
				.WithMessage("The commune must belong to the district.");
		}
		
		protected async Task<bool> IsValidCityId(EmployeeViewModel model)
		{
			var city = await _cityRepository.GetSingleByIdAsync(c => c.Id == model.CityId);
			if (city != null)
			{
				return true;
			}
			return false;
		}
		protected async Task<bool> IsValidJobId(EmployeeViewModel model)
		{
			var job = await _jobRepository.GetSingleByIdAsync(c => c.Id == model.JobId);
			if (job != null)
			{
				return true;
			}
			return false;
		}
		protected async Task<bool> IsValidNationId(EmployeeViewModel model)
		{
			var nation = await _nationRepository.GetSingleByIdAsync(c => c.Id == model.NationId);
			if (nation != null)
			{
				return true;
			}
			return false;
		}
		protected async Task<bool> IsValidDistrictId(EmployeeViewModel model, int districtId)
		{
			var district = await _districtRespository.GetSingleByIdAsync(c => c.Id == districtId);
			if (district != null)
			{
				if (model.CityId == district.CityId)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		protected async Task<bool> IsValidWardId(EmployeeViewModel model, int wardId)
		{
			var ward = await _wardRepository.GetSingleByIdAsync(c => c.Id == wardId);
			if (ward != null)
			{
				if (model.DistrictId == ward.DistrictId)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		protected async Task<bool> IsValidIdentityCardNumber(EmployeeViewModel model)
		{
			if (model.IdentityCardNumber != null)
			{

				if (model.Id == null)
				{
					var identityCardNumbers = await _employeeRepository.GetSingleByIdAsync(i => i.IdentityCardNumber == model.IdentityCardNumber);
					if (identityCardNumbers == null)
					{
						return true;
					}
					return false;
				}
				var identityCardNumber = await _employeeRepository.GetSingleByIdAsync(x => x.Id != model.Id && x.IdentityCardNumber == model.IdentityCardNumber);
				if (identityCardNumber == null)
				{
					return true;
				}
				return false;
			}
			return true;


		}
		
		protected async Task<bool> IsValidAge(EmployeeViewModel model)
		{
			var date = DateTime.Now;
			DateTime dateOfbirth = FuncUtilities.ConvertStringToDate(model.DateOfBirth);
			var age = date.Year - dateOfbirth.Year;
			if (date.Year >= dateOfbirth.Year)
			{
				if (date.Month > dateOfbirth.Month && date.Day > dateOfbirth.Day &&  model.Age==age-1)
				{
					return true;
				}
                if (date.Month <= dateOfbirth.Month && date.Day <= dateOfbirth.Day && model.Age == age)
                {
                    return true;
                }
                return false;
			}
			
			return false;
		}



	}
}
