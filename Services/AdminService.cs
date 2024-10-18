using AccountAndTransactions.dto.request;
using AccountAndTransactions.genericobj;
using AccountAndTransactions.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace AccountAndTransactions.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppSettings _settings;
        private readonly IAdminService _adminService;
        private readonly IConfiguration configuration;
        private readonly string con;


        public AdminService(IOptions<AppSettings> settings, IAdminService adminService)
        {
            _settings = settings.Value;
            _adminService = adminService;
            con = configuration.GetConnectionString("dbconn");
        }
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
