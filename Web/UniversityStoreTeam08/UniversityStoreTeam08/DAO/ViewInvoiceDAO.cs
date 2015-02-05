using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class ViewInvoiceDAO
    {

       public static List<Object> pupulateInvoice(string supName)
        {
           UniversityStoreEntities context = new UniversityStoreEntities();
           var b=from x in context.PurchaseOrders
                 where x.Supplier.Name == supName && x.Status=="Pending"
                 select new {
                             x.SupplierCode,
                             x.DateCreated,
                             x.Supplier.Name,
                             x.Status,
                             x.PONumber
                            };
                    
           return b.ToList<Object>();
       }

       public static List<Object> getSupplierName()
       {
           UniversityStoreEntities context = new UniversityStoreEntities();
           var q = from x in context.Suppliers
                   select x.Name;
           return q.ToList<Object>();


       }

       


       
    }
}