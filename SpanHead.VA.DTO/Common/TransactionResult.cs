namespace SpanHead.VA.DTO.Common
{
    using System.Collections.Generic;

    public class TransactionResult<T>
    {
        public TransactionResult()
        {
            this.Errors = new List<string>();
        }
        public TransactionResult(T data)
            :this()
        {
            this.Data = data;
        }

        public T Data { get; set; }
        public IList<string> Errors { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}