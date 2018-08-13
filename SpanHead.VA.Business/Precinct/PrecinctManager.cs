namespace SpanHead.VA.Business.Precinct
{
    using SpanHead.VA.Business.Common;
    using SpanHead.VA.DTO.Common;
    using SpanHead.VA.DTO.Precinct;
    using SpanHead.VA.Repository.Precinct;
    using System;
    using System.Threading.Tasks;

    public interface IPrecinctManager : IManagerBase<Precinct>
    {
    }

    public class PrecinctManager : ManagerBase<Precinct>, IPrecinctManager
    {
        public PrecinctManager(IPrecinctDao precinctDao) 
            : base(precinctDao)
        {
        }

        public async override Task<TransactionResult<Precinct>> Save(Precinct entity)
        {
            var result = new TransactionResult<Precinct>();
            try
            {
                if (entity != null)
                {
                    //ToDo: Run validator..
                    if (entity.Id > 0)
                    {
                        var updatedPrecinct = await((IPrecinctDao)baseDao).Update(entity);
                        if (updatedPrecinct != null)
                        {
                            result.IsSuccess = true;
                            result.Data = updatedPrecinct;
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Errors.Add("Unable to update this precinct.");
                        }
                    }
                    else
                    {
                        var newPrecinct = await((IPrecinctDao)baseDao).Add(entity);
                        if (newPrecinct != null)
                        {
                            result.IsSuccess = true;
                            result.Data = newPrecinct;
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Errors.Add("Unable to add new precinct.");
                        }
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Errors.Add("Invalid request");
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }
    }


}
