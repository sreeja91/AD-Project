using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class ViewInvoiceController
    {
        public static List<Object> appearInvoice(string supName)
        {
            return ViewInvoiceDAO.pupulateInvoice(supName);
        }

        public static List<Object> getSuppliers()
        {
            return ViewInvoiceDAO.getSupplierName();
        }
    }
}