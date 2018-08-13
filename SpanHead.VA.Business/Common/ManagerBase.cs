namespace SpanHead.VA.Business.Common
{
    using SpanHead.VA.DTO.Common;
    using SpanHead.VA.Repository.Common;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IManagerBase<T>
        where T : class, new()
    {
        Task<TransactionResult<T>> Save(T entity);
        Task<TransactionResult<bool>> Remove(T entity);
        Task<TransactionResult<bool>> Remove(int id);
        Task<TransactionResult<IEnumerable<T>>> Get();
        Task<TransactionResult<T>> Get(int id);
    }

    public abstract class ManagerBase<T> : IManagerBase<T>
        where T : class, new()
    {
        protected readonly IBaseDao<T> baseDao;

        public ManagerBase(IBaseDao<T> baseDao)
        {
            this.baseDao = baseDao;
        }

        public async Task<TransactionResult<IEnumerable<T>>> Get()
        {
            var result = new TransactionResult<IEnumerable<T>>();

            try
            {
                var entities = await this.baseDao.Get();
                if (entities != null)
                {
                    result.Data = entities;
                    result.IsSuccess = true;
                    result.Message = "Items have been successfully loaded.";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Errors.Add("Unable to load items.");
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        public async Task<TransactionResult<T>> Get(int id)
        {
            var result = new TransactionResult<T>();

            try
            {
                if (id > 0)
                {
                    var entity = await this.baseDao.Get(id);
                    if (entity != null)
                    {
                        result.Data = entity;
                        result.IsSuccess = true;
                        result.Message = "Item has been successfully loaded.";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Errors.Add("Unable to load item.");
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

        public async Task<TransactionResult<bool>> Remove(T entity)
        {
            var result = new TransactionResult<bool>();

            try
            {
                if (entity != null)
                {
                    var removed = await this.baseDao.Remove(entity);
                    result.Data = removed;
                    result.IsSuccess = removed;
                    if (removed)
                        result.Message = "Item has been successfully removed.";
                    else
                        result.Errors.Add("Unable to delete item.");
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

        public async Task<TransactionResult<bool>> Remove(int id)
        {
            var result = new TransactionResult<bool>();

            try
            {
                if (id > 0)
                {
                    var removed = await this.baseDao.Remove(id);
                    result.Data = removed;
                    result.IsSuccess = removed;
                    if (removed)
                        result.Message = "Item has been successfully removed.";
                    else
                        result.Errors.Add("Unable to delete item.");
                }
                else
                {
                    result.IsSuccess = false;
                    result.Errors.Add("Invalid Id");
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        public abstract Task<TransactionResult<T>> Save(T entity);
    }
}
