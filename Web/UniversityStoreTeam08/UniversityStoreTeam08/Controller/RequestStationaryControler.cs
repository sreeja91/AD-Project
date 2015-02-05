using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class RequestStationaryControler
    {
        public static List<Object>  acceptAllProducts()
        {
            return ProductDAO.populateAllProduct();

        }

       


        public static void addSelectedItemsToCart(string[] itemNum, int[] qty,string empNum)
        {
            for (int i = 0; i < itemNum.Count(); i++)
            {
                CartDAO.addToCart(itemNum[i], qty[i], empNum);
               
            }
           

        }

        public static void deleteSelectedFromCart(String iteNum, String empNum)
        {
            CartDAO.deleteItemFromCart(iteNum, empNum);
        }

        public static void submitCart(string empNum)
        {
            RequisitionDAO.createRequisitionDetails(empNum);
            CartDAO.deleteEmployeeCart(empNum);

        }

        public static List<Object> viewCart(string empNumber)
        {
           return CartDAO.viewCart(empNumber);

        }

        public static List<RequestStationaryCart> getEmployeeCart(string empNumber)
        {
            return CartDAO.getEmployeeCart(empNumber);

        }

        public static List<Object> displaySerachResults(string searchString)
        {
            return ProductDAO.getSearchResultbyname(searchString);
        }


        public static void addItemToCart(string itemNum, int qty, string empNum)  ///for WCF
        {
            CartDAO.addToCart(itemNum, qty, empNum);
        }

        public static List<RequestStationaryCart> viewCartW(string empNumber)
        {
            return CartDAO.viewCartWCF(empNumber);

        }

        public static int getCartSize(string empNum)
        {
            return CartDAO.getCartSize(empNum);
        }



    }
}
