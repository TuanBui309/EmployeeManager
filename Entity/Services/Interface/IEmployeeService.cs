using Entity.Constants;
using Entity.Models;
using Entity.Services.ViewModels;

namespace Entity.Services.Interface
{
	public interface IEmployeeService
	{
		Task<ResponseEntity> GetAllEmployee(string keyWord,int soTrang , int soPhanTuTrenTrang );
		Task<ResponseEntity> GetEmployeeById(int id);
		Task<ResponseEntity> InsertEmployee(EmployeeViewModel model);
		Task<ResponseEntity> UpdateEmployee(EmployeeViewModel model);
		Task<ResponseEntity> DeleteEmployee(int id);
		//Task<ResponseEntity> GetEmployeeByKeyWord(string keyWord);
		byte[] DownloadReport(string keyWord);
		List<EmployeeViewModel> ReadEmployeeFromExcel(string fullPath);



	}
}
