using Microsoft.EntityFrameworkCore;
using RonwellProject.Helpers;
using RonwellProject.Interface;
using RonwellProject.Models;
using RonwellProject.ViewModels;

namespace RonwellProject.Services
{


    public class EmployeeServices : IEmployeeServices
    {
        private readonly DataContext _context;

        public EmployeeServices(DataContext context)
        {
            _context = context;
        }

        public async Task<EmployeeInfo> Delete(int? id)
        {
            var employeeInfo = await _context.Employees.FindAsync(id);
            if (employeeInfo != null)
            {
                _context.Employees.Remove(employeeInfo);
            }

            await _context.SaveChangesAsync();

            return employeeInfo;
        }

        public async Task<EmployeeInfo> GetDetails(int? id)
        {
            var employeeInfo = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
         

            return employeeInfo; 
        }

        public async Task<EmployeeInfo> Upsert(EmployeeInfoVM employeeInfo)
        {
            if(employeeInfo.EmployeeId != 0)
            {
                var employeeInfoDb = await _context.Employees.FirstOrDefaultAsync(s => s.EmployeeId == employeeInfo.EmployeeId);

                if (employeeInfoDb != null)
                {
                    employeeInfoDb.Name = employeeInfo.Name;
                    employeeInfoDb.Position = employeeInfo.Position;
                    employeeInfoDb.Salary = employeeInfo.Salary;

                    _context.Update(employeeInfoDb);
                    await _context.SaveChangesAsync();
                }

                return employeeInfoDb;
            }
            else
            {
                var newEmployeeInfo = new EmployeeInfo
                {
                    Name = employeeInfo.Name,
                    Position = employeeInfo.Position,
                    Salary = employeeInfo.Salary
                };

                await _context.AddAsync(newEmployeeInfo);
                await _context.SaveChangesAsync();

                return newEmployeeInfo;
            }
         
        }

        public async Task<List<EmployeeInfo>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<EmployeeInfo> GetEmployeeDetails(int? id)
        {
            return await _context.Employees.FirstOrDefaultAsync(s => s.EmployeeId == id);
        }
    }
}