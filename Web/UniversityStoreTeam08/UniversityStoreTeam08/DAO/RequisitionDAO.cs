using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;


namespace UniversityStoreTeam08
{
    class RequisitionDAO

    {
        static string STATUS_PENDING = "pending";
        static string STATUS_APPROVED = "approved";
        static string STATUS_REJECTED = "rejected";
        static string STATUS_CLOSED = "closed";



        public static RequisitionList addNewReqList(string empNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            DateTime d = DateTime.Now;
            Employee emp = context.Employees.First<Employee>(x => x.EmployeeNumber == empNumber);
            RequisitionList rl = new RequisitionList();
            rl.DepartmentCode = emp.DepartmentCode;
            rl.EmployeeNumber = emp.EmployeeNumber;
            rl.DateCreated = DateTime.Now;
            rl.Status = STATUS_PENDING;

            context.AddToRequisitionLists(rl);
            context.SaveChanges();

            return rl;

        }

        public static void createNewRequisitionList(Dictionary<string, List<RequisitionDetail>> dictionary)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            foreach (KeyValuePair<string, List<RequisitionDetail>> kvp in dictionary)
            {


                DateTime d = DateTime.Now;
                Employee emp = context.Employees.First<Employee>(x => x.EmployeeNumber == kvp.Key);
                RequisitionList rl = new RequisitionList();
                rl.DepartmentCode = emp.DepartmentCode;
                rl.EmployeeNumber = emp.EmployeeNumber;
                rl.DateCreated = DateTime.Now;
                rl.Status = STATUS_PENDING;

                EntityCollection<RequisitionDetail> listDetails = new EntityCollection<RequisitionDetail>();

                foreach (RequisitionDetail detail in kvp.Value)
                {
                    listDetails.Add(detail);
                }
                rl.RequisitionDetails = listDetails;
                context.AddToRequisitionLists(rl);
                context.SaveChanges();



            }
        }

        public static void createRequisitionDetails(string empNum)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<RequisitionDetail> rd = new List<RequisitionDetail>();
            var q = from x in context.RequestStationaryCarts where x.EmployeeNumber == empNum select x;

            List<RequestStationaryCart> empCart = q.ToList<RequestStationaryCart>();


            for (int i = 0; i < empCart.Count(); i++)
            {

                RequisitionDetail rd1 = new RequisitionDetail();
                rd1.ItemNmber = empCart[i].ItemCode;
                rd1.Quantity = empCart[i].Quantity;
                rd.Add(rd1);

            }

            Dictionary<string, List<RequisitionDetail>> rDetails = new Dictionary<string, List<RequisitionDetail>>();

            rDetails.Add(empNum, rd);
            createNewRequisitionList(rDetails);



        }


        public static List<RequisitionList> getAllPendingReqList()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.RequisitionLists
                        where x.Status == STATUS_PENDING
                        select x;
            return query.ToList<RequisitionList>();
        }

        public static List<Object> getAllPendingReqData(string depCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.RequisitionLists
                        where x.Status == STATUS_PENDING && x.DepartmentCode == depCode
                        select new {x.EmployeeNumber, x.Employee.Name, x.DateCreated, x.Status, x.RequisitionListNumber, x.RequisitionDetails.FirstOrDefault().Product.Description };
            return query.ToList<Object>();


        }


        public static List<Object> getDetialsOfList(string reqListNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            System.Int32 rNum = Convert.ToInt32(reqListNumber);
            var query = from x in context.RequisitionDetails
                        from y in context.Products
                        where x.RequisitionListNumber == rNum && y.ItemNumber == x.ItemNmber
                        select new
                        {
                            x.ItemNmber,
                            y.Description,
                            x.Quantity,
                            x.Product.UnitOfMeasure

                        };
            return query.ToList<Object>();


        }

        public static void changeReqListStatusApproved(string rlistNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            System.Int32 rln = Convert.ToInt32(rlistNumber);
            var query = from x in context.RequisitionLists
                        where x.RequisitionListNumber == rln
                        select x;
            RequisitionList rl = query.First<RequisitionList>();
            rl.Status = STATUS_APPROVED;
            rl.Comment = "approved";
            context.SaveChanges();
        }



        public static void changeReqListStatusClosed(string rlistNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            System.Int32 rln = Convert.ToInt32(rlistNumber);
            var query = from x in context.RequisitionLists
                        where x.RequisitionListNumber == rln
                        select x;
            RequisitionList rl = query.First<RequisitionList>();
            rl.Status = STATUS_CLOSED;
            context.SaveChanges();

        }


        public static void changeReqListStatusRejected(string rlistNumber, string comment)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            System.Int32 rln = Convert.ToInt32(rlistNumber);
            var query = from x in context.RequisitionLists
                        where x.RequisitionListNumber == rln
                        select x;
            RequisitionList rl = query.First<RequisitionList>();
            rl.Status = STATUS_REJECTED;
            rl.Comment = comment;
            context.SaveChanges();

        }


        /*******
     * method added my nitin
     * ******/



        public static List<RequisitionList> getAllPendingReqDataW(string depCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.RequisitionLists
                        where x.Status == STATUS_PENDING && x.DepartmentCode == depCode
                        select x;
            return query.ToList<RequisitionList>();

        }

        public static List<RequisitionDetail> getDetialsOfListW(string reqListNumber) //// for web
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            System.Int32 rNum = Convert.ToInt32(reqListNumber);
            var query = from x in context.RequisitionDetails
                        from y in context.Products
                        where x.RequisitionListNumber == rNum && y.ItemNumber == x.ItemNmber
                        select x;
            return query.ToList<RequisitionDetail>();


        }

        public static List<Object> getAllApprovedDetails(string depCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.RequisitionDetails
                    join y in context.RequisitionLists on x.RequisitionListNumber equals y.RequisitionListNumber
                    where y.Status == STATUS_APPROVED && y.DepartmentCode == depCode
                    orderby y.DateCreated descending
                    group x by x.ItemNmber into g
                    select new
                    {

                        itemNumber = g.Key,
                        totalQty = g.Sum(x => x.Quantity)
                    };

            List<Object> olist = q.ToList<Object>();
            return olist;

        }

        public static List<Object> getAllDepInvolved()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.RequisitionLists
                    where x.Status == STATUS_APPROVED
                    group x by x.DepartmentCode into g
                    select new
                    {
                        depCode = g.Key
                    };
            return q.ToList<Object>();

        }

        public static void changeApprovedToClosed()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.RequisitionLists
                    where x.Status == STATUS_APPROVED
                    select x;
            List<RequisitionList> rl = q.ToList<RequisitionList>();
            if (rl.Count() != 0)
            {
                foreach (RequisitionList r in rl)
                {
                    r.Status = STATUS_CLOSED;

                }
                context.SaveChanges();
            }
        }

        public static DateTime getTheFirstDatetDate(string depCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<DateTime> dlist = new List<DateTime>();
            var q = from x in context.RequisitionLists
                    where x.DepartmentCode == depCode && x.Status == STATUS_APPROVED
                    select x;
            List<RequisitionList> rlist = q.ToList<RequisitionList>();
            foreach (RequisitionList rl in rlist)
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


        public static List<Object> getAllApprovedOfDep(string depCode)
        {

            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.RequisitionDetails

                    where x.RequisitionList.Status == STATUS_APPROVED && x.RequisitionList.DepartmentCode == depCode
                    orderby x.RequisitionList.DateCreated descending
                    group x by x.ItemNmber into g
                    select new
                    {

                        itemNumber = g.Key,
                        totalQty = g.Sum(x => x.Quantity)
                    };

            List<Object> olist = q.ToList<Object>();
            foreach (Object o in olist)
            {
                Console.WriteLine(o.ToString());
            }
            return olist;

        }

        public static List<string> getAllReqDepartments()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<string> depList = new List<string>();
            var q = from x in context.RequisitionLists
                    where x.Status == STATUS_APPROVED
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



        //////****************Wu********************///////

        public static List<Object> getAllPendingReqList(string depName)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.RequisitionLists
                        where x.Status == STATUS_PENDING && x.Department.DepatmentName == depName
                        select x;
            int reqNumber = query.First<RequisitionList>().RequisitionListNumber;
            var q = from x in context.RequisitionDetails
                    where x.RequisitionListNumber == reqNumber
                    select new { x.Quantity, x.ItemNmber, x.Product.Description };

            //RequisitionList test = (RequisitionList) query;
            //EntityCollection<RequisitionDetail> details =  test.RequisitionDetails;

            //var q = from y in details select new { y.Quantity,
            //                                       y.ItemNmber,
            //                                       y.ItemNmber.Description
            //                                     };

            return q.ToList<Object>();
        }

        public static string getApprovedBy(string dept)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.RequisitionLists
                        where x.DepartmentCode == dept
                        select x;
            string s = query.First<RequisitionList>().ApprovedByHeadID;

            //return s;
            var q = from x in context.RequisitionLists
                    where x.ApprovedByHeadID == s
                    select x;
            string o = q.First<RequisitionList>().Employee.Name;

            return o;

        }

        //////****************Wu********************///////

        public static Employee getEmp(string reqId)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            int y = Convert.ToInt32(reqId);
            var query = from x in context.RequisitionLists
                        where x.RequisitionListNumber == y
                        select x;
            Employee e = query.First<RequisitionList>().Employee;
            return e;
        }

    }
}
