using AccountAndTransactions.dto.request;
using AccountAndTransactions.dto.response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountAndTransactions.IServices
{


    public interface IAdminService
    {
        List<TransactionDto> getListOfTransactions(int page = 0, int size = 0);
        Task<GenericResponse> GetCustomerAccounts(int page=0,int size=0);
        List<CustomerDto> getCustomers(int page = 0, int size = 0);
    }
}
