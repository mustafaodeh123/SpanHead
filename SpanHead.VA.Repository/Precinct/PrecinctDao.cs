namespace SpanHead.VA.Repository.Precinct
{
    using Common;
    using Dapper;
    using DTO.Precinct;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IPrecinctDao : IBaseDao<Precinct>
    {
    }
    public class PrecinctDao : BaseDao<Precinct>, IPrecinctDao
    {
        public PrecinctDao() : base()
        {
        }

        public override async Task<Precinct> Add(Precinct precinct)
        {
            #region Insert Query
            string sql = @"
                   INSERT INTO [dbo].[Precinct]
                              ([Name]
                              ,[Number]
                              ,[Location]
                              ,[Predirectional])
                        VALUES
                              (@Name
                              ,@Number
                              ,@Location
                              ,@Predirectional)
              
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                int? Id = (await conn.QueryAsync<int>(sql, new
                {
                    Location = precinct.Location,
                    Name = precinct.Name,
                    Number = precinct.Number,
                    Predirectional = precinct.Predirectional
                })).FirstOrDefault();

                precinct.Id = Id ?? 0;
            }

            return precinct;
        }

        public override async Task<IEnumerable<Precinct>> Get()
        {
            #region Select Query
            string sql = @"
                           SELECT
                                 [Id]
                                ,[Name]
                                ,[Number]
                                ,[Location]
                                ,[Predirectional]
                           FROM
                               [dbo].[Precinct]";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<Precinct>(sql);
            }
        }

        public override async Task<Precinct> Get(int id)
        {
            #region Select Query
            string sql = @"
                           SELECT
                                 [Id]
                                ,[Name]
                                ,[Number]
                                ,[Location]
                                ,[Predirectional]
                           FROM
                               [dbo].[Precinct]
                           WHERE
                                Id = @Id";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                return (await conn.QueryAsync<Precinct>(sql, new { Id = id })).FirstOrDefault();
            }
        }

        public override async Task<bool> Remove(Precinct precinct)
        {
            return await Remove(precinct.Id);
        }

        public override async Task<bool> Remove(int id)
        {
            #region Delete Query
            string sql = @"
                           DELETE
                           FROM
                               [dbo].[Precinct]
                           WHERE
                                 Id = @Id";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                var affectedRows = await conn.ExecuteAsync(sql, new { Id = id });
                return affectedRows > 0;
            }
        }

        public override async Task<Precinct> Update(Precinct precinct)
        {
            #region Insert Query
            string sql = @"
                   UPDATE [dbo].[Precinct]
                        SET    [Name]           = @Name
                              ,[Number]         = @Number
                              ,[Location]       = @Location
                              ,[Predirectional] = @Predirectional
                        WHERE
                             Id = @Id";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                int? affectedRows = await conn.ExecuteAsync(sql, new
                {
                    Id =precinct.Id,
                    Location = precinct.Location,
                    Name = precinct.Name,
                    Number = precinct.Number,
                    Predirectional = precinct.Predirectional
                });
                // will handle the null in BL
                return affectedRows.HasValue && affectedRows > 0 ? precinct : null;
            }
        }
    }
}
