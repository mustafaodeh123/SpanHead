namespace SpanHead.VA.DTO.Voters
{
    using Common;
    using System.Collections.Generic;

    public class MyVoterDetails : IValidatable
    {
        public DataField PrimaryPhone { get; set; }
        public DataField SecondaryPhone { get; set; }
        public DataField Email { get; set; }
        public DataField Heritage { get; set; }
        public DropDownDataField ContactableMethod { get; set; }

        public MyVoterDetails()
        {
            this.PrimaryPhone = new DataField();
            this.SecondaryPhone = new DataField();
            this.Email = new DataField();
            this.Heritage = new DataField();
            this.ContactableMethod = new DropDownDataField();
        }
    }

    public  class MyVoter
    {
        public int AccountId { get; set; }
        public Voter Voter { get; set; }
        public MyVoterDetails Details { get; set; }
        public IEnumerable<MyVoterComment> Comments { get; set; }
        public bool IsContactable { get; set; }

        public MyVoter()
        {
            this.Comments = new List<MyVoterComment>();
            this.Voter = new Voter();
            this.Details = new MyVoterDetails();
        }
    }
}