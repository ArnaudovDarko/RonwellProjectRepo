using RonwellProject.Models;
using RonwellProject.ViewModels;

namespace RonwellProject.Interface
{
    public interface IEmployeeServices
    {
        Task<EmployeeInfo> GetDetails(int? id);
        Task<EmployeeInfo> Upsert(EmployeeInfoVM employeeInfo);

        Task<EmployeeInfo> Delete(int? id);
        Task<EmployeeInfo> GetEmployeeDetails(int? id);
        Task<List<EmployeeInfo>> GetEmployees();

    }
}
