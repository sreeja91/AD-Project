using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class IssueAdjustmentVoucherController
    {
        private static string SUPERVISOR = "storesupervisor";
        private static string MANAGER = "storemanager";

        public static List<AdjustmentVoucher> retrieveAllIssueVoucherForRole(string role)
        {
            List<AdjustmentVoucher> list = null;

            if (role.ToLower().Equals(SUPERVISOR))
                list = adjustmentVoucherDAO.getAllVouchersForStoreSupervisor();
            
            else if (role.ToLower().Equals(MANAGER))
                list = adjustmentVoucherDAO.getAllVouchersForStoreManager();

            return list;
        }

        public static List<AdjustmentVoucherDetail> retrieveAllItemsWithVoucherNumber(int voucherNumber, string role)
        {
            List<AdjustmentVoucherDetail> list = new List<AdjustmentVoucherDetail>();

            switch(role.ToLower())
            {
                case "storesupervisor":
                    list = adjustmentVoucherDAO.getItemsForSupervisorApproval(voucherNumber);
                    break;

                case "storemanager":
                    list = adjustmentVoucherDAO.getItemsForManagerApproval(voucherNumber);
                    break;
            }
            return list;
        }

        public static void approveItemsForVoucherNumber(int voucherNumber, string role)
        {
            string roleLC = role.ToLower();

            if (roleLC.Equals(SUPERVISOR) || roleLC.Equals(MANAGER))
            {
                //update adjustmentdetailsobject aprovalstatus column to approved
                if (roleLC.Equals(SUPERVISOR))
                {
                    List<AdjustmentVoucherDetail> list = adjustmentVoucherDAO.getItemsForSupervisorApproval(voucherNumber);
                    adjustmentVoucherDAO.approveVoucherDetailItemsBySupervisor(list);
                    //updateStockForItems(list);
                }
                else if (roleLC.Equals(MANAGER))
                {
                    List<AdjustmentVoucherDetail> list = adjustmentVoucherDAO.getItemsForManagerApproval(voucherNumber);
                    adjustmentVoucherDAO.approveVoucherDetailItemsByManager(list);
                   // updateStockForItems(list);
                }
            }
        }
        //added by ashwin 
        public static void approveitems(List<AdjustmentVoucherDetail> list)
        {
           // List<AdjustmentVoucherDetail> list = AdjustmentVoucherEFFacade.getitemsforapproval(voucher);
            updatestockfromadjustment(list);
            AdjustmentVoucherEFFacade.approvestatusadjustmentvoucher(list);
            
        }
        private static void updateStockForItems(List<AdjustmentVoucherDetail> list)
        {
            foreach(AdjustmentVoucherDetail voucherDetail in list)
            {
                ProductDAO.UpdateStock(voucherDetail.ItemNumber, (int)voucherDetail.Quantity);  
               
            }
        }
        private static void updatestockfromadjustment(List<AdjustmentVoucherDetail> list)
        {
            foreach (AdjustmentVoucherDetail voucherdetail in list)
            {
                ProductDAO.UpdateStockfromadjustment(voucherdetail.ItemNumber, voucherdetail.Status, (int)voucherdetail.Quantity);

            }
        }



    }
}
