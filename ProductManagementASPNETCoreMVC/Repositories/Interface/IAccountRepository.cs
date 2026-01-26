using BusinessObjects;

namespace Repositories.Interface
{
    public interface IAccountRepository
    {
        AccountMember GetAccountById(string accountID);
    }
}