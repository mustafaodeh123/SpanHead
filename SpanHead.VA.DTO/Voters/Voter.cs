namespace SpanHead.VA.DTO.Voters
{
    using Common;
    using System;
    using System.Collections.Generic;

    public class Voter: IValidatable
    {
        public int Id { get; set; }
        public string PrecinctName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Race { get; set; }
        public int OrigVoter { get; set; }
        public string Suffix { get; set; }
        public int BirthDate { get; set; }
        //ToDo: check if this field nullable
        public DateTime RegisterDate { get; set; }
        public string Gender { get; set; }
        public string StreetNumber { get; set; }
        /// <summary>
        /// An address element that indicates geographic location 
        /// such as N, S, E, W, NE, NW, SE, and SW that is placed to the left of (before) the street name such as E HOOVER ST.
        /// </summary>
        public string Predirectional { get; set; }
        public string StreetName { get; set; }
        public string StreetType { get; set; }
        public string RESEXT { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PermAvind { get; set; }
        public string MailAddress { get; set; }
        public string MailAddress2 { get; set; }
        public string StateSenateCo { get; set; }
        public string StateHouseCo { get; set; }
        public string USCongress { get; set; }
        public string CountyCode { get; set; }
        public string SuffDirection { get; set; }
        //public Precinct Precinct { get; set; }
        public IEnumerable<VoterHistory> VoterHistory { get; set; }

        public Voter()
        {
            this.VoterHistory = new List<VoterHistory>();
            // this not updatable field .. we gonna get that from the data feed
            //this.Gender = new DropDownDataField();
        }
    }

    public class VoterHistory
    {
        //public int VoterHistoryId { get; set; }
        public int ElectionYear { get; set; }
        /// <summary>
        /// Primary or General
        /// </summary>
        public string ElectionType { get; set; }
        public VoterHistory()
        {
            // this not updatable field .. we gonna get that from the data feed
            // this.ElectionType = new DropDownDataField();
        }
    }
}
