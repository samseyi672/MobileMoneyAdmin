using AccountAndTransactions.dto.request;
using System.Collections.Generic;

namespace AccountAndTransactions.IServices
{


    public interface IAdminService
    {
        List<TransactionDto> getListOfTransactions(int page = 0, int size = 0);
        List<AccountDto> GetCustomerAccounts(int page=0,int size=0);
        List<CustomerDto> getCustomers(int page = 0, int size = 0);
    }
}
