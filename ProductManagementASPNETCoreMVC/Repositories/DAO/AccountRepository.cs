using BusinessObjects;
using DataAccessObjects;
using Repositories.Interface;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AccountMember GetAccountById(string accountID) => AccountDAO.GetAccountById(accountID);
    }
}