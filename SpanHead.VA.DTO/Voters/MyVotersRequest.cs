namespace SpanHead.VA.DTO.Voters
{
    public class MyVotersRequest
    {
        public int AccountId { get; set; }
        public int? PrecinctId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
