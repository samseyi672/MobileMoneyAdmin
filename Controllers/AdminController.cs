using AccountAndTransactions.genericobj;
using AccountAndTransactions.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AccountAndTransactions.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {

        private readonly AppSettings _settings;
        private readonly IAdminService _adminService;

        public AdminController(IOptions<AppSettings> settings, IAdminService adminService)
        {
            _settings = settings.Value;
            _adminService = adminService;
        }

       

    }
}









































































