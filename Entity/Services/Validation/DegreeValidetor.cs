using Entity.Repository.Repositories;
using Entity.Services.Utilities;
using Entity.Services.ViewModels;
using FluentValidation;
using System.Globalization;

namespace Entity.Services.Validation
{
	public class DegreeValidetor : AbstractValidator<DegreeViewModel>
	{
		IDegreeRepository _degreeRepository;
		public DegreeValidetor(IDegreeRepository degreeRepository)
		{
			_degreeRepository = degreeRepository;
			RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("EmloyeeId is required")
				.MustAsync((model,EmployeeId, CancellationToken)=>IsvalidEmployeeId(model)).WithMessage("This person has a maximum of 3 unexpired degrees!");
			RuleFor(x => x.DegreeName).NotEmpty().WithMessage("DegreeName is required")
				.MaximumLength(250).WithMessage("Name can not over 250 characters");
			RuleFor(x => x.IssuedBy).NotEmpty().WithMessage("IssuedBy is required")
				.MaximumLength(550).WithMessage("IssuedBy can not over 250 characters");
			RuleFor(x => x.DateRange).NotEmpty().WithMessage("DateRange is required")
				.Must(FuncUtilities.BeAValidDate).WithMessage("Invaild date (MM/dd/yyyy)!")
				.MustAsync((model,DateRange, CancellationToken)=>IsvalidDate(model)).WithMessage("The issue date cannot be greater than the expiration date !");
			RuleFor(x => x.DateOfExpiry).NotEmpty().WithMessage("DateOfExpiry is required")
				.Must(FuncUtilities.BeAValidDateOfExpiry).WithMessage("Invaild date (MM/dd/yyyy)!")
				.MustAsync((model, DateRange, CancellationToken) => IsvalidDate(model)).WithMessage("Expiration date cannot be less than issue date");



		}
		protected async Task<bool> IsvalidEmployeeId(DegreeViewModel model)
		{
			if (model.id == null)
			{
				var degrees = _degreeRepository.GetMultiBycondition(x => x.EmployeeId == model.EmployeeId && x.DateOfExpiry > DateTime.Now).Result;
				if (degrees.Count() < 3)
				{
					return true;

				}
				return false;
			}
			var degree = _degreeRepository.GetMultiBycondition(x => x.EmployeeId == model.EmployeeId && x.DateOfExpiry > DateTime.Now && x.Id != model.id).Result;
			if (degree.Count() < 3)
			{
				return true;

			}
			return false;
		}
		protected async Task<bool> IsvalidDate(DegreeViewModel model)
		{
			
			if (FuncUtilities.ConvertStringToDate(model.DateOfExpiry)>FuncUtilities.ConvertStringToDate(model.DateRange))
			{
				return true;

			}
			return false;
		}
		

	}
}
