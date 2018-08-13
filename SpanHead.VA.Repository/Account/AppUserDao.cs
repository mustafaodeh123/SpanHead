namespace SpanHead.VA.Repository.Account
{
    using Common;
    using Dapper;
    using DTO.Account;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IAppUserDao : IBaseDao<AppUser>
    {
        Task<AppUser> Get(string userName);
        Task<AppUser> Login(AppUser user);
    }

    public class AppUserDao : BaseDao<AppUser>, IAppUserDao
    {
        public AppUserDao()
            : base()
        {
        }

        public override async Task<AppUser> Add(AppUser user)
        {
            #region Insert Query
            string sql = @"
                   INSERT INTO [dbo].[Account]
                              ([UserName]
                               ,[Password]
                               ,[FirstName]
                               ,[LastName]
                               ,[Address]
                               ,[City]
                               ,[State]
                               ,[ZipCode])
                        VALUES
                              (@UserName
                              ,@Password
                              ,@FirstName
                              ,@LastName
                              ,@Address
                              ,@City
                              ,@State
                              ,@ZipCode)
              
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                int? userId = (await conn.QueryAsync<int>(sql, new
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode
                })).FirstOrDefault();

                user.AccountId = userId ?? 0;
            }

            return user;
        }

        public override async Task<IEnumerable<AppUser>> Get()
        {
            #region Select Query
            string sql = @"
                           SELECT
                                [AccountId]
                               ,[UserName]
                               ,[FirstName]
                               ,[LastName]
                               ,[Address]
                               ,[City]
                               ,[State]
                               ,[ZipCode]
                           FROM
                               dbo.Account
                           WHERE
                                 IsActive = 1";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<AppUser>(sql);
            }
        }

        public async Task<AppUser> Get(string userName)
        {
            #region Select Query
            string sql = @"
                           SELECT 
                                [UserName]
                               ,[FirstName]
                               ,[LastName]
                               ,[Address]
                               ,[City]
                               ,[State]
                               ,[ZipCode]
                           FROM
                               dbo.Account
                           WHERE
                                 UserName = @userName AND
                                 IsActive = 1";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                return (await conn.QueryAsync<AppUser>(sql, new { userName = userName })).FirstOrDefault();
            }
        }

        public async Task<AppUser> Login(AppUser user)
        {
            #region Select Query
            string sql = @"
                           SELECT
                                [AccountId]
                               ,[UserName]
                               ,[FirstName]
                               ,[LastName]
                               ,[Address]
                               ,[City]
                               ,[State]
                               ,[ZipCode]
                               ,[IsActive]
                           FROM
                               dbo.Account
                           WHERE
                                 UserName = @userName AND
                                 Password = @password AND
                                 IsActive = 1";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                return (await conn.QueryAsync<AppUser>(sql, new { userName = user.UserName, password = user.Password })).FirstOrDefault();
            }
        }

        public override async Task<AppUser> Get(int id)
        {
            #region Select Query
            string sql = @"
                           SELECT 
                                [AccountId]
                               ,[UserName]
                               ,[FirstName]
                               ,[LastName]
                               ,[Address]
                               ,[City]
                               ,[State]
                               ,[ZipCode]
                               ,[IsActive]
                           FROM
                               dbo.Account
                           WHERE
                                 AccountId = @Id AND
                                 IsActive = 1";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                return (await conn.QueryAsync<AppUser>(sql, new { Id = id })).FirstOrDefault();
            }
        }
              
        public override async Task<bool> Remove(AppUser user)
        {
            return await Remove(user.AccountId);
        }

        public override async Task<bool> Remove(int id)
        {
            #region Delete Query
            string sql = @"
                           DELETE
                           FROM
                               dbo.Account
                           WHERE
                                 AccountId = @Id";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                var affectedRows = await conn.ExecuteAsync(sql, new { Id = id });
                return affectedRows > 0;
            }
        }

        public override async Task<AppUser> Update(AppUser user)
        {
            #region Update Query
            string sql = @"
                   UPDATE [dbo].[Account]   
                           SET  [Password]  = @Password
                               ,[FirstName] = @FirstName
                               ,[LastName]  = @LastName
                               ,[Address]   = @Address
                               ,[City]      = @City
                               ,[State]     = @State
                               ,[ZipCode])  = @ZipCode
                               ,[IsActive]  = @IsActive
                        WHERE
                              AccountId = @Id";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                int? affectedRows = await conn.ExecuteAsync(sql, new
                {
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    IsActive = user.IsActive,
                    Id = user.AccountId
                });

                // will handle the null in BL
                return affectedRows.HasValue && affectedRows > 0 ? user : null;
            }
        }
    }
}
