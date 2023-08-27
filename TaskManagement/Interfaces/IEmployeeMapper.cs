using TaskManagement.DTO;
using TaskManagement.Models;

namespace TaskManagement.Interfaces
{
    public interface IEmployeeMapper
    {
        public User Map(EmployeeDTO employeeDTO);
    }
}
