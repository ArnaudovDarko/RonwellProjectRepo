using RonwellProject.Models;

namespace RonwellProject.Interface
{
    public interface IEmployeeServices
    {
        Task<EmployeeInfo> Details(int? id);
        Task<EmployeeInfo> Edit(int? id, EmployeeInfo employeeInfo);
    }
}
