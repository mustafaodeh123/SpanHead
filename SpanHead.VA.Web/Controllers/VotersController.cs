namespace SpanHead.VA.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SpanHead.VA.Business.Voters;
    using SpanHead.VA.DTO.Common;
    using SpanHead.VA.DTO.Voters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Produces("application/json")] // this is the default anyway .. 
    [Route("api/voters")]
    public class VotersController : Controller
    {
        private readonly IVoterManager voterManager;

        public VotersController(IVoterManager voterManager)
        {
            this.voterManager = voterManager;
        }

        [HttpPost("getvoters")]
        [Authorize]
        public async Task<TransactionResult<IEnumerable<Voter>>> Post([FromBody]VoterRequest request)
        {
            return await this.voterManager.Get(request);
        }
    }
}
