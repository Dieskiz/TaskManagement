using TaskManagement.DTO;
using TaskManagement.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Mappers
{
    public class DepartmentMapper : IDepartmentMapper
    {
        public Department Map(DepartmentDTO departmentDTO)
        {
            return new Department() {DepartmentId = departmentDTO.DepartmentId, DepartmentName = departmentDTO.DepartmentName, UserId = departmentDTO.UserId};
        }
    }
}
