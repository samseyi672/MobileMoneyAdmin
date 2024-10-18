using AccountAndTransactions.dto.request;
using AccountAndTransactions.dto.response;
using AccountAndTransactions.genericobj;
using AccountAndTransactions.IServices;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AccountAndTransactions.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppSettings _settings;
        private readonly IAdminService _adminService;
        private readonly IConfiguration configuration;
        private readonly DapperContext _context;


        public AdminService(IOptions<AppSettings> settings, IAdminService adminService, DapperContext context)
        {
            _settings = settings.Value;
            _adminService = adminService;
            _context = context ;
        }

        public async Task<GenericResponse> GetCustomerAccounts(int page, int size)
        {
            try
            {
                using IDbConnection con = _context.CreateConnection();
                int skip = page == 0 ? page : (page - 1) * size;
                int take = size;
                var listofAccounts =   (await con.QueryAsync<AccountDto>("select * from accounts  limit @Take offset @Skip", new { Take=take,Skip=skip})).ToList();
                  
                return new PrimeAdminResponse() {Response=EnumResponse.Successful,Success=true,Data=listofAccounts };
            }
            catch (Exception ex)
            {
                return new GenericResponse() { Response = EnumResponse.SystemError,Message=ex.Message };
            }
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
