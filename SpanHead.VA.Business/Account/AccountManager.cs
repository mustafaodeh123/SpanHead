namespace SpanHead.VA.Business.Account
{
    using SpanHead.VA.Business.Common;
    using SpanHead.VA.DTO.Account;
    using SpanHead.VA.DTO.Common;
    using SpanHead.VA.Repository.Account;
    using System;
    using System.Threading.Tasks;

    public interface IAccountManager : IManagerBase<AppUser>
    {
        Task<TransactionResult<AppUser>> Get(string userName);
        Task<TransactionResult<AppUser>> Login(AppUser user);
    }

    public class AccountManager : ManagerBase<AppUser>, IAccountManager
    {

        public AccountManager(IAppUserDao appUserDao)
            : base(appUserDao)
        {
        }

        public async Task<TransactionResult<AppUser>> Login(AppUser user)
        {
            var result = new TransactionResult<AppUser>();
            if (user != null && !string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
            {
                try
                {
                    // ToDo: decrypt password..
                    var userExists = await ((IAppUserDao)baseDao).Login(user);
                    if (userExists != null)
                    {
                        result.IsSuccess = true;
                        result.Data = userExists;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Errors.Add("User does not exists.");
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Errors.Add(ex.Message);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Errors.Add("Username or Password cannot be empty.");
            }

            return result;
        }

        public async Task<TransactionResult<AppUser>> Get(string userName)
        {
            var result = new TransactionResult<AppUser>();
            if (!string.IsNullOrEmpty(userName))
            {
                try
                {
                    var user = await ((IAppUserDao)baseDao).Get(userName);
                    if (user != null)
                    {
                        result.IsSuccess = true;
                        result.Data = user;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Errors.Add("User does not exists.");
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Errors.Add(ex.Message);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Errors.Add("Username cannot be empty.");
            }

            return result;
        }

        public override async Task<TransactionResult<AppUser>> Save(AppUser entity)
        {
            var result = new TransactionResult<AppUser>();
            try
            {
                if (entity != null)
                {
                    //ToDo: Run validator..
                    if (entity.AccountId > 0)
                    {
                        var updatedUser = await ((IAppUserDao)baseDao).Update(entity);
                        if (updatedUser != null)
                        {
                            result.IsSuccess = true;
                            result.Data = updatedUser;
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Errors.Add("Unable to update this user.");
                        }
                    }
                    else
                    {
                        var newUser = await ((IAppUserDao)baseDao).Add(entity);
                        if (newUser != null)
                        {
                            result.IsSuccess = true;
                            result.Data = newUser;
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Errors.Add("Unable to add new user.");
                        }
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Errors.Add("Invalid request");
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors.Add(ex.Message);
            }

            return result;

        }
    }
}
