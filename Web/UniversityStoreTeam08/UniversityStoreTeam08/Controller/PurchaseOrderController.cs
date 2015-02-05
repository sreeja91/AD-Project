using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class PurchaseOrderController
    {

        //public static UniversityStoreEntities context = new UniversityStoreEntities();

        public static List<Product> ProductWithLowStock()
        {
            List<Product> pr = ProductDAO.StockIsLow();
            return pr;
        }

        public static List<int> Reorderlevel()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<Product> prod = ProductWithLowStock();
            List<int> tot = new List<int>();
            foreach (Product p in prod)
            {
                int s = 0;
                var q = from x in context.Unfulfilleds
                        where x.Status == "Pending" && x.ItemNumber == p.ItemNumber
                        select x;
                var z = from x in context.Products
                        where x.ItemNumber == p.ItemNumber
                        select x;
                var y = from x in context.RequisitionLists
                        join
                            t in context.RequisitionDetails on x.RequisitionListNumber equals t.RequisitionListNumber
                        where x.Status == "Approved" && t.ItemNmber == p.ItemNumber
                        select t;
                List<RequisitionDetail> crld = y.ToList<RequisitionDetail>();

                foreach (RequisitionDetail v in crld)
                {
                    s = s + Convert.ToInt32(v.Quantity);
                }

                int sum = 0;
                if (q.FirstOrDefault<Unfulfilled>() != null)
                {
                    sum = Convert.ToInt32(q.First<Unfulfilled>().UnfulfilledQuantity + z.First<Product>().ReorderQuantity) + s;
                }
                else
                {
                    sum = Convert.ToInt32(z.First<Product>().ReorderQuantity) + s;
                }
                tot.Add(sum);

            }
            return tot;
        }

        //public static void GetSupplyOrder(String[] PID, String[] SuppID, int[] QNTY, String[] SuppID2, int[] QNTY2, String[] SuppID3, int[] QNTY3)
        //{
            //UniversityStoreEntities context = new UniversityStoreEntities();
            //int i = 0; int j = 0;

            //PurchaseOrder pomain = new PurchaseOrder();
            //PurchaseOrder pomain2 = new PurchaseOrder();
            //PurchaseOrder pomain3 = new PurchaseOrder();

            //List<PurchaseOrderDetail> lpod = new List<PurchaseOrderDetail>();
            //List<PurchaseOrderDetail> lpod2 = new List<PurchaseOrderDetail>();
            //List<PurchaseOrderDetail> lpod3 = new List<PurchaseOrderDetail>();



            //foreach (String p in PID)
            //{
            //    var q = from x in context.Products
            //            where x.ItemNumber == p
            //            select x;
              
            //    PurchaseOrder po = new PurchaseOrder();
            //    if (SuppID[i].Equals(q.First<Product>().Supplier1ID) || SuppID[i].Equals(q.First<Product>().Supplier2ID) || SuppID[i].Equals(q.First<Product>().Supplier3ID))
            //    {
            //        if (SuppID[i].Equals(q.First<Product>().Supplier1ID))
            //        {
            //            po.SupplierCode = q.First<Product>().Supplier1ID;
            //            po.DateCreated = DateTime.Now;
            //            po.Status = "Pending";              
            //            pomain = po;
                      

                     
            //        }
            //        else if (SuppID[i].Equals(q.First<Product>().Supplier2ID))
            //        {
            //            po.SupplierCode = q.First<Product>().Supplier2ID;
            //            po.DateCreated = DateTime.Now;
            //            po.Status = "Pending";
            //                pomain2 = po;
                        
            //        }
            //        else if (SuppID[i].Equals(q.First<Product>().Supplier3ID))
            //        {
            //            po.SupplierCode = q.First<Product>().Supplier3ID;
            //            po.DateCreated = DateTime.Now;
            //            po.Status = "Pending";
                        
            //                pomain3 = po;
                      
            //        }



            //        if (SuppID[i].Equals(q.First<Product>().Supplier1ID))
            //        {
            //            PurchaseOrderDetail pod = new PurchaseOrderDetail();

            //            var q1 = from x in context.Products
            //                     where x.ItemNumber == p
            //                     select x;
            //            pod.ItemNumber = p;
            //            pod.Description = q.First<Product>().Description;
            //            pod.Quantity = QNTY[i];
            //            String g = q1.First<Product>().Supplier1ID;
            //            var z = from x in context.SupplyDetails
            //                    where x.SupplierCode == g && x.ItemNumber == p
            //                    select x;
            //            SupplyDetail s = z.First<SupplyDetail>();

            //            pod.Price = s.Price;
                       
            //            lpod.Add(pod);

            //        }
            //        else if (SuppID[i].Equals(q.First<Product>().Supplier2ID))
            //        {
            //            PurchaseOrderDetail pod = new PurchaseOrderDetail();

            //            var q1 = from x in context.Products
            //                     where x.ItemNumber == p
            //                     select x;
            //            pod.ItemNumber = p;
            //            pod.Description = q.First<Product>().Description;
            //            pod.Quantity = QNTY[i];
            //            String g = q1.First<Product>().Supplier2ID;
            //            var z = from x in context.SupplyDetails
            //                    where x.SupplierCode == g && x.ItemNumber == p
            //                    select x;

            //            pod.Price = z.First<SupplyDetail>().Price;
                       
            //            lpod2.Add(pod);

            //        }
            //        else if (SuppID[i].Equals(q.First<Product>().Supplier3ID))
            //        {
            //            PurchaseOrderDetail pod = new PurchaseOrderDetail();

            //            var q1 = from x in context.Products
            //                     where x.ItemNumber == p
            //                     select x;
            //            pod.ItemNumber = p;
            //            pod.Description = q1.First<Product>().Description;
            //            pod.Quantity = QNTY[i];
            //            String g = q1.First<Product>().Supplier3ID;
            //            var z = from x in context.SupplyDetails
            //                    where x.SupplierCode == g && x.ItemNumber == p
            //                    select x;

            //            pod.Price = z.First<SupplyDetail>().Price;
                      
            //            lpod3.Add(pod);

            //        }


            //    }
            //    i++;

            //}



            //foreach (String p in PID)
            //{

            //    PurchaseOrder po2 = new PurchaseOrder();
            //    var q = from x in context.Products
            //            where x.ItemNumber == p
            //            select x;
                
            //    if (SuppID2[j].Equals(q.First<Product>().Supplier1ID) || SuppID2[j].Equals(q.First<Product>().Supplier2ID) || SuppID2[j].Equals(q.First<Product>().Supplier3ID))
            //    {
            //        String t = q.First<Product>().Supplier1ID;
            //        if (SuppID2[j].Equals(q.First<Product>().Supplier1ID))
            //        {       

            //            po2.SupplierCode = q.First<Product>().Supplier1ID;
            //            po2.DateCreated = DateTime.Now;
            //            po2.Status = "Pending";
                           
                        
                       
                            
                       
            //                pomain = po2;
                       
                      
                     
            //        }
            //        else if (SuppID2[j].Equals(q.First<Product>().Supplier2ID))
            //        {
            //            po2.SupplierCode = q.First<Product>().Supplier2ID;
            //            po2.DateCreated = DateTime.Now;
            //            po2.Status = "Pending";
            //            pomain2 = po2;
                        
                       
                        
                            
                        
            //        }
            //        else if (SuppID2[j].Equals(q.First<Product>().Supplier3ID))
            //        {
            //            po2.SupplierCode = q.First<Product>().Supplier3ID;
            //            po2.DateCreated = DateTime.Now;
            //            po2.Status = "Pending";
            //            pomain3 = po2;
                       
            //                pomain3 = po2;
                        
            //        }




            //        if (SuppID2[j].Equals(q.First<Product>().Supplier1ID) && QNTY2[j] > 0)
            //        {
            //            PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
            //            var q2 = from x in context.Products
            //                     where x.ItemNumber == p
            //                     select x;
            //            pod2.ItemNumber = p;
            //            pod2.Description = q2.First<Product>().Description;
            //            pod2.Quantity = QNTY2[j];
            //            String g = q2.First<Product>().Supplier1ID;
            //            var z = from x in context.SupplyDetails
            //                    where x.SupplierCode == g && x.ItemNumber == p
            //                    select x;

            //            pod2.Price = z.First<SupplyDetail>().Price;
                        
            //           lpod.Add(pod2);

            //        }
            //        else if (SuppID2[j].Equals(q.First<Product>().Supplier2ID) && QNTY2[j] > 0)
            //        {
            //            PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
            //            var q2 = from x in context.Products
            //                     where x.ItemNumber == p
            //                     select x;
            //            pod2.ItemNumber = p;
            //            pod2.Description = q2.First<Product>().Description;
            //            pod2.Quantity = QNTY2[j];
            //            String g = q2.First<Product>().Supplier2ID;
            //            var z = from x in context.SupplyDetails
            //                    where x.SupplierCode == g && x.ItemNumber == p
            //                    select x;

            //            pod2.Price = z.First<SupplyDetail>().Price;
                      
            //            lpod2.Add(pod2);

            //        }
            //        else if (SuppID2[j].Equals(q.First<Product>().Supplier3ID) && QNTY2[j] > 0)
            //        {
            //            PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
            //            var q2 = from x in context.Products
            //                     where x.ItemNumber == p
            //                     select x;
            //            pod2.ItemNumber = p;
            //            pod2.Description = q2.First<Product>().Description;
            //            pod2.Quantity = QNTY2[j];
            //            String g = q2.First<Product>().Supplier3ID;
            //            var z = from x in context.SupplyDetails
            //                    where x.SupplierCode == g && x.ItemNumber == p
            //                    select x;

            //            pod2.Price = z.First<SupplyDetail>().Price;
                        
            //            lpod3.Add(pod2);

            //        }

            //    }
            //    j++;

            //}

            //int k = 0;


            //foreach (String p in PID)
            //{
            //    PurchaseOrder po3 = new PurchaseOrder();
            //    var q = from x in context.Products
            //            where x.ItemNumber == p
            //            select x;
              

            //    if (SuppID3[k].Equals(q.First<Product>().Supplier1ID) || SuppID3[k].Equals(q.First<Product>().Supplier2ID) || SuppID3[k].Equals(q.First<Product>().Supplier3ID))
            //    {
            //        if (SuppID3[k].Equals(q.First<Product>().Supplier1ID))
            //        {
            //            po3.SupplierCode = q.First<Product>().Supplier1ID;
            //            po3.DateCreated = DateTime.Now;
            //            po3.Status = "Pending";
            //            pomain = po3;
                       
                            
                      
                       
                       
            //        }
            //        else if (SuppID3[k].Equals(q.First<Product>().Supplier2ID))
            //        {
            //            po3.SupplierCode = q.First<Product>().Supplier2ID;
            //            po3.DateCreated = DateTime.Now;
            //            po3.Status = "Pending";
            //            pomain2 = po3;
                      
            //        }
            //        else if (SuppID3[k].Equals(q.First<Product>().Supplier3ID))
            //        {
            //            po3.SupplierCode = q.First<Product>().Supplier3ID;
            //            po3.DateCreated = DateTime.Now;
            //            po3.Status = "Pending";
            //            pomain3 = po3;
                      
            //        }





            //        if (SuppID3[k].Equals(q.First<Product>().Supplier1ID) && QNTY3[k] > 0)
            //        {
            //            PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
            //            var q3 = from x in context.Products
            //                     where x.ItemNumber == p
            //                     select x;
            //            pod3.ItemNumber = p;
            //            pod3.Description = q3.First<Product>().Description;
            //            pod3.Quantity = QNTY3[k];
            //            String g = q3.First<Product>().Supplier1ID;
            //            var z = from x in context.SupplyDetails
            //                    where x.SupplierCode == g && x.ItemNumber == p
            //                    select x;

            //            pod3.Price = z.First<SupplyDetail>().Price;
                        
            //            lpod.Add(pod3);

            //        }
            //        else if (SuppID3[k].Equals(q.First<Product>().Supplier2ID) && QNTY3[k] > 0)
            //        {
            //            PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
            //            var q3 = from x in context.Products
            //                     where x.ItemNumber == p
            //                     select x;
            //            pod3.ItemNumber = p;
            //            pod3.Description = q3.First<Product>().Description;
            //            pod3.Quantity = QNTY3[k];
            //            String g = q3.First<Product>().Supplier2ID;
            //            var z = from x in context.SupplyDetails
            //                    where x.SupplierCode == g && x.ItemNumber == p
            //                    select x;

            //            pod3.Price = z.First<SupplyDetail>().Price; 
            //            lpod2.Add(pod3);


            //        }
            //        else if (SuppID3[k].Equals(q.First<Product>().Supplier3ID) && QNTY3[k] > 0)
            //        {
            //            PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
            //            var q3 = from x in context.Products
            //                     where x.ItemNumber == p
            //                     select x;
            //            pod3.ItemNumber = p;
            //            pod3.Description = q3.First<Product>().Description;
            //            pod3.Quantity = QNTY3[k];
            //            String g = q3.First<Product>().Supplier3ID;
            //            var z = from x in context.SupplyDetails
            //                    where x.SupplierCode == g && x.ItemNumber == p
            //                    select x;

            //            pod3.Price = z.First<SupplyDetail>().Price;
                      
            //            lpod3.Add(pod3);

            //        }
            //    }
            //    k++;

            //}
            //if (pomain.SupplierCode != null&& lpod.Count>0)
            //    PurchaseOrderDAO.CreatePurchaseOrder(pomain, lpod);
            //if (pomain2.SupplierCode != null&&lpod2.Count>0)
            //    PurchaseOrderDAO.CreatePurchaseOrder(pomain2, lpod2);
            //if (pomain3.SupplierCode != null&&lpod3.Count>0)
            //    PurchaseOrderDAO.CreatePurchaseOrder(pomain3, lpod3);

            public static void GetSupplyOrder(String[] PID, String[] SuppID, int[] QNTY, String[] SuppID2, int[] QNTY2, String[] SuppID3, int[] QNTY3)
        {   
            List<PurchaseOrder> po=new List<PurchaseOrder>();
             List<string> currentSuppylists = new List<string>();
        List<Int32> supplierPOid = new List<Int32>();
        //PurchaseOrder pon = new PurchaseOrder();
        List<PurchaseOrderDetail> pod = new List<PurchaseOrderDetail>();
            UniversityStoreEntities context = new UniversityStoreEntities();
            int i = 0; int j = 0;
            foreach(String s in PID)
            {
                var q = from x in context.Products
                        where x.ItemNumber == s
                        select x;
                Product product = q.First<Product>();
                string description = product.Description;
                if (SuppID[i].Equals(q.First<Product>().Supplier1ID) || SuppID[i].Equals(q.First<Product>().Supplier2ID) || SuppID[i].Equals(q.First<Product>().Supplier3ID))
                {
                    if (SuppID[i].Equals(q.First<Product>().Supplier1ID))
                    {       string supplir=SuppID[i];
                        var kk = from x in context.SupplyDetails
                                where x.SupplierCode == supplir && x.ItemNumber == s
                                select x;
                        SupplyDetail ssd = kk.First<SupplyDetail>();
                        string priceee = ssd.Price.ToString();
                        string supID = q.First<Product>().Supplier1ID;
                        if (currentSuppylists.Contains(supID))
                        {
                            int pos = currentSuppylists.IndexOf(supID);
                            int ppid = supplierPOid[pos];
                            PurchaseOrderDAO.generateDetails(ppid, priceee, s, QNTY[i], description);
                        }
                        else
                        {
                            int pid = PurchaseOrderDAO.generatePO(supID);
                            supplierPOid.Add(pid);
                            currentSuppylists.Add(supID);
                            PurchaseOrderDAO.generateDetails(pid, priceee, s, QNTY[i], description);
 
                        }
                        
 
                    }
                    if (SuppID[i].Equals(q.First<Product>().Supplier2ID))
                    {
                        string supplir = SuppID[i];
                        var kk = from x in context.SupplyDetails
                                 where x.SupplierCode == supplir && x.ItemNumber == s
                                 select x;
                        SupplyDetail ssd = kk.First<SupplyDetail>();
                        string priceee = ssd.Price.ToString();
                        string supID = q.First<Product>().Supplier2ID;
                        if (currentSuppylists.Contains(supID))
                        {
                            int pos = currentSuppylists.IndexOf(supID);
                            int ppid = supplierPOid[pos];
                            PurchaseOrderDAO.generateDetails(ppid, priceee, s, QNTY[i], description);
                        }
                        else
                        {
                            int pid = PurchaseOrderDAO.generatePO(supID);
                            supplierPOid.Add(pid);
                            currentSuppylists.Add(supID);
                            PurchaseOrderDAO.generateDetails(pid, priceee, s, QNTY[i], description);
                        }
                    }
                    if (SuppID[i].Equals(q.First<Product>().Supplier3ID))
                    {
                        string supplir = SuppID[i];
                        var kk = from x in context.SupplyDetails
                                 where x.SupplierCode == supplir && x.ItemNumber == s
                                 select x;
                        SupplyDetail ssd = kk.First<SupplyDetail>();
                        string priceee = ssd.Price.ToString();
                        string supID = q.First<Product>().Supplier3ID;
                        if (currentSuppylists.Contains(supID))
                        {
                            int pos = currentSuppylists.IndexOf(supID);
                            int ppid = supplierPOid[pos];
                            PurchaseOrderDAO.generateDetails(ppid, priceee, s, QNTY2[i], description);
                        }
                        else
                        {
                            int pid = PurchaseOrderDAO.generatePO(supID);
                            supplierPOid.Add(pid);
                            currentSuppylists.Add(supID);
                            PurchaseOrderDAO.generateDetails(pid, priceee, s, QNTY[i], description);
                        }
                    }
                    
                }
                if (SuppID2[i].Equals(q.First<Product>().Supplier1ID) || SuppID2[i].Equals(q.First<Product>().Supplier2ID) || SuppID2[i].Equals(q.First<Product>().Supplier3ID))
                {
                    if (SuppID2[i].Equals(q.First<Product>().Supplier1ID))
                    {
                        string supplir = SuppID2[i];
                        var kk = from x in context.SupplyDetails
                                 where x.SupplierCode == supplir && x.ItemNumber == s
                                 select x;
                        SupplyDetail ssd = kk.First<SupplyDetail>();
                        string priceee = ssd.Price.ToString();
                        string supID = q.First<Product>().Supplier1ID;
                        if (currentSuppylists.Contains(supID))
                        {
                            int pos = currentSuppylists.IndexOf(supID);
                            int ppid = supplierPOid[pos];
                            PurchaseOrderDAO.generateDetails(ppid, priceee, s, QNTY2[i], description);
                        }
                        else
                        {
                            int pid = PurchaseOrderDAO.generatePO(supID);
                            supplierPOid.Add(pid);
                            currentSuppylists.Add(supID);
                            PurchaseOrderDAO.generateDetails(pid, priceee, s, QNTY2[i], description);
                        }
                    }
                    if (SuppID2[i].Equals(q.First<Product>().Supplier2ID))
                    {
                        string supplir = SuppID2[i];
                        var kk = from x in context.SupplyDetails
                                 where x.SupplierCode == supplir && x.ItemNumber == s
                                 select x;
                        SupplyDetail ssd = kk.First<SupplyDetail>();
                        string priceee = ssd.Price.ToString();
                        string supID = q.First<Product>().Supplier2ID;
                        if (currentSuppylists.Contains(supID))
                        {
                            int pos = currentSuppylists.IndexOf(supID);
                            int ppid = supplierPOid[pos];
                            PurchaseOrderDAO.generateDetails(ppid, priceee, s, QNTY2[i], description);
                        }
                        else
                        {
                            int pid = PurchaseOrderDAO.generatePO(supID);
                            supplierPOid.Add(pid);
                            currentSuppylists.Add(supID);
                            PurchaseOrderDAO.generateDetails(pid, priceee, s, QNTY2[i], description);
                        }
                    }
                    if (SuppID2[i].Equals(q.First<Product>().Supplier3ID))
                    {
                        string supplir = SuppID2[i];
                        var kk = from x in context.SupplyDetails
                                 where x.SupplierCode == supplir && x.ItemNumber == s
                                 select x;
                        SupplyDetail ssd = kk.First<SupplyDetail>();
                        string priceee = ssd.Price.ToString();
                        string supID = q.First<Product>().Supplier3ID;
                        if (currentSuppylists.Contains(supID))
                        {
                            int pos = currentSuppylists.IndexOf(supID);
                            int ppid = supplierPOid[pos];
                            PurchaseOrderDAO.generateDetails(ppid, priceee, s, QNTY2[i], description);
                        }
                        else
                        {
                            int pid = PurchaseOrderDAO.generatePO(supID);
                            currentSuppylists.Add(supID);
                            supplierPOid.Add(pid);
                            PurchaseOrderDAO.generateDetails(pid, priceee, s, QNTY2[i], description);
                        }
                    }
 
                }
                if (SuppID3[i].Equals(q.First<Product>().Supplier1ID) || SuppID3[i].Equals(q.First<Product>().Supplier2ID) || SuppID3[i].Equals(q.First<Product>().Supplier3ID))
                {
                    if (SuppID3[i].Equals(q.First<Product>().Supplier1ID))
                    {
                        string supplir = SuppID3[i];
                        var kk = from x in context.SupplyDetails
                                 where x.SupplierCode == supplir && x.ItemNumber == s
                                 select x;
                        SupplyDetail ssd = kk.First<SupplyDetail>();
                        string priceee = ssd.Price.ToString();
                        string supID = q.First<Product>().Supplier1ID;
                        if (currentSuppylists.Contains(supID))
                        {
                            int pos = currentSuppylists.IndexOf(supID);
                            int ppid = supplierPOid[pos];
                            PurchaseOrderDAO.generateDetails(ppid, priceee, s, QNTY3[i], description);
                        }
                        else
                        {
                            int pid = PurchaseOrderDAO.generatePO(supID);
                            currentSuppylists.Add(supID);
                            supplierPOid.Add(pid);
                            PurchaseOrderDAO.generateDetails(pid, priceee, s, QNTY3[i], description);
                        }
                    }
                    if (SuppID3[i].Equals(q.First<Product>().Supplier2ID))
                    {
                        string supplir = SuppID3[i];
                        var kk = from x in context.SupplyDetails
                                 where x.SupplierCode == supplir && x.ItemNumber == s
                                 select x;
                        SupplyDetail ssd = kk.First<SupplyDetail>();
                        string priceee = ssd.Price.ToString();
                        string supID = q.First<Product>().Supplier2ID;
                        if (currentSuppylists.Contains(supID))
                        {
                            int pos = currentSuppylists.IndexOf(supID);
                            int ppid = supplierPOid[pos];
                            PurchaseOrderDAO.generateDetails(ppid, priceee, s, QNTY3[i], description);
                        }
                        else
                        {
                            int pid = PurchaseOrderDAO.generatePO(supID);
                            currentSuppylists.Add(supID);
                            supplierPOid.Add(pid);
                            PurchaseOrderDAO.generateDetails(pid, priceee, s, QNTY3[i], description);
                        }
                    }
                    if (SuppID3[i].Equals(q.First<Product>().Supplier3ID))
                    {
                        string supplir = SuppID[i];
                        var kk = from x in context.SupplyDetails
                                 where x.SupplierCode == supplir && x.ItemNumber == s
                                 select x;
                        SupplyDetail ssd = kk.First<SupplyDetail>();
                        string priceee = ssd.Price.ToString();
                        string supID = q.First<Product>().Supplier3ID;
                        if (currentSuppylists.Contains(supID))
                        {
                            int pos = currentSuppylists.IndexOf(supID);
                            int ppid = supplierPOid[pos];
                            PurchaseOrderDAO.generateDetails(ppid, priceee, s, QNTY3[i], description);
                        }
                        else
                        {
                            int pid = PurchaseOrderDAO.generatePO(supID);
                            currentSuppylists.Add(supID);
                            supplierPOid.Add(pid);
                            PurchaseOrderDAO.generateDetails(pid, priceee, s, QNTY3[i], description);
                        }
                    }
                }
                i++;
                
            }
          
           
            //if (pomain.SupplierCode != null)
            //    PurchaseOrderDAO.CreatePurchaseOrder(pomain, lpod);
            //if (pomain2.SupplierCode != null)
            //    PurchaseOrderDAO.CreatePurchaseOrder(pomain2, lpod2);
            //if (pomain3.SupplierCode != null)
            //    PurchaseOrderDAO.CreatePurchaseOrder(pomain3, lpod3);
        }
        


        public static ArrayList GetPendingPurchaseOrder()
        {
            return PurchaseOrderDAO.GetPendingPurchaseOrders();
        }

        public static List<PurchaseOrderDetail> GetPendingItemsList(int poNumber)
        {
            return PurchaseOrderDAO.GetPendingItemsList(poNumber);
        }







        public static List<PurchaseOrder> getsupplies(String SupplierID)
        {


            List<PurchaseOrder> po = PurchaseOrderDAO.getsupplies(SupplierID);
            return po;
        }

        public static List<PurchaseOrderDetail> AcceptSupply(int PONumber)
        {

            

            List<PurchaseOrderDetail> pli = new List<PurchaseOrderDetail>();
            pli = PurchaseOrderDAO.AcceptSupply(PONumber);
            return pli;
        }
        public static void ChangeStatusToApproved(int ponumber)
        {
            PurchaseOrderDAO.ChangeStatusToApproved(ponumber);
            PurchaseOrderDAO.changequantityinstock(ponumber);
            
        }



        /** added by wu
         * */
        public static List<Object> getAllOd(int pn)
        {
            return PurchaseOrderDAO.getAllOrder(pn);
        }

        public static void changeStatus(int pn)
        {
            PurchaseOrderDAO.changeToAcknowledge(pn);
            PurchaseOrderDAO.changequantityinstock(pn);
        }
        /* end by wu
         * */

    }
}