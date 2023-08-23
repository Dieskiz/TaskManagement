using TaskManagement.DTO;
using TaskManagement.Models;

namespace TaskManagement.Interfaces
{
    public interface IDepartmentMapper
    {
        public Department Map(DepartmentDTO departmentDTO);
    }
}
