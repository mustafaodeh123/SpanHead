namespace SpanHead.VA.Repository.Voters
{
    using Common;
    using Dapper;
    using DTO.Common;
    using DTO.Voters;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IVoterDao : IBaseDao<Voter>
    {
        Task<IEnumerable<Voter>> Get(VoterRequest request);
        Task<IEnumerable<VoterHistory>> GetVoterHistory(int id);
        Task<IEnumerable<MyVoter>> MyVoters(MyVotersRequest request);
    }
    public class VoterDao : BaseDao<Voter>, IVoterDao
    {
        public VoterDao() : base()
        {
        }

        public async Task<IEnumerable<Voter>> Get(VoterRequest request)
        {
            #region Select Query
            string sql = @"
                           IF(@PrecinctId <=0)
                            SET @PrecinctId = NULL
                                
                           IF(@AccountId <=0)
                            SET @AccountId = NULL
                           
                           IF(@BirthDateFrom <=0)
                            SET @BirthDateFrom = NULL
                            
                           IF(@BirthDateTo <=0)
                            SET @BirthDateTo = NULL
                           
                           SELECT
                                v.[Id]
                               ,p.[Name] PrecinctName
                               ,v.[FirstName]
                               ,v.[MiddleName]
                               ,v.[LastName]
                               ,v.[Race]
                               ,v.[OrigVoter]
                               ,v.[Suffix]
                               ,v.[BirthDate]
                               ,v.[RegisterDate]
                               ,v.[Gender]
                               ,v.[StreetNumber]
                               ,v.[Predirectional]
                               ,v.[StreetName]
                               ,v.[StreetType]
                               ,v.[RESEXT]
                               ,v.[Address2]
                               ,v.[City]
                               ,v.[State]
                               ,v.[Zip]
                               ,v.[PermAvind]
                               ,v.[MailAddress]
                               ,v.[MailAddress2]
                               ,v.[StateSenateCo]
                               ,v.[StateHouseCo]
                               ,v.[USCongress]
                               ,v.[CountyCode]
                               ,v.[SuffDirection]
                           FROM 
                               [dbo].[Voter] v
                             INNER JOIN 
                               [dbo].[Precinct] p ON v.PrecinctId = p.Id
                             LEFT JOIN
                               [dbo].[AccountVoters] av ON av.VoterId = v.Id
                             LEFT JOIN
                           	[dbo].[Account] a ON a.AccountId = av.AccountId
                           
                             WHERE
                                  ((av.AccountId = @AccountId AND a.IsActive = 1) OR @AccountId IS NULL) AND
                                  (v.PrecinctId = @PrecinctId OR @PrecinctId IS NULL) AND
                                  ((v.LastName LIKE @LastName + '%' OR NULLIF(@LastName,'') IS NULL) OR
                                  (v.FirstName LIKE @FirstName + '%' OR NULLIF(@FirstName,'') IS NULL) OR
                                  (v.City LIKE @City + '%' OR NULLIF(@City,'') IS NULL) OR
                                  (v.State LIKE @State + '%' OR NULLIF(@State,'') IS NULL) OR
                                  (v.Zip LIKE @zipCode + '%' OR NULLIF(@zipCode,'') IS NULL) OR
                           	      (v.CountyCode = @CountyCode OR NULLIF(@CountyCode,'') IS NULL) OR
                           	      (v.StreetName LIKE @StreetName + '%' OR NULLIF(@StreetName,'') IS NULL) OR
                           	      (v.StreetNumber LIKE @StreetNumber + '%' OR NULLIF(@StreetNumber,'') IS NULL)) AND
                           	      (v.Gender = @Gender OR NULLIF(@Gender,'') IS NULL) AND
                           	      (v.BirthDate >= @BirthDateFrom OR @BirthDateFrom IS NULL) AND
                           	      (v.BirthDate <= @BirthDateTo OR @BirthDateTo IS NULL) AND
                           	      (v.RegisterDate >= @RegisterDateFrom OR @RegisterDateFrom IS NULL) AND
                           	      (v.RegisterDate <= @RegisterDateTo OR @RegisterDateTo IS NULL)";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                var results = await conn.QueryAsync<Voter>(sql,
                    new
                    {
                        AccountId = request.AccountId,
                        City = request.City,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PrecinctId = request.PrecinctId,
                        State = request.State,
                        ZipCode = request.ZipCode,
                        BirthDateFrom = request.BirthDateFrom,
                        BirthDateTo = request.BirthDateTo,
                        CountyCode = request.CountyCode,
                        Gender = request.Gender,
                        RegisterDateFrom = request.RegisterDateFrom,
                        RegisterDateTo = request.RegisterDateTo,
                        StreetNumber = request.StreetNumber,
                        StreetName = request.StreetName
                    });
                return results;
            }
        }

        public async override Task<IEnumerable<Voter>> Get()
        {
            #region Select Query
            string sql = @"
                           SELECT
                              v.[Id]
                             ,p.[Name] PrecinctName
                             ,v.[FirstName]
                             ,v.[MiddleName]
                             ,v.[LastName]
                             ,v.[Race]
                             ,v.[OrigVoter]
                             ,v.[Suffix]
                             ,v.[BirthDate]
                             ,v.[RegisterDate]
                             ,v.[Gender]
                             ,v.[StreetNumber]
                             ,v.[Predirectional]
                             ,v.[StreetName]
                             ,v.[StreetType]
                             ,v.[RESEXT]
                             ,v.[Address2]
                             ,v.[City]
                             ,v.[State]
                             ,v.[Zip]
                             ,v.[PermAvind]
                             ,v.[MailAddress]
                             ,v.[MailAddress2]
                             ,v.[StateSenateCo]
                             ,v.[StateHouseCo]
                             ,v.[USCongress]
                             ,v.[CountyCode]
                             ,v.[SuffDirection]
                         FROM 
                             [dbo].[Voter] v
                           INNER JOIN 
                             [dbo].[Precinct] p ON v.PrecinctId = p.Id";
            #endregion

            var voters = new List<Voter>();
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                var results = await conn.QueryAsync<Voter>(sql);
                if (results != null)
                    foreach (var voter in results)
                    {
                        voter.VoterHistory = await GetVoterHistory(voter.Id);
                        voters.Add(voter);
                    }
            }

            return voters;
        }

        public async override Task<Voter> Get(int id)
        {
            #region Select Query
            string sql = @"
                           SELECT
                              v.[Id]
                             ,p.[Name] PrecinctName
                             ,v.[FirstName]
                             ,v.[MiddleName]
                             ,v.[LastName]
                             ,v.[Race]
                             ,v.[OrigVoter]
                             ,v.[Suffix]
                             ,v.[BirthDate]
                             ,v.[RegisterDate]
                             ,v.[Gender]
                             ,v.[StreetNumber]
                             ,v.[Predirectional]
                             ,v.[StreetName]
                             ,v.[StreetType]
                             ,v.[RESEXT]
                             ,v.[Address2]
                             ,v.[City]
                             ,v.[State]
                             ,v.[Zip]
                             ,v.[PermAvind]
                             ,v.[MailAddress]
                             ,v.[MailAddress2]
                             ,v.[StateSenateCo]
                             ,v.[StateHouseCo]
                             ,v.[USCongress]
                             ,v.[CountyCode]
                             ,v.[SuffDirection]
                         FROM 
                             [dbo].[Voter] v
                           INNER JOIN 
                             [dbo].[Precinct] p ON v.PrecinctId = p.Id
                         WHERE 
                               v.Id = @Id";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                var voter = (await conn.QueryAsync<Voter>(sql, new { Id = id })).FirstOrDefault();
                if (voter != null)
                    voter.VoterHistory = await GetVoterHistory(id);

                return voter;
            }
        }

        public async Task<IEnumerable<VoterHistory>> GetVoterHistory(int id)
        {
            #region Select Query
            string sql = @"
                           SELECT 
                                  [ElectionYear]
                                 ,[ElectionType]
                           FROM 
                               [dbo].[VoterHistory]
                           WHERE
                                [VoterId] = @Id";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<VoterHistory>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<MyVoter>> MyVoters(MyVotersRequest request)
        {
            var myVoters = new List<MyVoter>();

            #region Select Query
            string sql = @"
                           IF(@PrecinctId <=0)
                             SET @PrecinctId = NULL
                            
                           SELECT 
                           	   av.[VoterId]
                                 ,av.[AccountId]
                                 ,av.[PrimaryPhoneNumber]
                                 ,av.[SecondaryPhoneNumebr]
                                 ,av.[Email]
                                 ,av.[Contactable]
                                 ,av.[FavContactMethod]
                                 ,av.[Heritage]
                             FROM 
                                 [dbo].[AccountVoters] av
                           	INNER JOIN
                           	  [dbo].[Account] a ON a.AccountId = av.AccountId
                           	INNER JOIN 
                           	  [dbo].[Voter] v ON v.Id = av.VoterId
                            WHERE
                                 a.IsActive = 1 AND
                           	     av.AccountId = @AccountId AND
                           	     (v.PrecinctId = @PrecinctId OR @PrecinctId IS NULL) AND
                           	     ((v.LastName LIKE @LastName + '%' OR NULLIF(@LastName,'') IS NULL) OR
                           	     (v.FirstName LIKE @FirstName + '%' OR NULLIF(@FirstName,'') IS NULL) OR
                           	     (v.City LIKE @City + '%' OR NULLIF(@City,'') IS NULL) OR
                           	     (v.State LIKE @State + '%' OR NULLIF(@State,'') IS NULL) OR
                           	     (v.Zip LIKE @zipCode + '%' OR NULLIF(@zipCode,'') IS NULL))";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                var results = await conn.QueryAsync<MyVoterMapObject>(sql,
                    new
                    {
                        AccountId = request.AccountId,
                        City = request.City,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PrecinctId = request.PrecinctId,
                        State = request.State,
                        ZipCode = request.ZipCode
                    });
                if (results != null)
                    foreach (var myVoter in results)
                    {
                        myVoters.
                         Add(
                             new MyVoter()
                             {
                                 AccountId = myVoter.AccountId,
                                 Comments = await Comments(myVoter.VoterId),
                                 IsContactable = myVoter.Contactable,
                                 Voter = await Get(myVoter.VoterId),
                                 Details = new MyVoterDetails()
                                 {
                                     ContactableMethod = new DropDownDataField() { Value = new NameValueCodeItem() { Code = myVoter.FavContactMethod } },
                                     Email = new DataField() { Value = myVoter.Email },
                                     Heritage = new DataField() { Value = myVoter.Heritage },
                                     PrimaryPhone = new DTO.Common.DataField() { Value = myVoter.PrimaryPhoneNumber },
                                     SecondaryPhone = new DTO.Common.DataField() { Value = myVoter.SecondaryPhoneNumebr }
                                 }
                             }
                            );
                    }
            }

            return myVoters;
        }

        private async Task<IEnumerable<MyVoterComment>> Comments(int voterId)
        {
            var comments = new List<MyVoterComment>();
            #region Select Query
            string sql = @"
                           SELECT 
                        	   avc.[VoterId]
                              ,avc.[AccountId]
                              ,avc.[Comment]
                              ,avc.[InsertTs] InsertedDate
                              ,(ai.[LastName] + ', ' + ai.[FirstName]) InsertedBy
                              ,avc.[UpdateTs] UpdatedDate
                              ,(au.[LastName] + ', ' + au.[FirstName]) LastUpdateBy
                           FROM 
                        	  [dbo].[AccountVoterComments] avc
                        	INNER JOIN
                        	  [dbo].[Account] ai ON ai.AccountId = avc.InsertedBy
                        	LEFT JOIN
                        	  [dbo].[Account] au ON au.AccountId = avc.UpdatedBy
                           WHERE
                              avc.[IsActive] = 1 AND
                              avc.VoterId = @voterId 
                        ";
            #endregion

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                await conn.OpenAsync();
                var results = await conn.QueryAsync<dynamic>(sql, new { voterId = voterId });
                if (results != null)
                {
                    foreach (var comment in results)
                    {
                        comments.Add(new MyVoterComment()
                        {
                            AccountId = comment.AccountId,
                            Comment = new DataField() { Value = comment.Comment },
                            InsertedBy = comment.InsertedBy,
                            InsertedDate = comment.InsertedDate,
                            LastUpdateBy = comment.LastUpdateBy,
                            UpdatedDate = comment.UpdatedDate,
                            VoterId = comment.VoterId
                        });
                    }
                }
            }

            return comments;
        }

        #region Not Implemented Methods...
        //ToDo: need implementation if we need to turn on this feature
        public override Task<Voter> Add(Voter voter) =>
            throw new NotImplementedException();

        //ToDo: need implementation if we need to turn on this feature
        public override Task<bool> Remove(Voter voter) =>
            throw new NotImplementedException();

        //ToDo: need implementation if we need to turn on this feature
        public override Task<bool> Remove(int id) =>
            throw new NotImplementedException();

        //ToDo: need implementation if we need to turn on this feature
        public override Task<Voter> Update(Voter voter) =>
            throw new NotImplementedException();
        #endregion
    }
}
