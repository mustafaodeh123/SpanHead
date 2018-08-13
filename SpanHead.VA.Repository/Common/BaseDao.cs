namespace SpanHead.VA.Repository.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;

    public interface IBaseDao<T>
      where T : class, new()
    {
        Task<T> Add(T entity);
        Task<bool> Remove(T entity);
        Task<bool> Remove(int id);
        Task<T> Update(T entity);
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
    }
    public abstract class BaseDao<T> : Base, IBaseDao<T>
        where T : class, new()
    {
        public BaseDao()
            :base()
        {
        }
        public abstract Task<T> Add(T entity);
        public abstract Task<IEnumerable<T>> Get();
        public abstract Task<T> Get(int id);
        public abstract Task<bool> Remove(int id);
        public abstract Task<bool> Remove(T entity);
        public abstract Task<T> Update(T entity);
    }

    public class Base
    {
        protected string ConnectionString { get; private set; }
        protected Base(string byClient = "DefaultConnection")
        {
            //this.ConnectionString = @"Data Source=DET-3896\MSSQL2012;Initial Catalog=CandidateCompanion;Integrated Security=True";
            this.ConnectionString = @"Server=PC; Database=CandidateCompanion; Trusted_Connection=True; MultipleActiveResultSets=true";
           // this.ConnectionString = ConfigurationManager.ConnectionStrings[byClient].ConnectionString;
        }
    }
}
