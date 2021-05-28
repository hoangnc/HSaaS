namespace HSaaS.Account.Web.Areas.Account.Controllers.Models
{
    public class HSaaSLoginResult
    {
        public HSaaSLoginResult(LoginResultType result)
        {
            Result = result;
        }

        public LoginResultType Result { get; }

        public string Description => Result.ToString();
    }
}
