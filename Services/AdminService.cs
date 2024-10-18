using AccountAndTransactions.dto.request;
using AccountAndTransactions.IServices;
using System.Collections.Generic;

namespace AccountAndTransactions.Services
{
    public class AdminService : IAdminService
    {
        public List<AccountDto> GetCustomerAccounts(int page = 0, int size = 0)
        {
            throw new System.NotImplementedException();
        }

        public List<CustomerDto> getCustomers(int page = 0, int size = 0)
        {
            throw new System.NotImplementedException();
        }

        public List<TransactionDto> getListOfTransactions(int page = 0, int size = 0)
        {
            throw new System.NotImplementedException();
        }
    }
}
