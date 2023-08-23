using TaskManagement.DTO;
using TaskManagement.Models;

namespace TaskManagement.Interfaces
{
    public interface IManagerMapper
    {
        public User Map(ManagerDTO managerDTO);
    }
}
