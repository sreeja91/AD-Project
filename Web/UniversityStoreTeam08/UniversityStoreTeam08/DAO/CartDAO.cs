using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class CartDAO
    {

        public static void addToCart(string itemNumber,int qty, string empNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            RequestStationaryCart c = new RequestStationaryCart();
            c.EmployeeNumber = empNumber;
            c.ItemCode = itemNumber;
            c.Quantity = Convert.ToInt32(qty);

            context.AddToRequestStationaryCarts(c);

            context.SaveChanges();

        }

         
        //public static  List<Product> getEmployeeCart(string empNumber)
        //{
        //    List<Product> pList = new List<Product>();
        //   var q=from x in context.RequestStationaryCarts where x.EmployeeNumber==empNumber select x;
        //   List<RequestStationaryCart> cList = q.ToList<RequestStationaryCart>();
        //   for (int i = 0; i < cList.Count(); i++)
        //   {
        //       String g = cList[i].ItemCode.ToString();
        //      Product p = context.Products.First<Product>(x => x.ItemNumber == g);
        //      pList.Add(p);

              
        //   }

        //   return pList;

        //}

       

        public static void deleteEmployeeCart(String empno)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            context = new UniversityStoreEntities();
            var q = from x in context.RequestStationaryCarts
                    where x.EmployeeNumber == empno
                    select x;
            if (q != null)
            {
                foreach (RequestStationaryCart rsc in q)
                    context.DeleteObject(rsc);
                context.SaveChanges();
            }

        }

        public static List<RequestStationaryCart> getEmployeeCart(String empno)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            context = new UniversityStoreEntities();
            var q = from x in context.RequestStationaryCarts
                    where x.EmployeeNumber == empno
                    select x;
            return q.ToList();
        }

        public static List<Object> viewCart(string empNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.RequestStationaryCarts
                    where x.EmployeeNumber == empNumber
                    select new { x.ItemCode, x.Quantity, x.EmployeeNumber };
            return q.ToList<Object>();


        }

        public static void deleteItemFromCart(string itemNum, string empNum)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.RequestStationaryCarts
                    where x.EmployeeNumber == empNum && x.ItemCode == itemNum
                    select x;
            RequestStationaryCart rc = q.First<RequestStationaryCart>();
            context.DeleteObject(rc);
            context.SaveChanges();
        }

        /****added by nitin 
         * */
        public static int getCartSize(string empNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.RequestStationaryCarts
                    where x.EmployeeNumber == empNumber
                    select x;
            int e = q.ToArray<RequestStationaryCart>().Count();
            return e;

        }

        public static List<RequestStationaryCart> viewCartWCF(string empNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.RequestStationaryCarts
                    where x.EmployeeNumber == empNumber
                    select x;
            return q.ToList<RequestStationaryCart>();
        }

        
    }
}
