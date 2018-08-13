using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SpanHead.VA.Business.Account;
using SpanHead.VA.DTO.Common;
using SpanHead.VA.DTO.Account;
using Microsoft.Extensions.Logging;
using SpanHead.VA.Web.Auth;

namespace SpanHead.VA.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountManager accountManager;
        private readonly ILogger logger;
        private readonly IJWTFactory jwtFactory;

        public AccountsController(IAccountManager accountManager, ILoggerFactory logger, IJWTFactory jwtFactory)
        {
            this.logger = logger.CreateLogger<AccountsController>();
            this.accountManager = accountManager;
            this.jwtFactory = jwtFactory;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<TransactionResult<AppUser>> Post([FromBody] AppUser appUser)
        {
            var existingUser = await this.accountManager.Login(appUser);

            if (existingUser == null || !existingUser.IsSuccess)
            {
                this.logger.LogInformation($"Invalid username ({appUser.UserName}) or password.");
                return existingUser;
            }

            existingUser.Data = await this.jwtFactory.GetEncodedToken(existingUser.Data);
           
            return existingUser;
        }


    }
}