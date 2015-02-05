using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class SupplierDAO
    {

        public static List<Supplier> getSuppliers()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();

            var Q = from z in context.Suppliers
                    select z;
            List<Supplier> s = Q.ToList<Supplier>();
            return s;

        }
        //////****************Ashwin********************///////

        public static int getPrice(string supid, string pid)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();

            var a = from x in context.SupplyDetails
                    where x.SupplierCode == supid && x.ItemNumber==pid
                    select x;
            int s =Convert.ToInt32 (a.First<SupplyDetail>().Price);
            return s;        
        }

        /// <summary>
        /// CHANGE FOR RAISE PUCHASE ORDER
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> GetallSupplier(String ProductID)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();

            List<Supplier> sup = new List<Supplier>();
            
            var q = from x in context.Products
                    where x.ItemNumber==ProductID
                    select x;
            String[] supp = { q.First<Product>().Supplier1ID, q.First<Product>().Supplier2ID, q.First<Product>().Supplier3ID };


            foreach (String s in supp)
            {
                var z = from x in context.Suppliers
                        where x.SupplierCode == s
                        select x;
                sup.Add(z.First<Supplier>());

            }
            
            return sup;

        }
    }
}
      //////****************Ashwin********************///////
