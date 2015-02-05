using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class DeliverRequestController
    {

        public static List<CollectionPoint> getAllCollectionPoints()
        {
            List<CollectionPoint> collectionPointList = CollectionPointEFFacade.getCollectionPoint();
            return collectionPointList;
        }

        public static List<Department> getAllDeptForCollectionPointID(string collectionPointId)
        {
            //get all dept for collection point who have pending consolidated list(awaiting)
            
            //1) get all depts for that collection point id
            List<Department> deptForCollectionPtIDList = DepartmentDAO.getDepartmentsForCollectionPointId(collectionPointId);

            //2) get all depts with consolidated list in awaiting
            List<ConsolidatedRequisitionList> awaiting_Consol_List = ConsolidatedRequisitionListEFFacade.getAllAwaitingStatus();

            // 3) create new list for depts for that collection point with awaiting consolidated list
            List<Department> finalDeptList = new List<Department>();

            foreach (ConsolidatedRequisitionList consol_List in awaiting_Consol_List)
            {
                foreach (Department dept in deptForCollectionPtIDList)
                {
                    if (dept.DepartmentCode.Equals(consol_List.DepartmentCode))
                    {
                        //check if theres duplicate in finallist if not add to list
                        bool isduplicate = false;

                        foreach(Department dep in finalDeptList)
                        {
                            if(dept.DepartmentCode.Equals(dep.DepartmentCode))
                                isduplicate = true;
                        }

                        if (!isduplicate)
                            finalDeptList.Add(dept);
                    }
                }
            }

            return finalDeptList;
        }

        public static bool checkAuthorisation(string deptId, string username, string password)
        {
            Employee deptRep = DepartmentDAO.getDepartmentRepDetails(deptId);

            if(deptRep.UserName.Equals(username) && deptRep.Password.Equals(password))
                return true;

            else
                return false;
        }

        public static List<ConsolidatedRequisitionListDetail> getAllAwaitingItemsForDept(string deptCode)
        {
            List<ConsolidatedRequisitionList> list = ConsolidatedRequisitionListEFFacade.getAllAwaitingForDept(deptCode);
            List<ConsolidatedRequisitionListDetail> detailsList = new List<ConsolidatedRequisitionListDetail>();

            foreach (ConsolidatedRequisitionList conlist in list)
            {
                foreach (ConsolidatedRequisitionListDetail detail in conlist.ConsolidatedRequisitionListDetails)
                {
                    detailsList.Add(detail);
                }
            }

            return detailsList;
        }

        //public static void acknowledgeCollectItems1(List<ConsolidatedRequisitionListDetail> detailsList)
        //{
        //    foreach (ConsolidatedRequisitionListDetail detailItem in detailsList)
        //    {
        //        ConsolidatedRequisitionListDetail item = ConsolidatedRequisitionListEFFacade.getItemWithMatchingListIDAndItemCode(detailItem.ConsolidatedListID, detailItem.ItemNumber);
                

        //        if (detailItem.ActualQuantity < item.ActualQuantity)
        //        {
        //            int difference = (int)item.ActualQuantity - (int)detailItem.ActualQuantity;
        //            ConsolidatedRequisitionListEFFacade.setActualQuantityForItem(detailItem.ConsolidatedListID, detailItem.ItemNumber, (int)detailItem.ActualQuantity);

        //            //raise voucher and update unfulfilled here

        //            //1. update unfulfilled items table
        //            updateUnfulfilledList(detailItem, difference);

        //            //2. raise adjustment voucher
        //            raiseNewAdjustmentVoucher(detailItem, difference);

        //            //3. update stock
        //            ProductDAO.UpdateStock(detailItem.ItemNumber, difference);

        //        }
        //    }
        //}

        private static void updateUnfulfilledList(ConsolidatedRequisitionListDetail detailItem, int quantity)
        {
            Department dept = ConsolidatedRequisitionListEFFacade.getListForConsolidatedItem(detailItem);
            Unfulfilled uf = new Unfulfilled();
            uf.ItemNumber = detailItem.ItemNumber;
            uf.DepartmentCode = dept.DepartmentCode;
            uf.UnfulfilledQuantity = quantity;
            uf.Status = UnfulfilledDAO.STATUS_PENDING;
            uf.DateCreated = DateTime.Now;
            UnfulfilledDAO.CreateUnfulfilled(uf);
        }

        private static void raiseNewAdjustmentVoucher(ConsolidatedRequisitionListDetail item, int quantity)
        {
            MakeAdjustmentItem adjustmentItem = new MakeAdjustmentItem();
            adjustmentItem.Comment = "Damaged";
            adjustmentItem.ItemNumber = item.ItemNumber;
            adjustmentItem.Quantity = quantity;
            adjustmentItem.Status = AdjustmentVoucherEFFacade.STATUS_DECREASE;

            List<MakeAdjustmentItem> list = new List<MakeAdjustmentItem>();
            list.Add(adjustmentItem);

            MakeAdjustmentVoucherController.makeNewAdjustmentVoucher(list);
        }



        /** added by ashwin
         * */
        public static Department getdepartmentdetails(string deptCode)
        {
            Department dept = DepartmentDAO.getdepartment(deptCode);

            return dept;

        }

        public static Employee getEmployee(String empNo)
        {
            Employee emp = EmployeeDAO.getEmployeeByid(empNo);
            return emp;
        }

        public static void updatedepartmentcollection(Department dep, string s2, string s3)
        {
            DepartmentDAO.updatecollectiondetails(dep, s2, s3);

        }
        public static Department getdepartmentfromempid(String empid)
        {
            Department dep = DepartmentDAO.getdepartmentfromempid(empid);
            return dep;
        }

        public static Employee getEmployeefromname(String empName)
        {
            Employee emp = EmployeeDAO.getemployeefromname(empName);
            return emp;
        }
        public static CollectionPoint getcollectionpoint(string g)
        {
            CollectionPoint cp = CollectionPointEFFacade.getcollectionpointfromname(g);
            return cp;
        }


        /** end by ashwin
         * */



        /** added by lee
         * */

        public static bool acknowledgeCollectItems(List<ConsolidatedRequisitionListDetail> detailsList)
        {
            List<int> listIDs = new List<int>();

            foreach (ConsolidatedRequisitionListDetail detailItem in detailsList)
            {
                ConsolidatedRequisitionListDetail item = ConsolidatedRequisitionListEFFacade.getItemWithMatchingListIDAndItemCode(detailItem.ConsolidatedListID, detailItem.ItemNumber);

                if (detailItem.ActualQuantity < item.ActualQuantity)
                {
                    int difference = (int)item.ActualQuantity - (int)detailItem.ActualQuantity;
                    ConsolidatedRequisitionListEFFacade.setActualQuantityForItem(detailItem.ConsolidatedListID, detailItem.ItemNumber, (int)detailItem.ActualQuantity);

                    //raise voucher and update unfulfilled here

                    //1. update unfulfilled items table
                    updateUnfulfilledList(detailItem, difference);

                    //2. raise adjustment voucher
                    raiseNewAdjustmentVoucher(detailItem, difference);

                    //3. update stock
                    ProductDAO.UpdateStock(detailItem.ItemNumber, difference);
                }

                if (!listIDs.Contains(detailItem.ConsolidatedListID))
                {
                    listIDs.Add(detailItem.ConsolidatedListID);
                }
            }

            foreach (int id in listIDs)
            {
                ConsolidatedRequisitionListEFFacade.setListStatusToClosed(id);
            }

            return true;
        }







        /** end of lee
         * */
    }
}
