namespace SpanHead.VA.DTO.Account
{
    using SpanHead.VA.DTO.Common;

    public class AppUser : JWTDto, IValidatable
    {
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return string.Concat(this.FirstName, " ", this.LastName);
        }
    }
}
