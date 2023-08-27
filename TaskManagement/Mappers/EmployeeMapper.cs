using TaskManagement.DTO;
using TaskManagement.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Mappers
{
    public class EmployeeMapper : IEmployeeMapper
    {

        private static readonly Random random = new Random();
        private const string CharSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+";

        public static string GenerateRandomPassword(int length)
        {
            return new string(Enumerable.Repeat(CharSet, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public User Map(EmployeeDTO employeeDTO)
        {
            return new User() { UserId = employeeDTO.UserId, Username = employeeDTO.Username, Password = GenerateRandomPassword(8), Role = "Employee", Birthday = employeeDTO.Birthday, PhoneNumber = employeeDTO.PhoneNumber, Email = employeeDTO.Email };
        }

    }
}
