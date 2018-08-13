namespace SpanHead.VA.Business.Voters
{
    using SpanHead.VA.Business.Common;
    using SpanHead.VA.DTO.Common;
    using SpanHead.VA.DTO.Voters;
    using SpanHead.VA.Repository.Voters;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVoterManager : IManagerBase<Voter>
    {
        Task<TransactionResult<IEnumerable<Voter>>> Get(VoterRequest request);
        Task<TransactionResult<IEnumerable<Voter>>> GetVotersWithHistory(VoterRequest request);
        Task<TransactionResult<IEnumerable<VoterHistory>>> GetVoterHistory(int id);
        Task<TransactionResult<IEnumerable<MyVoter>>> MyVoters(MyVotersRequest request);
    }

    public class VoterManager : ManagerBase<Voter>, IVoterManager
    {
        public VoterManager(IVoterDao voterDao)
            : base(voterDao)
        {
        }

        public async Task<TransactionResult<IEnumerable<Voter>>> Get(VoterRequest request)
        {
            var result = new TransactionResult<IEnumerable<Voter>>();
            if (request != null)
            {
                try
                {
                    var voters = await ((IVoterDao)baseDao).Get(request);
                    result.IsSuccess = true;
                    if (voters != null)
                        result.Data = voters;
                    else
                    {
                        result.Data = new List<Voter>();
                        result.Message = "No data to display.";
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Errors.Add(ex.Message);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Errors.Add("request is invalid.");
            }

            return result;
        }

        public async Task<TransactionResult<IEnumerable<VoterHistory>>> GetVoterHistory(int id)
        {
            var result = new TransactionResult<IEnumerable<VoterHistory>>();
            if (id > 0)
            {
                try
                {
                    var voterHistories = await ((IVoterDao)baseDao).GetVoterHistory(id);
                    result.IsSuccess = true;
                    if (voterHistories != null)
                        result.Data = voterHistories;
                    else
                    {
                        result.Data = new List<VoterHistory>();
                        result.Message = "No data to display.";
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Errors.Add(ex.Message);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Errors.Add("Invalid Id.");
            }

            return result;
        }

        public async Task<TransactionResult<IEnumerable<Voter>>> GetVotersWithHistory(VoterRequest request)
        {
            var result = new TransactionResult<IEnumerable<Voter>>();
            if (request != null)
            {
                try
                {
                    var votersWithHistory = new List<Voter>();
                    var voters = await ((IVoterDao)baseDao).Get(request);
                    result.IsSuccess = true;
                    if (voters != null)
                    {
                        foreach (var voter in voters)
                        {
                            voter.VoterHistory = await ((IVoterDao)baseDao).GetVoterHistory(voter.Id);
                            votersWithHistory.Add(voter);
                        }
                        result.Data = votersWithHistory;
                    }
                    else
                    {
                        result.Data = new List<Voter>();
                        result.Message = "No data to display.";
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Errors.Add(ex.Message);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Errors.Add("Request is invalid.");
            }

            return result;
        }

        public async Task<TransactionResult<IEnumerable<MyVoter>>> MyVoters(MyVotersRequest request)
        {
            var result = new TransactionResult<IEnumerable<MyVoter>>();
            if (request != null)
            {
                try
                {
                    var myVoters = await((IVoterDao)baseDao).MyVoters(request);
                    result.IsSuccess = true;
                    if (myVoters != null)
                        result.Data = myVoters;
                    else
                    {
                        result.Data = new List<MyVoter>();
                        result.Message = "No data to display.";
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Errors.Add(ex.Message);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Errors.Add("request is invalid.");
            }

            return result;
        }

        #region Not Implemented Methods...
        //ToDo: need implementation if we need to turn on this feature
        public override Task<TransactionResult<Voter>> Save(Voter entity) =>
            throw new System.NotImplementedException();
        #endregion
    }
}
