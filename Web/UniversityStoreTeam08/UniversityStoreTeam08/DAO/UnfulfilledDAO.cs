using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class UnfulfilledDAO
    {
        //static string STATUS_OPEN = "open";
        static string STAUS_CLOSED = "closed";
        public static string STATUS_PENDING = "pending";
        public static string STATUS_CLOSED = "closed";



        public static void CreateUnfulfilled(Unfulfilled unful)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            context.AddToUnfulfilleds(unful);
            context.SaveChanges();

        }
        /**** method added by sreeja ***/

        public static List<Object> GetUnfulfilledInfo(String deptHeadNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q1 = from x in context.Employees
                     where x.EmployeeNumber == deptHeadNumber
                     select x;

            string deptcode = q1.First<Employee>().DepartmentCode;
           

            List<Unfulfilled> unfulfilled = new List<Unfulfilled>();
            var q2 = from x in context.Unfulfilleds
                    where x.DepartmentCode == deptcode && x.Status==STATUS_PENDING
                    select new { x.Product.Category,
                    x.Product.Description,
                    x.UnfulfilledQuantity,
                    x.Product.UnitOfMeasure,
                    x.Status,
                    x.DateCreated
                    };
            return q2.ToList<Object>();

        }



        public static void GetUnfulfilledList(String dept)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<Unfulfilled> unfulfilled = new List<Unfulfilled>();
            var q = from x in context.Unfulfilleds
                    where x.DepartmentCode == dept
                    select x;
            unfulfilled = q.ToList<Unfulfilled>();

        }


        public static List<string> getAllUnfullfilledDepartments()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<string> depList = new List<string>();
            var q = from x in context.Unfulfilleds
                    where x.Status == STATUS_PENDING
                    group x by x.DepartmentCode into g
                    select new
                    {
                        depCode = g.Key
                    };

            List<Object> olist = q.ToList<Object>();
            foreach (Object o in olist)
            {
                //Console.WriteLine(o.ToString().Substring(12, 4));
                depList.Add(o.ToString().Substring(12, 4));
            }

            foreach (string s in depList)
            {
                Console.WriteLine(s);
            }
            return depList;

        }

        public static List<Object> getAllUnfuffilledOfDep(string depCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.Unfulfilleds

                    where x.Status == STATUS_PENDING && x.DepartmentCode == depCode
                    orderby x.DateCreated descending
                    group x by x.ItemNumber into g
                    select new
                    {

                        itemNumber = g.Key,
                        totalQty = g.Sum(x => x.UnfulfilledQuantity)
                    };

            List<Object> olist = q.ToList<Object>();
            foreach (Object o in olist)
            {
                Console.WriteLine(o.ToString());
            }
            return olist;
        }

        public static void changeOpenToClosed()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.Unfulfilleds
                    where x.Status == STATUS_PENDING
                    select x;
            List<Unfulfilled> rl = q.ToList<Unfulfilled>();
            if (rl.Count() != 0)
            {
                foreach (Unfulfilled r in rl)
                {
                    r.Status = STAUS_CLOSED;

                }
                context.SaveChanges();
            }
        }

        public static DateTime getTheFirstDatetDate(string depCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<DateTime> dlist = new List<DateTime>();
            var q = from x in context.Unfulfilleds
                    where x.DepartmentCode == depCode && x.Status == STATUS_PENDING
                    select x;
            List<Unfulfilled> rlist = q.ToList<Unfulfilled>();
            foreach (Unfulfilled rl in rlist)
            {
                dlist.Add((DateTime)rl.DateCreated);
            }

            dlist.Sort();
            if (dlist.Count() != 0)
            {
                return dlist[0];
            }

            else
            {
                DateTime d = Convert.ToDateTime("2000-01-01");
                return d;
            }
        }

        public static void raiseAdjustmentFromCollection(string itemNum,string depCode,int qtyDeff)
        {
            Unfulfilled uf = new Unfulfilled();
            uf.ItemNumber =itemNum;
            uf.DepartmentCode = depCode;
            uf.UnfulfilledQuantity = qtyDeff;
            uf.Status = UnfulfilledDAO.STATUS_PENDING;
            uf.DateCreated = DateTime.Now;
            UnfulfilledDAO.CreateUnfulfilled(uf);
        }
        


    }
}
