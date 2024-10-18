using AccountAndTransactions.dto.request;
using AccountAndTransactions.genericobj;
using AccountAndTransactions.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace AccountAndTransactions.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IOptions<AppSettings> settings, IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Get Account balance
        /// </summary>
        [HttpGet("GetCustomerAccounts")]
        public List<AccountDto> GetCustomerAccounts([FromQuery] int page,[FromQuery] int size)
        {
            return _adminService.GetCustomerAccounts(page, size);
        }

        /// <summary>
        /// Get Account balance
        /// </summary>
        [HttpGet("getListOfTransactions")]
        public List<TransactionDto> getListOfTransactions([FromQuery] int page,[FromQuery] int size)
        {
           return _adminService.getListOfTransactions(page, size);
        }
    }
}









































































