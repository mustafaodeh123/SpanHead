namespace SpanHead.VA.DTO.Common
{
    public class DataField : IValidatableField
    {
        public string Value { get; set; }
        public string ErrorText { get; set; }
        public string ErrorSection { get; set; }
        public bool IsError { get; set; }
    }
}
