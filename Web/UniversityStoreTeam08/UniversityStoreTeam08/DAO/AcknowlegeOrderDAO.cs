using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class AcknowlegeOrderDAO
    {
        
        public static string STATUS_ACKNOWLEGED = "Approved";
        public static string STATUS_PENDING = "pending";



        public static List<Object> getAllOrder(int pn)
        {
            //if (pn != null)
           // {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.PurchaseOrderDetails
                    where x.PurchaseOrder.PONumber == pn
                    select 
                           new { x.ItemNumber, x.Description, x.Price, x.Quantity };
                return q.ToList<Object>();
               
            //}
          
        }

        public static void changeToAcknowledge(int pn)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.PurchaseOrderDetails
                    where x.PONumber == pn && x.PurchaseOrder.Status == STATUS_PENDING
                    select x;

            PurchaseOrderDetail a = q.First<PurchaseOrderDetail>();

            a.PurchaseOrder.Status = STATUS_ACKNOWLEGED;
            PurchaseOrderDAO.changequantityinstock(pn);
            context.SaveChanges();
            
                  
        }

       // public static string getpo()
    }
}