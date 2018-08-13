namespace SpanHead.VA.DTO.Common
{
    public class DropDownDataField : IValidatableField
    {
        public DropDownDataField()
        {
            this.Value = new NameValueCodeItem();
        }
        public NameValueCodeItem Value { get; set; }
        public string ErrorText { get; set; }
        public string ErrorSection { get; set; }
        public bool IsError { get; set; }
    }
}
