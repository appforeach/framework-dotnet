using System;

namespace EscapeHit.App.Services
{
    public interface IUserNumberService
    {
        string GenerateUserNumber(UserEntity invoice);
    }

    public class UserNumberService : IUserNumberService
    {
        public string GenerateUserNumber(UserEntity invoice)
        {
            return "inv-001";
        }
    }
}
