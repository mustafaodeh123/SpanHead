namespace SpanHead.VA.DTO.Voters
{
    using Common;
    using System;

    public class MyVoterComment: IValidatable
    {
        public int AccountId { get; set; }
        public int VoterId { get; set; }
        public DataField Comment { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public MyVoterComment()
        {
            this.Comment = new DataField();
        }
    }
}
