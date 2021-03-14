using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using RMDesktop.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSaleData(SaleModel saleInfo, string cashierId)
        {
            // TODO : Make this SOLID DRY better
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            var taxRate = ConfigHelper.GetTaxRate() / 100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = (new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });

                // Get the information about this product
                var productInfo = products.GetProductById(detail.ProductId);

                if(productInfo == null)
                {
                    throw new Exception($"The product id of { detail.ProductId } could not be found in that database.");
                }

                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if(productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                details.Add(detail);
            }

            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };
            sale.Total = sale.SubTotal + sale.Tax;

            
            using(SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("RMData");

                    sql.SaveDataInTransaction("dbo.spSaleInsert", sale);

                    sale.Id = sql.LoadDataInTransaction<int, dynamic>("dbo.spSaleLookup", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

                    foreach (var item in details)
                    {
                        item.SaleId = sale.Id;
                        sql.SaveDataInTransaction("dbo.spSaleDetailInsert", item);
                    }

                    sql.CommitTransaction();
                }
                catch (Exception ex)
                {
                    sql.RollbackTransaction();
                    throw;
                }

            }            
        }
    }
}
