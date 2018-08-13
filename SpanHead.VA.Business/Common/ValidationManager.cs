namespace SpanHead.VA.Business.Common
{
    using SpanHead.VA.DTO.Common;
    using System;
    using System.Threading.Tasks;

    public interface IValidationManager<T>
        where T : IValidatable
    {
        Task<TransactionResult<T>> RunValidator(T dto, string validationFor);
    }

    public class ValidationManager<T> : IValidationManager<T>
        where T : IValidatable
    {
        public Task<TransactionResult<T>> RunValidator(T dto, string validationFor)
        {
            throw new NotImplementedException();
        }
    }
}
