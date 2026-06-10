using ECommerce.UserService.Core.Common.Enums;
using ECommerce.UserService.Core.Entities;
using ECommerce.UserService.Core.RepositoryContracts;
using ECommerce.UserService.Infrastructure.Data;
using Dapper;

namespace ECommerce.UserService.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _dapperDbContext;

        public UserRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        public async Task<ApplicationUser> AddUser(ApplicationUser user)
        {
            user.UserID = Guid.NewGuid();

            //Sql query to insert user into database can be written here using _dapperDbContext
            string sql = "INSERT INTO public.\"Users\" (\"UserID\", \"Email\", \"Password\", \"PersonName\", \"Gender\") " +
                "VALUES (@UserID, @Email, @Password, @PersonName, @Gender)";
            var rowsAffected = await _dapperDbContext.DbConnection.ExecuteAsync(sql, user);

            if (rowsAffected > 0)
            {
                return user;
            }

            return null;
        }

        public async Task<ApplicationUser> GetUserByEmailAndPassword(string email, string password)
        {
            var query = "SELECT * FROM public.\"Users\" WHERE \"Email\" = @Email AND \"Password\" = @Password";
            var user = await _dapperDbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, new { Email = email, Password = password });

            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}
