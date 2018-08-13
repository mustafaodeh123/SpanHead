namespace SpanHead.VA.DTO.Voters
{
    using System;

    public class VoterRequest : MyVotersRequest
    {
        public int? BirthDateFrom { get; set; }
        public int? BirthDateTo { get; set; }
        public DateTime? RegisterDateFrom { get; set; }
        public DateTime? RegisterDateTo { get; set; }
        public string CountyCode { get; set; }
        public string Gender { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
    }
}
