using TaskManagement.DTO;
using TaskManagement.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Mappers
{
    public class ManagerMapper : IManagerMapper
    {
       
        private static readonly Random random = new Random();
        private const string CharSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+";

        public static string GenerateRandomPassword(int length)
        {
        return new string(Enumerable.Repeat(CharSet, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public User Map(ManagerDTO managerDTO)
        {
            return new User() { UserId = managerDTO.UserId,Username= managerDTO.Username, Password = GenerateRandomPassword(8),Role = "Manager", Birthday = managerDTO.Birthday, PhoneNumber = managerDTO.PhoneNumber, Email = managerDTO.Email };
        }


    }
}
