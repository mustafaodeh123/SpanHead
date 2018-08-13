namespace SpanHead.VA.DTO.Common
{
    public interface IValidatableField
    {
        string ErrorText { get; set; }
        string ErrorSection { get; set; }
        bool IsError { get; set; }
    }
}
