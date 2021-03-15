using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        public IEnumerable<SaleReportModel> GetSalesReport()
        {
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }

        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            data.SaveSaleData(sale, userId);
        }
    }
}
