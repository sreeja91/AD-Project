using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace UniversityStoreTeam08
{
    class ConsolidatedRequisitionListEFFacade
    {
       // private static UniversityStoreEntities context = new UniversityStoreEntities();

        private static string STATUS_OPEN = "open";
        private static string STATUS_WAITING = "awaiting";
        private static string STATUS_CLOSED = "closed";

        public static void createNewConsolidatedRequisitionList(Dictionary<string, List<ConsolidatedRequisitionListDetail>> dictionary)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            foreach (KeyValuePair<string, List<ConsolidatedRequisitionListDetail>> kvp in dictionary)
            {
                ConsolidatedRequisitionList consolidatedList = new ConsolidatedRequisitionList();

                consolidatedList.DepartmentCode = kvp.Key;
                consolidatedList.Status = STATUS_OPEN;
                EntityCollection<ConsolidatedRequisitionListDetail> listDetails = new EntityCollection<ConsolidatedRequisitionListDetail>();


                List<ConsolidatedRequisitionListDetail> detailsList = kvp.Value;
                Console.WriteLine(detailsList.Count);

                foreach (ConsolidatedRequisitionListDetail detail in detailsList)
                {
                    Console.WriteLine(detail.ItemNumber);
                    Console.WriteLine("asdfasdf" + detail.QuantityRequested);
                    listDetails.Add(detail);
                }

                consolidatedList.ConsolidatedRequisitionListDetails = listDetails;

                context.AddToConsolidatedRequisitionLists(consolidatedList);
                context.SaveChanges();
            }
        }

        public static List<ConsolidatedRequisitionList> getAllOpenStatus()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.ConsolidatedRequisitionLists
                        where x.Status == STATUS_OPEN
                        select x;

            return query.ToList<ConsolidatedRequisitionList>();
        }

        public static List<ConsolidatedRequisitionList> getAllAwaitingStatus()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.ConsolidatedRequisitionLists
                        where x.Status == STATUS_WAITING
                        select x;

            return query.ToList<ConsolidatedRequisitionList>();
        }

        public static void setListStatusToAwaiting(int consolidatedListId)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            ConsolidatedRequisitionList consolidatedList = context.ConsolidatedRequisitionLists.Where(x => x.ConsolidatedListID == consolidatedListId).First();
            consolidatedList.Status = STATUS_WAITING;
            context.SaveChanges();
        }

        public static void setActualQuantityForItem(int consolidatedListId, string itemCode, int quantity)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.ConsolidatedRequisitionListDetails
                        where x.ConsolidatedListID == consolidatedListId && x.ItemNumber == itemCode
                        select x;
            ConsolidatedRequisitionListDetail detailItem = query.First();
            detailItem.ActualQuantity = quantity;
            context.SaveChanges();
        }

        

        /*******************
         * Add New Methods YZ
         * ****************/

        public static List<ConsolidatedRequisitionList> getAllAwaitingForDept(string deptCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.ConsolidatedRequisitionLists
                        where x.DepartmentCode == deptCode && x.Status == STATUS_WAITING
                        select x;

            List<ConsolidatedRequisitionList> list = query.ToList<ConsolidatedRequisitionList>();

            return list;
        }

        public static ConsolidatedRequisitionListDetail getItemWithMatchingListIDAndItemCode(int listID, string itemCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.ConsolidatedRequisitionListDetails
                        where x.ConsolidatedListID == listID && x.ItemNumber == itemCode
                        select x;

            return query.First<ConsolidatedRequisitionListDetail>();
        }

        public static Department getListForConsolidatedItem(ConsolidatedRequisitionListDetail item)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.ConsolidatedRequisitionLists
                        from y in context.ConsolidatedRequisitionListDetails
                        where x.ConsolidatedListID == y.ConsolidatedListID
                        select x;

            ConsolidatedRequisitionList list = query.First();
            Department dept = list.Department;

            return dept;
        }


        /**********
         * added New method by Nitin
         * *********/

        public static void finisheCollection()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.ConsolidatedRequisitionLists
                    where x.Status == STATUS_OPEN
                    select x;
            List<ConsolidatedRequisitionList> conlist = q.ToList<ConsolidatedRequisitionList>();
            foreach (ConsolidatedRequisitionList c in conlist)
            {
                c.Status = STATUS_WAITING;
                context.SaveChanges();
            }
        }

        public static void updateStationaryCollection(int conListID, string itemNum, int actualQty)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.ConsolidatedRequisitionListDetails
                    where x.ConsolidatedListID == conListID && x.ItemNumber == itemNum
                    select x;
            ConsolidatedRequisitionListDetail c = q.First<ConsolidatedRequisitionListDetail>();
            c.ActualQuantity = actualQty;
            context.SaveChanges();

 
        }

        public static int getDepOpenConsolidatedIDfromProd(string itemNum,string depCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.ConsolidatedRequisitionListDetails
                    where x.ConsolidatedRequisitionList.DepartmentCode==depCode && x.ConsolidatedRequisitionList.Status==STATUS_OPEN && x.ItemNumber==itemNum
                    select x;
            return q.First<ConsolidatedRequisitionListDetail>().ConsolidatedListID;
        }

        public static List<ConsolidatedRequisitionList> getAllDepInvolved()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.ConsolidatedRequisitionLists
                    where x.Status == STATUS_OPEN
                    select x;
            return q.ToList<ConsolidatedRequisitionList>();

        }

        public static List<string> getAllItemsOfList(int consolidatedListId)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.ConsolidatedRequisitionListDetails
                    where x.ConsolidatedListID == consolidatedListId
                    select x;
            List<string> itemList = new List<string>();
            List<ConsolidatedRequisitionListDetail> cl = q.ToList<ConsolidatedRequisitionListDetail>();
            foreach (ConsolidatedRequisitionListDetail c in cl)
            {
                itemList.Add(c.ItemNumber);
            }

            return itemList;
     
        }

        public static void addDetailToList(int listID,string itemCode,int qtyReq,DateTime dateCreated,int actualQty)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            ConsolidatedRequisitionListDetail c2 = new ConsolidatedRequisitionListDetail();
            c2.ConsolidatedListID = listID;
            c2.ItemNumber = itemCode;
            c2.QuantityRequested = qtyReq;
            c2.DateRequest = dateCreated;
            c2.ActualQuantity = 0;

            context.AddToConsolidatedRequisitionListDetails(c2);
            context.SaveChanges();

 
        }

        public static ConsolidatedRequisitionList getOpenListForDept(string deptCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.ConsolidatedRequisitionLists
                        where x.DepartmentCode == deptCode && x.Status == STATUS_OPEN
                        select x;

            List<ConsolidatedRequisitionList> list = query.ToList<ConsolidatedRequisitionList>();
            ConsolidatedRequisitionList cl = query.First<ConsolidatedRequisitionList>();
            return cl;

        }


        public static void addRequestedQtyForItem(int consolidatedListId, string itemCode, int qty)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.ConsolidatedRequisitionListDetails
                        where x.ConsolidatedListID == consolidatedListId && x.ItemNumber == itemCode
                        select x;
            ConsolidatedRequisitionListDetail detailItem = query.First();
            int k = Convert.ToInt32(detailItem.QuantityRequested);
            detailItem.QuantityRequested = qty+k;
            context.SaveChanges();

        }


        public static void createConsolidatedRequisitionDetails(string empNum)
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

           //createNewRequisitionList(rDetails); 

        }

        public static List<Product> getAllProductsToCollect()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<string> pNumberList= new List<string>();
            var q = from x in context.ConsolidatedRequisitionListDetails
                    join y in context.ConsolidatedRequisitionLists on x.ConsolidatedListID equals y.ConsolidatedListID
                    where y.Status == STATUS_OPEN
                    orderby x.DateRequest descending
                    group x by x.ItemNumber into g
                    select new
                    {

                        itemNumber = g.Key,
                        totalQty = g.Sum(x => x.QuantityRequested)
                    };

            List<Object> olist = q.ToList<Object>();
            if (olist.Count() != 0)
            {
                for (int i = 0; i < olist.Count(); i++ )
                {
                    pNumberList.Add(olist[i].ToString().Substring(15,4));

                    //Console.WriteLine(o.ToString().Substring(15, 4));
                   // Console.WriteLine(o.ToString().Substring(32, 3).Trim());

                }

 
            }
           

           return ProductDAO.getProductFromListOfID2(pNumberList);
         
 
        }

        

        public static int getNeededQty(string productNum)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();


            var q = from x in context.ConsolidatedRequisitionListDetails
                    join y in context.ConsolidatedRequisitionLists on x.ConsolidatedListID equals y.ConsolidatedListID
                    where y.Status == STATUS_OPEN && x.ItemNumber == productNum
                    orderby x.DateRequest descending
                    group x by x.ItemNumber into g
                    select new
                    {

                        itemNumber = g.Key,
                        totalQty = g.Sum(x => x.QuantityRequested)

                    };
            int oo=0;
            foreach (var v in q)
            {
                oo =Convert.ToInt32(v.totalQty);

            }

            return oo;
        }

        public static List<string> getAllDepInvolved(string itemNum)
        {
            //var q = from x in context.ConsolidatedRequisitionListDetails
            //        join y in context.ConsolidatedRequisitionLists on x.ConsolidatedListID equals y.ConsolidatedListID
            //        where y.Status == STATUS_OPEN && x.ItemNumber==itemNum
            //        orderby x.DateRequest descending
            //        group x by y.DepartmentCode into g
            //        select new
            //        {
            //            depCode = g.Key
            //        };

            //List<string> deplist = new List<string>();
            ////string[] u = new string;
            //List<object> il= q.ToList<object>();
            //string[] u = new string[il.Count()];
            //foreach (var v in q)
            //{
            //    deplist.Add(v.depCode);
            //}
            //int pl = 0;
            //foreach(var i in q)
                
            //{
                
            //    u[pl] = i.depCode;
            //    pl++;
            //}

            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.ConsolidatedRequisitionListDetails

                    where x.ConsolidatedRequisitionList.Status == STATUS_OPEN && x.ItemNumber == itemNum
                    orderby x.DateRequest ascending
                    select x;
            List<string> deplist = new List<string>();
            List<ConsolidatedRequisitionListDetail> clist = q.ToList<ConsolidatedRequisitionListDetail>();
            foreach (ConsolidatedRequisitionListDetail c in clist)
            {
                deplist.Add(c.ConsolidatedRequisitionList.DepartmentCode);


            }
            
            return deplist;

        }

        public static List<int> getQtyNeedeOfDep(List<string> depCode, string itemNumebr )
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<int> qlist = new List<int>();
            foreach (string s in depCode)
            {
                var q = from x in context.ConsolidatedRequisitionListDetails
                        join y in context.ConsolidatedRequisitionLists on x.ConsolidatedListID equals y.ConsolidatedListID
                        where y.Status == STATUS_OPEN && y.DepartmentCode == s && x.ItemNumber == itemNumebr
                        orderby x.DateRequest descending
                        group x by x.ItemNumber into g
                        select new
                        {

                            itemNumber = g.Key,
                            totalQty = g.Sum(x => x.QuantityRequested)
                        };

                int j = Convert.ToInt32(q.First().totalQty);
                qlist.Add(j);
 
            }
            return qlist;
          
        }

        /** added by lee
         * */

        public static List<ConsolidatedRequisitionList> getAllOpenList()
        {
           
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.ConsolidatedRequisitionLists
                        where x.Status == STATUS_OPEN
                        select x;

            return query.ToList<ConsolidatedRequisitionList>();
        }
        public static void setListStatusToClosed(int consolidatedListId)
        {
            
            UniversityStoreEntities context = new UniversityStoreEntities();
            ConsolidatedRequisitionList consolidatedList = context.ConsolidatedRequisitionLists.Where
                (x => x.ConsolidatedListID == consolidatedListId).First();
            consolidatedList.Status = STATUS_CLOSED;
            context.SaveChanges();
        }


        /** end of lee
         * */



        /*** sreeja 
         * */

         public static List<ConsolidatedRequisitionListDetail> getAllImpendingList(string dep)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            List<ConsolidatedRequisitionListDetail> crld = new List<ConsolidatedRequisitionListDetail>();

            var s = from x in context.ConsolidatedRequisitionLists
                    where x.Status == STATUS_OPEN && x.DepartmentCode == dep
                    select x;

            List<ConsolidatedRequisitionList> crlist = s.ToList<ConsolidatedRequisitionList>();

            foreach (ConsolidatedRequisitionList clrrr in crlist)
            {
                System.Int32 cl_id = clrrr.ConsolidatedListID;

                var query = from x in context.ConsolidatedRequisitionListDetails
                            where x.ConsolidatedListID == cl_id
                            select x;

                List<ConsolidatedRequisitionListDetail> li = query.ToList<ConsolidatedRequisitionListDetail>();

                foreach (ConsolidatedRequisitionListDetail crobj in li)
                {
                    crld.Add(crobj);
                }

            }

            return crld;

        }


        //--sreeja--//
        public static List<ConsolidatedRequisitionList> getPendingReqAllDepts()
        {

            UniversityStoreEntities context = new UniversityStoreEntities();


            var q = from x in context.ConsolidatedRequisitionLists
                    where x.Status == STATUS_OPEN
                    select x;
            return q.ToList();
        }
        //--sreeja--//
    }
        /** end sreeja
         * */

    
}
