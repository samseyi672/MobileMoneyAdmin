using AccountAndTransactions.dto.request;
using AccountAndTransactions.dto.response;
using AccountAndTransactions.genericobj;
using AccountAndTransactions.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [AllowAnonymous]
        [HttpGet("GetCustomerAccounts")]
        public async Task<GenericResponse> GetCustomerAccounts([FromQuery] int page,[FromQuery] int size)
        {
            return await _adminService.GetCustomerAccounts(page, size);
        }

        /// <summary>
        /// Get Account balance
        /// </summary>
        [AllowAnonymous]
        [HttpGet("getListOfTransactions")]
        public List<TransactionDto> getListOfTransactions([FromQuery] int page,[FromQuery] int size)
        {
           return _adminService.getListOfTransactions(page, size);
        }
    }
}









































































