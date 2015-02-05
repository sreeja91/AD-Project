using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class ProductEFFacade
    {
     private static UniversityStoreEntities us = new UniversityStoreEntities();

        public static List<Product> selectAllProductList()
        {
            
            var q = from x in us.Products
                    select x;
            List<Product> p = q.ToList<Product>();
            return p;


        }
        //////****************Ashwin********************///////

        public static Product getSearchProductbyid(String id)
        {
            Product c = us.Products.First<Product>(x => x.ItemNumber == id);
            return c;
        }

        public static List<Product> getSearchProductbyname(String name)
        {
            
            var q = from x in us.Products
                    where x.Description.Contains(name)
                    select x;
            List<Product> p = q.ToList<Product>();
            return p;

        }
        public static List<Product> getSearchProductbyCategory(String cat)
        {

            var q = from x in us.Products
                    where x.Category.Contains(cat)
                    select x;
            List<Product> p = q.ToList<Product>();
            return p;

        }
        public static List<Product> getProductFromListOfID(String[] id)
        {
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
            

            return n;
        }

        public static void UpdateStock(String Pid, int qnt)
        {
            Stock s=us.Stocks.First<Stock>(x => x.ItemNumber == Pid);
            s.TotalInventoryBalance += qnt;

        }
        public static void UpdateStockReduce(String Pid, int qnt)
        {
            Stock s = us.Stocks.First<Stock>(x => x.ItemNumber == Pid);
            s.TotalInventoryBalance -= qnt;

        }

    }
}
//////****************Ashwin********************///////
