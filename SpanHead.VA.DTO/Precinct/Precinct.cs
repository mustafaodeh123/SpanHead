namespace SpanHead.VA.DTO.Precinct
{
    using SpanHead.VA.DTO.Common;
    using System.Collections.Generic;
    using Voters;

    public class Precinct: IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Location { get; set; }
        /// <summary>
        /// An address element that indicates geographic location 
        /// such as N, S, E, W, NE, NW, SE, and SW
        /// </summary>
        public string Predirectional { get; set; }
        public IEnumerable<Voter> Voters { get; set; }

        public Precinct()
        {
            this.Voters = new List<Voter>();
        }
    }
}
