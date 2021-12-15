using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscapeHit.App.Repositories
{
    public interface IUserRepository
    {
        Task Create(UserEntity invoice);

        Task<UserEntity> FindById(int id);
    }
}
