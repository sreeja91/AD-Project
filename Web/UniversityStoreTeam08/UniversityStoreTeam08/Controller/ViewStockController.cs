using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class ViewStockController
    {
        public static List<Object> populateAll()
        {
            return ProductDAO.selectAll();
        }


        public static List<object> findStock(string cat)
        {
            return ProductDAO.populateStock(cat);
        }


        public static string getClerk(string clerk,string username)
        {
            return EmployeeDAO.getClerkName(clerk,username);
        }

        //////****************Sreeja********************///////

        public static List<object> findStockByDescp(string descp)
        {
            return ProductDAO.populateStockByDescp(descp);
        }

        //////****************Sreeja********************///////
    }
}
