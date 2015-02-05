using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
//using PdfSharp;
//using PdfSharp.Drawing;
//using PdfSharp.Pdf;
using System.Data.SqlClient;
using System.Data;

namespace UniversityStoreTeam08
{   
     
    class RaisePurchaseOrderControllerDD
    {
        public static UniversityStoreEntities context = new UniversityStoreEntities();
      
        public static List<Product> ProductWithLowStock()
        {
          List<Product> pr=  ProductDAO.StockIsLow();
          
           
          //  foreach(Object ot in ob)
           // {Console.WriteLine(ob.ToString());}
            return pr; 

            
        }

        public static List<int> Reorderlevel()
        {
            List<Product> prod = ProductWithLowStock();
            List<int> tot = new List<int>(); int s = 0;
            foreach (Product p in prod)
            {
                var q = from x in context.Unfulfilleds
                        where x.Status == "Pending" && x.ItemNumber == p.ItemNumber
                        select x;
                var z = from x in context.Products
                        where x.ItemNumber == p.ItemNumber
                        select x;
                var y = from x in context.RequisitionLists join
                         t in context.RequisitionDetails on x.RequisitionListNumber equals t.RequisitionListNumber
                        where x.Status == "Approved" && t.ItemNmber == p.ItemNumber
                        select t;
                List<RequisitionDetail> crld = y.ToList<RequisitionDetail>();

                foreach (RequisitionDetail v in crld)
                {
                    s = s + Convert.ToInt32(v.Quantity);
                }

                int sum = 0;
                if (q.FirstOrDefault<Unfulfilled>()!= null)
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

        public static void GetSupplyOrder(String[] PID, String[] SuppID, int[] QNTY, String[] SuppID2, int[] QNTY2, String[] SuppID3, int[] QNTY3)
        {
            int i = 0; int j = 0;

            PurchaseOrder pomain = new PurchaseOrder();
            PurchaseOrder pomain2 = new PurchaseOrder();
            PurchaseOrder pomain3 = new PurchaseOrder();

            List<PurchaseOrderDetail> lpod = new List<PurchaseOrderDetail>();
            List<PurchaseOrderDetail> lpod2 = new List<PurchaseOrderDetail>();
            List<PurchaseOrderDetail> lpod3 = new List<PurchaseOrderDetail>();



            foreach (String p in PID)
            {
                var q = from x in context.Products
                        where x.ItemNumber == p
                        select x;
                PurchaseOrder po = new PurchaseOrder();
                if (SuppID[i].Equals(q.First<Product>().Supplier1ID) || SuppID[i].Equals(q.First<Product>().Supplier2ID) || SuppID[i].Equals(q.First<Product>().Supplier3ID))
                {
                    if (SuppID[i].Equals(q.First<Product>().Supplier1ID))
                    {
                        po.SupplierCode = q.First<Product>().Supplier1ID;
                        po.DateCreated = DateTime.Now;
                        po.Status = "Pending";
                        pomain = po;
                    }
                    else if (SuppID[i].Equals(q.First<Product>().Supplier2ID))
                    {
                        po.SupplierCode = q.First<Product>().Supplier2ID;
                        po.DateCreated = DateTime.Now;
                        po.Status = "Pending";
                        pomain2 = po;
                    }
                    else if (SuppID[i].Equals(q.First<Product>().Supplier3ID))
                    {
                        po.SupplierCode = q.First<Product>().Supplier3ID;
                        po.DateCreated = DateTime.Now;
                        po.Status = "Pending";
                        pomain3 = po;
                    }



                    if (SuppID[i].Equals(q.First<Product>().Supplier1ID))
                    {
                        PurchaseOrderDetail pod = new PurchaseOrderDetail();

                        var q1 = from x in context.Products
                                 where x.ItemNumber == p
                                 select x;
                        pod.ItemNumber = p;
                        pod.Description = q.First<Product>().Description;
                        pod.Quantity = QNTY[i];
                        String g = q1.First<Product>().Supplier1ID;
                        var z = from x in context.SupplyDetails
                                where x.SupplierCode == g && x.ItemNumber == p
                                select x;
                        SupplyDetail s = z.First<SupplyDetail>();

                        pod.Price = s.Price;
                        lpod.Add(pod);

                    }
                    else if (SuppID[i].Equals(q.First<Product>().Supplier2ID))
                    {
                        PurchaseOrderDetail pod = new PurchaseOrderDetail();

                        var q1 = from x in context.Products
                                 where x.ItemNumber == p
                                 select x;
                        pod.ItemNumber = p;
                        pod.Description = q.First<Product>().Description;
                        pod.Quantity = QNTY[i];
                        String g = q1.First<Product>().Supplier2ID;
                        var z = from x in context.SupplyDetails
                                where x.SupplierCode == g && x.ItemNumber == p
                                select x;

                        pod.Price = z.First<SupplyDetail>().Price;
                        lpod2.Add(pod);

                    }
                    else if (SuppID[i].Equals(q.First<Product>().Supplier3ID))
                    {
                        PurchaseOrderDetail pod = new PurchaseOrderDetail();

                        var q1 = from x in context.Products
                                 where x.ItemNumber == p
                                 select x;
                        pod.ItemNumber = p;
                        pod.Description = q1.First<Product>().Description;
                        pod.Quantity = QNTY[i];
                        String g = q1.First<Product>().Supplier3ID;
                        var z = from x in context.SupplyDetails
                                where x.SupplierCode == g && x.ItemNumber == p
                                select x;

                        pod.Price = z.First<SupplyDetail>().Price;
                        lpod3.Add(pod);

                    }


                }
                i++;

            }



            foreach (String p in PID)
            {

                PurchaseOrder po2 = new PurchaseOrder();
                var q = from x in context.Products
                        where x.ItemNumber == p
                        select x;
                if (SuppID2[j].Equals(q.First<Product>().Supplier1ID) || SuppID2[j].Equals(q.First<Product>().Supplier2ID) || SuppID2[j].Equals(q.First<Product>().Supplier3ID))
                {
                    String t = q.First<Product>().Supplier1ID;
                    if (SuppID2[j].Equals(q.First<Product>().Supplier1ID))
                    {
                        po2.SupplierCode = q.First<Product>().Supplier1ID;
                        po2.DateCreated = DateTime.Now;
                        po2.Status = "Pending";
                        pomain = po2;
                    }
                    else if (SuppID2[j].Equals(q.First<Product>().Supplier2ID))
                    {
                        po2.SupplierCode = q.First<Product>().Supplier2ID;
                        po2.DateCreated = DateTime.Now;
                        po2.Status = "Pending";
                        pomain2 = po2;
                    }
                    else if (SuppID2[j].Equals(q.First<Product>().Supplier3ID))
                    {
                        po2.SupplierCode = q.First<Product>().Supplier3ID;
                        po2.DateCreated = DateTime.Now;
                        po2.Status = "Pending";
                        pomain3 = po2;
                    }




                    if (SuppID2[j].Equals(q.First<Product>().Supplier1ID) && QNTY2[j] > 0)
                    {
                        PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
                        var q2 = from x in context.Products
                                 where x.ItemNumber == p
                                 select x;
                        pod2.ItemNumber = p;
                        pod2.Description = q2.First<Product>().Description;
                        pod2.Quantity = QNTY2[j];
                        String g = q2.First<Product>().Supplier1ID;
                        var z = from x in context.SupplyDetails
                                where x.SupplierCode == g && x.ItemNumber == p
                                select x;

                        pod2.Price = z.First<SupplyDetail>().Price;
                        lpod.Add(pod2);

                    }
                    else if (SuppID2[j].Equals(q.First<Product>().Supplier2ID) && QNTY2[j] > 0)
                    {
                        PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
                        var q2 = from x in context.Products
                                 where x.ItemNumber == p
                                 select x;
                        pod2.ItemNumber = p;
                        pod2.Description = q2.First<Product>().Description;
                        pod2.Quantity = QNTY2[j];
                        String g = q2.First<Product>().Supplier2ID;
                        var z = from x in context.SupplyDetails
                                where x.SupplierCode == g && x.ItemNumber == p
                                select x;

                        pod2.Price = z.First<SupplyDetail>().Price;
                        lpod2.Add(pod2);

                    }
                    else if (SuppID2[j].Equals(q.First<Product>().Supplier3ID) && QNTY2[j] > 0)
                    {
                        PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
                        var q2 = from x in context.Products
                                 where x.ItemNumber == p
                                 select x;
                        pod2.ItemNumber = p;
                        pod2.Description = q2.First<Product>().Description;
                        pod2.Quantity = QNTY2[j];
                        String g = q2.First<Product>().Supplier3ID;
                        var z = from x in context.SupplyDetails
                                where x.SupplierCode == g && x.ItemNumber == p
                                select x;

                        pod2.Price = z.First<SupplyDetail>().Price;
                        lpod3.Add(pod2);

                    }

                }
                j++;

            }

            int k = 0;


            foreach (String p in PID)
            {
                PurchaseOrder po3 = new PurchaseOrder();
                var q = from x in context.Products
                        where x.ItemNumber == p
                        select x;

                if (SuppID3[k].Equals(q.First<Product>().Supplier1ID) || SuppID3[k].Equals(q.First<Product>().Supplier2ID) || SuppID3[k].Equals(q.First<Product>().Supplier3ID))
                {
                    if (SuppID3[k].Equals(q.First<Product>().Supplier1ID))
                    {
                        po3.SupplierCode = q.First<Product>().Supplier1ID;
                        po3.DateCreated = DateTime.Now;
                        po3.Status = "Pending";
                        pomain = po3;
                    }
                    else if (SuppID3[k].Equals(q.First<Product>().Supplier2ID))
                    {
                        po3.SupplierCode = q.First<Product>().Supplier2ID;
                        po3.DateCreated = DateTime.Now;
                        po3.Status = "Pending";
                        pomain2 = po3;
                    }
                    else if (SuppID3[k].Equals(q.First<Product>().Supplier3ID))
                    {
                        po3.SupplierCode = q.First<Product>().Supplier3ID;
                        po3.DateCreated = DateTime.Now;
                        po3.Status = "Pending";
                        pomain3 = po3;
                    }





                    if (SuppID3[k].Equals(q.First<Product>().Supplier1ID) && QNTY3[k] > 0)
                    {
                        PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
                        var q3 = from x in context.Products
                                 where x.ItemNumber == p
                                 select x;
                        pod3.ItemNumber = p;
                        pod3.Description = q3.First<Product>().Description;
                        pod3.Quantity = QNTY3[k];
                        String g = q3.First<Product>().Supplier1ID;
                        var z = from x in context.SupplyDetails
                                where x.SupplierCode == g && x.ItemNumber == p
                                select x;

                        pod3.Price = z.First<SupplyDetail>().Price;
                        lpod.Add(pod3);

                    }
                    else if (SuppID3[k].Equals(q.First<Product>().Supplier2ID) && QNTY3[k] > 0)
                    {
                        PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
                        var q3 = from x in context.Products
                                 where x.ItemNumber == p
                                 select x;
                        pod3.ItemNumber = p;
                        pod3.Description = q3.First<Product>().Description;
                        pod3.Quantity = QNTY3[k];
                        String g = q3.First<Product>().Supplier2ID;
                        var z = from x in context.SupplyDetails
                                where x.SupplierCode == g && x.ItemNumber == p
                                select x;

                        pod3.Price = z.First<SupplyDetail>().Price;
                        lpod2.Add(pod3);


                    }
                    else if (SuppID3[k].Equals(q.First<Product>().Supplier3ID) && QNTY3[k] > 0)
                    {
                        PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
                        var q3 = from x in context.Products
                                 where x.ItemNumber == p
                                 select x;
                        pod3.ItemNumber = p;
                        pod3.Description = q3.First<Product>().Description;
                        pod3.Quantity = QNTY3[k];
                        String g = q3.First<Product>().Supplier3ID;
                        var z = from x in context.SupplyDetails
                                where x.SupplierCode == g && x.ItemNumber == p
                                select x;

                        pod3.Price = z.First<SupplyDetail>().Price;
                        lpod3.Add(pod3);

                    }
                }
                k++;

            }
            if (pomain.SupplierCode != null)
                PurchaseOrderDAO.CreatePurchaseOrder(pomain, lpod);
            if (pomain2.SupplierCode != null)
                PurchaseOrderDAO.CreatePurchaseOrder(pomain2, lpod2);
            if (pomain3.SupplierCode != null)
                PurchaseOrderDAO.CreatePurchaseOrder(pomain3, lpod3);


        }


        public static ArrayList GetPendingPurchaseOrder()
        {
            return PurchaseOrderDAO.GetPendingPurchaseOrders();
        }

        public static List<PurchaseOrderDetail> GetPendingItemsList(int poNumber)
        {
            return PurchaseOrderDAO.GetPendingItemsList(poNumber);
        }
        //public static void GetSupplyOrder(String[] PID, String[] SuppID, int[] QNTY, String[] SuppID2, int[] QNTY2,String[] SuppID3,int[] QNTY3)
        //{
        //     int i = 0; int j = 0;

        //     PurchaseOrder pomain = new PurchaseOrder();

        //    List<PurchaseOrderDetail> lpod = new List<PurchaseOrderDetail>();
            

            
           
        //    foreach (String p in PID)
        //    {
        //        var q = from x in context.Products
        //                where x.ItemNumber == p
        //                select x;
        //        PurchaseOrder po = new PurchaseOrder();
        //        if (SuppID[i].Equals(q.First<Product>().Supplier1ID)||SuppID[i].Equals(q.First<Product>().Supplier2ID)||SuppID[i].Equals(q.First<Product>().Supplier3ID))
        //        {  
        //            if(SuppID[i].Equals(q.First<Product>().Supplier1ID))
        //            {po.SupplierCode = q.First<Product>().Supplier1ID;
        //            po.DateCreated = DateTime.Now;
        //            po.Status = "Pending";
        //            pomain = po;
        //            }
        //            else if(SuppID[i].Equals(q.First<Product>().Supplier2ID))
        //            {po.SupplierCode=q.First<Product>().Supplier2ID;
        //            po.DateCreated = DateTime.Now;
        //            po.Status = "Pending";
        //            pomain = po;
        //            }
        //            else if(SuppID[i].Equals(q.First<Product>().Supplier3ID))
        //            {po.SupplierCode=q.First<Product>().Supplier3ID;
        //            po.DateCreated = DateTime.Now;
        //            po.Status = "Pending";
        //            pomain = po;
        //            }
                    
                    

        //            if (SuppID[i].Equals(q.First<Product>().Supplier1ID))
        //            {
        //                PurchaseOrderDetail pod = new PurchaseOrderDetail();

        //                var q1 = from x in context.Products
        //                        where x.ItemNumber == p
        //                        select x;
        //                pod.ItemNumber = p;
        //                pod.Description = q.First<Product>().Description;
        //                pod.Quantity = QNTY[i];
        //                String g = q1.First<Product>().Supplier1ID;
        //                var z = from x in context.SupplyDetails
        //                        where x.SupplierCode == g && x.ItemNumber == p
        //                        select x;
        //                SupplyDetail s = z.First<SupplyDetail>();

        //                pod.Price =s.Price;
        //                lpod.Add(pod);

        //            }
        //            else if (SuppID[i].Equals(q.First<Product>().Supplier2ID))
        //            {   
        //                PurchaseOrderDetail pod = new PurchaseOrderDetail();

        //                var q1 = from x in context.Products
        //                        where x.ItemNumber == p
        //                        select x;
        //                pod.ItemNumber = p;
        //                pod.Description = q.First<Product>().Description;
        //                pod.Quantity = QNTY[i];
        //                String g = q1.First<Product>().Supplier2ID;
        //                var z = from x in context.SupplyDetails
        //                        where x.SupplierCode == g && x.ItemNumber == p
        //                        select x;

        //                pod.Price = z.First<SupplyDetail>().Price;
        //                lpod.Add(pod);
                        
        //            }
        //            else if (SuppID[i].Equals(q.First<Product>().Supplier3ID))
        //            {
        //                PurchaseOrderDetail pod = new PurchaseOrderDetail();

        //                var q1 = from x in context.Products
        //                        where x.ItemNumber == p
        //                        select x;
        //                pod.ItemNumber = p;
        //                pod.Description = q1.First<Product>().Description;
        //                pod.Quantity = QNTY[i];
        //                String g = q1.First<Product>().Supplier3ID;
        //                var z = from x in context.SupplyDetails
        //                        where x.SupplierCode == g && x.ItemNumber == p
        //                        select x;

        //                pod.Price = z.First<SupplyDetail>().Price;
        //                lpod.Add(pod);
                    
        //            }


        //        }
        //        i++;
                
        //    }
        //    if(pomain.SupplierCode!=null)
        //    PurchaseOrderDAO.CreatePurchaseOrder(pomain, lpod);
        //    List<PurchaseOrderDetail> lpod2 = new List<PurchaseOrderDetail>();
        //    PurchaseOrder pomain2 = new PurchaseOrder();
        //        foreach (String p in PID)
        //        {
        //            PurchaseOrder po2 = new PurchaseOrder();
        //            var q = from x in context.Products
        //                    where x.ItemNumber == p
        //                    select x;
        //            if (SuppID2[j].Equals(q.First<Product>().Supplier1ID) || SuppID2[j].Equals(q.First<Product>().Supplier2ID) || SuppID2[j].Equals(q.First<Product>().Supplier3ID))
        //            {
        //                String t = q.First<Product>().Supplier1ID;
        //                if (SuppID2[j].Equals(q.First<Product>().Supplier1ID))
        //                { po2.SupplierCode = q.First<Product>().Supplier1ID;
        //                po2.DateCreated = DateTime.Now;
        //                po2.Status = "Pending";
        //                    pomain2 = po2;
        //                }
        //                else if (SuppID2[j].Equals(q.First<Product>().Supplier2ID))
        //                { po2.SupplierCode = q.First<Product>().Supplier2ID;
        //                po2.DateCreated = DateTime.Now;
        //                po2.Status = "Pending";
        //                pomain2 = po2;
        //                }
        //                else if (SuppID2[j].Equals(q.First<Product>().Supplier3ID))
        //                { po2.SupplierCode = q.First<Product>().Supplier3ID;
        //                po2.DateCreated = DateTime.Now;
        //                po2.Status = "Pending";
        //                pomain2 = po2;
        //                }

                       


        //                if (SuppID2[j].Equals(q.First<Product>().Supplier1ID) && QNTY2[j] > 0)
        //                {
        //                    PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
        //                    var q2 = from x in context.Products
        //                            where x.ItemNumber == p
        //                            select x;
        //                    pod2.ItemNumber = p;
        //                    pod2.Description = q2.First<Product>().Description;
        //                    pod2.Quantity = QNTY2[j];
        //                    String g = q2.First<Product>().Supplier1ID;
        //                    var z = from x in context.SupplyDetails
        //                            where x.SupplierCode ==g && x.ItemNumber == p
        //                            select x;

        //                    pod2.Price = z.First<SupplyDetail>().Price;
        //                        lpod2.Add(pod2);
                           
        //                }
        //                else if (SuppID2[j].Equals(q.First<Product>().Supplier2ID) && QNTY2[j] > 0)
        //                {
        //                    PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
        //                    var q2 = from x in context.Products
        //                             where x.ItemNumber == p
        //                             select x;
        //                    pod2.ItemNumber = p;
        //                    pod2.Description = q2.First<Product>().Description;
        //                    pod2.Quantity = QNTY2[j];
        //                    String g = q2.First<Product>().Supplier2ID;
        //                    var z = from x in context.SupplyDetails
        //                            where x.SupplierCode == g && x.ItemNumber == p
        //                            select x;

        //                    pod2.Price = z.First<SupplyDetail>().Price;
        //                    lpod2.Add(pod2);

        //                }
        //                else if (SuppID2[j].Equals(q.First<Product>().Supplier3ID) && QNTY2[j] > 0)
        //                {
        //                    PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
        //                    var q2 = from x in context.Products
        //                             where x.ItemNumber == p
        //                             select x;
        //                    pod2.ItemNumber = p;
        //                    pod2.Description = q2.First<Product>().Description;
        //                    pod2.Quantity = QNTY2[j];
        //                    String g = q2.First<Product>().Supplier3ID;
        //                    var z = from x in context.SupplyDetails
        //                            where x.SupplierCode == g && x.ItemNumber == p
        //                            select x;

        //                    pod2.Price = z.First<SupplyDetail>().Price;
        //                    lpod2.Add(pod2);
                            
        //                }
                        
        //                }
        //            j++;
                    
        //    }
        //    if(pomain2.SupplierCode!=null)
        //        PurchaseOrderDAO.CreatePurchaseOrder(pomain, lpod2); 
        //    int k = 0;
        //    List<PurchaseOrderDetail> lpod3 = new List<PurchaseOrderDetail>();
        //    PurchaseOrder pomain3 = new PurchaseOrder();
        //    foreach (String p in PID)
        //    {   
        //        PurchaseOrder po3 = new PurchaseOrder();
        //        var q = from x in context.Products
        //                where x.ItemNumber == p
        //                select x;

        //        if (SuppID3[k].Equals(q.First<Product>().Supplier1ID) || SuppID3[k].Equals(q.First<Product>().Supplier2ID) || SuppID3[k].Equals(q.First<Product>().Supplier3ID))
        //        {
        //            if (SuppID3[k].Equals(q.First<Product>().Supplier1ID))
        //            { po3.SupplierCode = q.First<Product>().Supplier1ID;
        //            po3.DateCreated = DateTime.Now;
        //            po3.Status = "Pending";
        //            pomain3 = po3;
        //            }
        //            else if (SuppID3[k].Equals(q.First<Product>().Supplier2ID))
        //            { po3.SupplierCode = q.First<Product>().Supplier2ID;
        //            po3.DateCreated = DateTime.Now;
        //            po3.Status = "Pending";
        //            pomain3 = po3;
        //            }
        //            else if (SuppID3[k].Equals(q.First<Product>().Supplier3ID))
        //            { po3.SupplierCode = q.First<Product>().Supplier3ID;
        //            po3.DateCreated = DateTime.Now;
        //            po3.Status = "Pending";
        //            pomain3 = po3;
        //            }


                    


        //            if (SuppID3[k].Equals(q.First<Product>().Supplier1ID) && QNTY3[k] > 0 )
        //            {
        //                PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
        //                var q2 = from x in context.Products
        //                        where x.ItemNumber == p
        //                        select x;
        //                pod3.ItemNumber = p;
        //                pod3.Description = q2.First<Product>().Description;
        //                pod3.Quantity = QNTY3[k];
        //                String g = q.First<Product>().Supplier1ID;
        //                var z = from x in context.SupplyDetails
        //                        where x.SupplierCode == g && x.ItemNumber == p
        //                        select x;

        //                pod3.Price = z.First<SupplyDetail>().Price;
        //                lpod3.Add(pod3);

        //            }
        //            else if (SuppID3[k].Equals(q.First<Product>().Supplier2ID) && QNTY3[k] > 0)
        //            {
        //                PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
        //                var q2 = from x in context.Products
        //                         where x.ItemNumber == p
        //                         select x;
        //                pod3.ItemNumber = p;
        //                pod3.Description = q2.First<Product>().Description;
        //                pod3.Quantity = QNTY3[k];
        //                String g = q.First<Product>().Supplier2ID;
        //                var z = from x in context.SupplyDetails
        //                        where x.SupplierCode == g && x.ItemNumber == p
        //                        select x;

        //                pod3.Price = z.First<SupplyDetail>().Price;
        //                lpod3.Add(pod3);

                        
        //            }
        //            else if (SuppID3[k].Equals(q.First<Product>().Supplier3ID) && QNTY3[k] > 0)
        //            {
        //                PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
        //                var q2 = from x in context.Products
        //                         where x.ItemNumber == p
        //                         select x;
        //                pod3.ItemNumber = p;
        //                pod3.Description = q2.First<Product>().Description;
        //                pod3.Quantity = QNTY3[k];
        //                String g = q.First<Product>().Supplier3ID;
        //                var z = from x in context.SupplyDetails
        //                        where x.SupplierCode == g && x.ItemNumber == p
        //                        select x;

        //                pod3.Price = z.First<SupplyDetail>().Price;
        //                lpod3.Add(pod3);

        //            }
        //        }
        //        k++;
                
        //    }
        //    if(pomain3.SupplierCode!=null)
        //    PurchaseOrderDAO.CreatePurchaseOrder(pomain3, lpod3);


        //}
        //public static void GetSupplyOrder(String[] PID, String[] SuppID, int[] QNTY, String[] SuppID2, int[] QNTY2,String[] SuppID3,int[] QNTY3)
        //{
        //    PurchaseOrder po = new PurchaseOrder(); int i = 0; int j = 0;
        


        //    List<PurchaseOrderDetail> lpod = new List<PurchaseOrderDetail>();
           
        //    foreach (String p in PID)
        //    {
        //        if (SuppID[i].Equals("ALPA"))
        //        {
        //            po.SupplierCode = "ALPA";
        //            po.DateCreated = DateTime.Now;
        //            po.Status = "Pending";

        //            if (SuppID[i].Equals("ALPA"))
        //            {
        //                PurchaseOrderDetail pod = new PurchaseOrderDetail();

        //                var q = from x in context.Products
        //                        where x.ItemNumber == p
        //                        select x;
        //                pod.ItemNumber = p;
        //                pod.Description = q.First<Product>().Description;
        //                pod.Quantity = QNTY[i];
        //                var z = from x in context.SupplyDetails
        //                        where x.SupplierCode == "ALPA" && x.ItemNumber == p
        //                        select x;

        //                pod.Price = z.First<SupplyDetail>().Price;
        //                lpod.Add(pod);

        //            }


        //        }
        //        i++;
        //    }
        //    PurchaseOrderDAO.CreatePurchaseOrder(po, lpod);
        //    List<PurchaseOrderDetail> lpod2 = new List<PurchaseOrderDetail>(); 
        //    PurchaseOrder po2 = new PurchaseOrder();
        //        foreach (String p in PID){
                
        //            if (SuppID2[j].Equals("CHEP"))
        //            {
        //                po2.SupplierCode = "CHEP";
        //                po2.DateCreated = DateTime.Now;
        //                po2.Status = "Pending";


        //                if (SuppID2[j].Equals("CHEP") && QNTY2[j]>0)
        //                {
        //                    PurchaseOrderDetail pod2 = new PurchaseOrderDetail();
        //                    var q = from x in context.Products
        //                            where x.ItemNumber == p
        //                            select x;
        //                    pod2.ItemNumber = p;
        //                    pod2.Description = q.First<Product>().Description;
        //                    pod2.Quantity = QNTY2[j];
        //                    var z = from x in context.SupplyDetails
        //                            where x.SupplierCode == "CHEP" && x.ItemNumber == p
        //                            select x;

        //                    pod2.Price = z.First<SupplyDetail>().Price;
        //                        lpod2.Add(pod2);
                           
        //                }
                      
        //            }
        //            j++;
        //    }
        //        PurchaseOrderDAO.CreatePurchaseOrder(po2, lpod2); 
        //    int k = 0;
        //    List<PurchaseOrderDetail> lpod3 = new List<PurchaseOrderDetail>(); 
        //    PurchaseOrder po3 = new PurchaseOrder();
        //    foreach (String p in PID)
        //    {

        //        if (SuppID3[k].Equals("BANE"))
        //        {
        //            po3.SupplierCode = "BANE";
        //            po3.DateCreated = DateTime.Now;
        //            po3.Status = "Pending";


        //            if (SuppID3[k].Equals("BANE") && QNTY3[k] > 0)
        //            {
        //                PurchaseOrderDetail pod3 = new PurchaseOrderDetail();
        //                var q = from x in context.Products
        //                        where x.ItemNumber == p
        //                        select x;
        //                pod3.ItemNumber = p;
        //                pod3.Description = q.First<Product>().Description;
        //                pod3.Quantity = QNTY3[k];
        //                var z = from x in context.SupplyDetails
        //                        where x.SupplierCode == "BANE" && x.ItemNumber == p
        //                        select x;

        //                pod3.Price = z.First<SupplyDetail>().Price;
        //                lpod3.Add(pod3);

        //            }

        //        }
        //        k++;
        //    }
        //    PurchaseOrderDAO.CreatePurchaseOrder(po3, lpod3);


        }
        
        //public static void CreatePurchaseorderDetails()
        //{
        //    try
        //    {
        //        string connetionString = null;
        //        SqlConnection connection;
        //        SqlCommand command;
        //        SqlDataAdapter adapter = new SqlDataAdapter();
                
        //        DataSet ds = new DataSet();
        //        DataSet ds2=new DataSet();
        //        int i = 0;
        //        string sql = null; string sql2 = null;
        //        int yPoint = 0;
              
        //        String SupplierCode = null;
                
        //        connetionString = "Data Source=(local);Initial Catalog=UniversityStore;Integrated Security=SSPI";
        //        sql = "select PONumber,SupplierCode from PurchaseOrder where status like'Pending'";
        //        connection = new SqlConnection(connetionString);
        //        connection.Open();
        //        command = new SqlCommand(sql, connection);
        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds);
        //        connection.Close();
        //        int k = 0;
        //        int t=20,y=20;
               
              
        //         for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        //        {

        //            SupplierCode = ds.Tables[0].Rows[i].ItemArray[1].ToString();
        //            if (SupplierCode == "BANE")
        //            {
        //                SqlDataAdapter adapter2 = new SqlDataAdapter();
        //                String bane = ds.Tables[0].Rows[i].ItemArray[0].ToString();
        //                sql2 = "select ItemNumber,Description,Quantity,Price from PurchaseOrderDetails where PONumber like '"+bane+"';";
        //                SqlConnection connection2 = new SqlConnection(connetionString);
        //                SqlCommand command2=new SqlCommand(sql2,connection2);
        //                connection2.Open();
        //                adapter2.SelectCommand = command2;
        //                adapter2.Fill(ds2);
        //                PdfDocument pdf = new PdfDocument();
        //                pdf.Info.Title = "Database to PDF";
        //                PdfPage pdfPage = pdf.AddPage();
        //                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
        //                XFont font = new XFont("Verdana", 10, XFontStyle.Regular);

        //                yPoint = yPoint + 100+k;
        //                graph.DrawString(SupplierCode, font, XBrushes.Black, new XRect(100, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
        //                for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
        //                {
        //                    String m = ds2.Tables[0].Rows[j].ItemArray[1].ToString(); String n = ds2.Tables[0].Rows[j].ItemArray[2].ToString();
        //                    graph.DrawString(m, font, XBrushes.Black, new XRect(t, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
        //                    graph.DrawString("-------------------------------------", font, XBrushes.Black, new XRect(t+100, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
        //                    graph.DrawString(n, font, XBrushes.Black, new XRect(t+400, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft); y = y + 30;
        //                }
        //                string pdfFilename = "BANE.pdf";
                      
        //                pdf.Save(pdfFilename);
        //            }
        //            else if (SupplierCode == "CHEP")
        //            {
        //                PdfDocument pdf = new PdfDocument();
        //                pdf.Info.Title = "Database to PDF";
        //                PdfPage pdfPage = pdf.AddPage();
        //                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
        //                XFont font = new XFont("Verdana", 10, XFontStyle.Regular);

        //                yPoint = yPoint + 100;
        //                graph.DrawString(SupplierCode, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
        //              //  graph.DrawString(SupplierCode, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
        //                string pdfFilename = "CHEP.pdf";
        //                pdf.Save(pdfFilename);
                        
        //            }
        //            else if (SupplierCode == "ALPA")
        //            {
        //                PdfDocument pdf = new PdfDocument();
        //                pdf.Info.Title = "Database to PDF";
        //                PdfPage pdfPage = pdf.AddPage();
        //                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
        //                XFont font = new XFont("Verdana", 10, XFontStyle.Regular);

        //                yPoint = yPoint + 100;
        //                graph.DrawString(SupplierCode, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
        //               // graph.DrawString(SupplierCode, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
        //                string pdfFilename = "ALPA.pdf";
        //                pdf.Save(pdfFilename);
                        
        //            }
                  
        //        }


               


        //    }
        //    catch
        //    {

        //    }
        //}

        }

       
    

