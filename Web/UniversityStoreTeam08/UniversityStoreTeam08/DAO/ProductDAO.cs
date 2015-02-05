using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class ProductDAO
    {
        private static string PRODUCT_ADJUSTMENT_STATUS_REDUCE = "reduce";
        private static string PRODUCT_ADJUSTMENT_STATUS_INCREASE = "increase";

        public static List<Product> selectAllProductList()
        {


            UniversityStoreEntities us = new UniversityStoreEntities();
            var q = from x in us.Products
                    select x;
            List<Product> p = q.ToList<Product>();
            return p;


        }

        public static List<Object> populateAllProduct()
        {
            UniversityStoreEntities us = new UniversityStoreEntities();
            var data = from x in us.Products
                       select new
                           {
                               x.ItemNumber,
                               x.Description,
                               x.UnitOfMeasure,
                               x.Category
                           };
            return data.ToList<Object>();
        }
        public static Product getSearchProductbyid(String id)
        {
            UniversityStoreEntities us = new UniversityStoreEntities();
            Product c = us.Products.First<Product>(x => x.ItemNumber == id);
            return c;
        }

        public static List<Product> getSearchProductbyname(String name)
        {
            UniversityStoreEntities us = new UniversityStoreEntities();
            
            var q = from x in us.Products
                    where x.Description.Contains(name)
                    select x;
            List<Product> p = q.ToList<Product>();
            return p;

        }

        public static List<Object> getSearchResultbyname(String name)
        {
            UniversityStoreEntities us = new UniversityStoreEntities();

            var q = from x in us.Products
                    where x.Description.Contains(name)
                    select new { x.ItemNumber, x.Description, x.UnitOfMeasure, x.Category };

            return q.ToList<Object>();


        }

        public static List<Product> getSearchProductbyCategory(String cat)
        {
            UniversityStoreEntities us = new UniversityStoreEntities();

            var q = from x in us.Products
                    where x.Category.Contains(cat)
                    select x;
            List<Product> p = q.ToList<Product>();
            return p;

        }
        public static List<Product> getProductFromListOfID(String[] id)
        {
            UniversityStoreEntities us = new UniversityStoreEntities();
            List<Product> pa = new List<Product>();
            foreach (String g in id)
            {
                Product c = us.Products.First<Product>(x => x.ItemNumber == g);
                pa.Add(c);
            }

            return pa;

        }

        public static List<Stock> getStockFromListOfID(String[] id)
        {
            UniversityStoreEntities us = new UniversityStoreEntities();
            List<Stock> s = new List<Stock>();
            foreach(String g in id)
            {
                Stock c = us.Stocks.First<Stock>(x => x.ItemNumber == g);
                s.Add(c);
            }
            return s;

        }
        public static List<Product> StockIsLow()
        {
            UniversityStoreEntities us = new UniversityStoreEntities();
            var q = from x in us.Products
                    select x;
            List<Product> p = q.ToList<Product>();
            List<Product> n = new List<Product>();
            foreach (Product a in p)
            {
                Stock s = us.Stocks.First<Stock>(x => x.ItemNumber == a.ItemNumber);
                if (s.TotalInventoryBalance <= a.ReorderLevel)
                {
                    n.Add(a);
                }
            }
          //  foreach (Product s in n)
           // {
             //   Console.WriteLine(s.Description);
            //}

            return n;
        }

        public static void UpdateStock(String Pid, int qnt)
        {
            //UniversityStoreEntities us = new UniversityStoreEntities();
            UniversityStoreEntities us = new UniversityStoreEntities();
            Stock s=us.Stocks.First<Stock>(x => x.ItemNumber == Pid);
            s.TotalInventoryBalance += qnt;
            us.SaveChanges();

        }
        public static void UpdateStockReduce(String Pid, int qnt)
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            Stock s = us.Stocks.First<Stock>(x => x.ItemNumber == Pid);
            s.TotalInventoryBalance -= qnt;
            us.SaveChanges();
        }



        //////****************Wu********************///////

        public static List<Object> selectAll()
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            var q = from x in us.Products
                    select new
                    {
                        x.ItemNumber,
                        x.Category,
                        x.Description,
                        x.ReorderLevel,
                        x.ReorderQuantity,
                        x.AdjustmentVoucherPrice,
                        x.Stock.TotalInventoryBalance,
                        x.UnitOfMeasure
                    };
            return q.ToList<Object>();

        }


        public static List<object> populateStock(string cat)
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            var a = from x in us.Products
                    where x.Category.Contains(cat)

                    select new
                    {
                        x.ItemNumber,
                        x.Category,
                        x.Description,
                        x.ReorderLevel,
                        x.ReorderQuantity,
                        x.AdjustmentVoucherPrice,
                        x.Stock.TotalInventoryBalance,
                        x.UnitOfMeasure
                    };

            return a.ToList<object>();

        }

        //////****************Wu********************///////


        //////****************Sreeja********************///////
        public static List<object> populateStockByDescp(string descp)
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            var a = from x in us.Products
                    where x.Description.Contains(descp)

                    select new
                    {
                        x.ItemNumber,
                        x.Category,
                        x.Description,
                        x.ReorderLevel,
                        x.ReorderQuantity,
                        x.AdjustmentVoucherPrice,
                        x.Stock.TotalInventoryBalance,
                        x.UnitOfMeasure
                    };

            return a.ToList<object>();

        }

        public static List<Product> ProductWithLowStock()
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            List<Product> pr = ProductDAO.StockIsLow();


            //  foreach(Object ot in ob)
            // {Console.WriteLine(ob.ToString());}
            return pr;


        }


        public static List<int> getTotalInventoryBalance()
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            List<int> iList = new List<int>();
            List<Product> prod = ProductWithLowStock();
            foreach (Product p in prod)
            {
                var q = from x in us.Products
                        where x.ItemNumber == p.ItemNumber
                        select x;

                string itemno = q.First<Product>().ItemNumber;

                var q1 = from x in us.Stocks
                         where x.ItemNumber == itemno
                         select x;

                int inventbalance = Convert.ToInt32(q1.First<Stock>().TotalInventoryBalance);
                iList.Add(inventbalance);
            }
            return iList;

        }


        public static List<Object> selectAllByGroupByCategory()
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            var q = from x in us.Products
                    group x by x.Category into g
                    select new
                    {
                        Category = g.Key
                    };
                    
            return q.ToList<Object>();

        }
        //////****************Sreeja********************///////


        //////****************Nitin********************///////
        public static List<Product> getProductFromListOfID2(List<string> id)
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            List<Product> pa = new List<Product>();
            foreach (string g in id)
            {
                Product c = us.Products.First<Product>(x => x.ItemNumber == g);
                pa.Add(c);
            }

            return pa;

        }


        public static Product gethProductbyName(String pname)
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            Product c = us.Products.First<Product>(x => x.Description.Contains(pname));
            return c;
        }


        //public static List<Object> populateAllProduct()
        //{
        //    var data = from x in us.Products
        //               select new
        //               {
        //                   x.ItemNumber,
        //                   x.Description,
        //                   x.Category
        //               };
        //    return data.ToList<Object>();
        //}

        //////****************Nitin********************///////


        ////////////*********Tiger**********////////////
        public static Product getProductForCart(string itemNumber)
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            var q = from prd in us.Products
                    from cart in us.RequestStationaryCarts
                    where prd.ItemNumber == cart.ItemCode
                    && cart.ItemCode==itemNumber
                    select prd;

            return q.First<Product>();
        }



        //////////////*************Ashwin***************//////////////

        public static void UpdateStockfromadjustment(String PID, String stat, int qnt)
        {

            UniversityStoreEntities us = new UniversityStoreEntities();
            Stock s = us.Stocks.First<Stock>(x => x.ItemNumber == PID);
            if (stat == PRODUCT_ADJUSTMENT_STATUS_REDUCE)
                s.TotalInventoryBalance -= qnt;
            else if (stat == PRODUCT_ADJUSTMENT_STATUS_INCREASE)
                s.TotalInventoryBalance += qnt;
            us.SaveChanges();
        }


        //////////////**************Ashwin***************////////////


    }



}
