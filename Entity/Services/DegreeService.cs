﻿using Entity.Constants;
using Entity.Models;
using Entity.Repository.Repositories;
using Entity.Services.Interface;
using Entity.Services.Utilities;
using Entity.Services.ViewModels;

namespace Entity.Services
{
	public class DegreeService : IDegreeService
	{
		IDegreeRepository _degreeRepository;
		public DegreeService(IDegreeRepository degreeRepository) : base()
		{
			_degreeRepository = degreeRepository;
		}

		public async Task<ResponseEntity> DeleteDegree(int id)
		{
			try
			{
				var degree = await _degreeRepository.GetSingleByIdAsync(x => x.Id == id);
				if (degree == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
				}
				await _degreeRepository.DeleteAsync(degree);
				return new ResponseEntity(StatusCodeConstants.OK, degree, MessageConstants.DELETE_SUCCESS);
			}
			catch(Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.DELETE_ERROR);
			}
		}

		public async Task<ResponseEntity> GetAllDegree()
		{
			var degrees = await _degreeRepository.GetAllAsync();
			return new ResponseEntity(StatusCodeConstants.OK, degrees, MessageConstants.MESSAGE_SUCCESS_200);
		}

		public async Task<ResponseEntity> GetDegreeById(int id)
		{
			try
			{
				var degree = await _degreeRepository.GetSingleByIdAsync(x => x.Id == id);
				if (degree == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
				}
				
				return new ResponseEntity(StatusCodeConstants.OK, degree, MessageConstants.MESSAGE_SUCCESS_200);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.NOT_FOUND,ex.Message, MessageConstants.MESSAGE_ERROR_404);
			}
		}

		public async Task<ResponseEntity> InsertDegree(DegreeViewModel model)
		{
			try
			{
				Degree degrees = new Degree();
				degrees.EmployeeId = model.EmployeeId;
				degrees.DegreeName = model.DegreeName;
				degrees.DateRange = FuncUtilities.ConvertStringToDate(model.DateRange);
				degrees.IssuedBy = model.IssuedBy;
				degrees.DateOfExpiry = FuncUtilities.ConvertStringToDate(model.DateOfExpiry);

				if (degrees == null)
				{
					return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, degrees, MessageConstants.INSERT_ERROR);
				}
				await _degreeRepository.InsertAsync(degrees);
				return new ResponseEntity(StatusCodeConstants.OK, degrees, MessageConstants.INSERT_SUCCESS);
			}
			catch(Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.INSERT_ERROR);
			}
		}

		public async Task<ResponseEntity> UpdateDegree( DegreeViewModel model)
		{
			try
			{
				var degree = await _degreeRepository.GetSingleByIdAsync(x => x.Id == model.id);
				if (degree == null)
				{
					return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
				}
				degree.EmployeeId = model.EmployeeId;
				degree.DegreeName = model.DegreeName;
				degree.DateRange = FuncUtilities.ConvertStringToDate(model.DateRange);
				degree.IssuedBy = model.IssuedBy;
				degree.DateOfExpiry = FuncUtilities.ConvertStringToDate(model.DateOfExpiry);

				await _degreeRepository.UpdateAsync(degree, degree);
				return new ResponseEntity(StatusCodeConstants.OK, degree, MessageConstants.UPDATE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.UPDATE_ERROR);
			}
		}
	}
}
