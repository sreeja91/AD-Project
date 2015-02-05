using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08.Controller
{
    public class StoreSummaryController
    {

        public static List<Product> StockIsLow()
        {
            return ProductDAO.StockIsLow();
        
        }

        public static List<int> getTotalInventoryBalance()
        {
            return ProductDAO.getTotalInventoryBalance();  
        }


    }
}