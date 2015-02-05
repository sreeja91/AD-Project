using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
namespace UniversityStoreTeam08
{
    class PurchaseOrderDAO
    {

        public static void CreatePurchaseOrder(PurchaseOrder purchaseorder,List< PurchaseOrderDetail> purchaselist)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();  
            EntityCollection<PurchaseOrderDetail> purchaseorderdetailcoll = new EntityCollection<PurchaseOrderDetail>();
                foreach(PurchaseOrderDetail p in purchaselist){
                purchaseorderdetailcoll.Add(p);
                }
            
            purchaseorder.PurchaseOrderDetails = purchaseorderdetailcoll;

            context.AddToPurchaseOrders(purchaseorder);
            context.SaveChanges();
    
        }

        //////****************Ashwin********************///////

        public static void changequantityinstock(int ponumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.PurchaseOrderDetails
                    where x.PONumber == ponumber
                    select x;
            List<PurchaseOrderDetail> pod = q.ToList<PurchaseOrderDetail>();
            foreach (PurchaseOrderDetail p in pod)
            {
                var z = from x in context.Stocks
                        where x.ItemNumber == p.ItemNumber
                        select x;
                Stock s = z.First<Stock>();
                s.TotalInventoryBalance = s.TotalInventoryBalance + p.Quantity;
                context.SaveChanges();
            }

        }

        public static void ChangeStatusToApproved(int ponumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            PurchaseOrder po = context.PurchaseOrders.First<PurchaseOrder>(x => x.PONumber == ponumber);
            po.Status = "Approved";
            context.SaveChanges();
        }

        public static List<PurchaseOrderDetail> AcceptSupply(int PONumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<PurchaseOrderDetail> purchaseorderdetail = new List<PurchaseOrderDetail>();
            var z = from x in context.PurchaseOrderDetails
                    where x.PONumber == PONumber
                    select x;

            List<PurchaseOrderDetail> pli = new List<PurchaseOrderDetail>();
            pli = z.ToList<PurchaseOrderDetail>();
            return pli;
        }
        public static List<PurchaseOrder> getsupplies(String SupplierID)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.PurchaseOrders
                    where x.SupplierCode == SupplierID && x.Status == "Pending"
                    select x;

            List<PurchaseOrder> po = q.ToList<PurchaseOrder>();
            return po;
        }

        ///////// end by ashwin

       
      
        


        //To Be ADDED IN SUPPLIER DAO
        public static List<Supplier> GetallSupplier()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.Suppliers
                    select x;
            List<Supplier> sup = q.ToList<Supplier>();
            return sup;

        }

        //TO BE ADDED IN SUPPLIER DAO
        public static double GetPrice(String PID, String SupplierID)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.SupplyDetails
                    where x.ItemNumber == PID && x.SupplierCode == SupplierID
                    select x;
            double price =Convert.ToDouble (q.First<SupplyDetail>().Price);
            return price;

        }


        ////////////********Tiger***********//////////////
        public static ArrayList GetPendingPurchaseOrders()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<string> po_list = new List<string>();
            List<string> name_list = new List<string>();
            List<DateTime> date_list = new List<DateTime>();
            ArrayList ar = new ArrayList();
            var q = from po in context.PurchaseOrders
                    from sup in context.Suppliers
                    where po.SupplierCode == sup.SupplierCode
                    && po.Status == "Pending"
                    orderby po.PONumber descending
                    select new {po.PONumber,sup.Name,po.DateCreated};
            foreach (var a in q)
            {
                po_list.Add(a.PONumber.ToString());
                name_list.Add(a.Name);
                date_list.Add((DateTime)a.DateCreated);
            }
            ar.Add(po_list.ToArray());
            ar.Add(name_list.ToArray());
            ar.Add(date_list.ToArray());
            return ar;
        }


        public static List<PurchaseOrderDetail> GetPendingItemsList(int poNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q=from pod in context.PurchaseOrderDetails
                  where pod.PONumber==poNumber
                  select pod;
            return q.ToList<PurchaseOrderDetail>();
        }
        ////////////********Tiger***********//////////////

        /*** added by wu
         * */
        public static string STATUS_ACKNOWLEGED = "acknowledge";
        public static string STATUS_PENDING = "pending";



        public static List<Object> getAllOrder(int pn)
        {
            //if (pn != null)
            // {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.PurchaseOrderDetails
                    where x.PurchaseOrder.PONumber == pn
                    select
                           new { x.ItemNumber, x.Description, x.Price, x.Quantity, x.Product.UnitOfMeasure };
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
            context.SaveChanges();


        }



        /** end by wu
         * */
        public static int generatePO(string sCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            PurchaseOrder po = new PurchaseOrder();
            po.SupplierCode = sCode;
            po.Status = "Pending";
            po.DateCreated = DateTime.Now;
            context.AddToPurchaseOrders(po);
            context.SaveChanges();

            return po.PONumber;
        }

        public static void generateDetails(int poNumber, string price, string itemNum, int qty,string desc)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            PurchaseOrderDetail pdetail = new PurchaseOrderDetail();
            pdetail.PONumber = poNumber;
            pdetail.Price = Convert.ToDouble(price);
            pdetail.ItemNumber = itemNum;
            pdetail.Description = desc;
            pdetail.Quantity = Convert.ToInt32(qty);
            
            context.AddToPurchaseOrderDetails(pdetail);

            context.SaveChanges();


        }



        }

    }

  //////****************Ashwin********************///////
