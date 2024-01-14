using Microsoft.EntityFrameworkCore;
using RonwellProject.Helpers;
using RonwellProject.Interface;
using RonwellProject.Models;

namespace RonwellProject.Services
{


    public class EmployeeServices : IEmployeeServices
    {
        private readonly DataContext _context;

        public EmployeeServices(DataContext context)
        {
            _context = context;
        }

        public async Task<EmployeeInfo> Details(int? id)
        {



            var employeeInfo = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
         
            return employeeInfo; 
        }

        public async Task<EmployeeInfo> Edit(int? id, EmployeeInfo employeeInfo)
        {
            try
            {
                _context.Update(employeeInfo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeInfoExists(employeeInfo.EmployeeId))
                {
                   
                }
                else
                {
                    throw;
                }
            }

            return employeeInfo;
           
        }

        private bool EmployeeInfoExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
