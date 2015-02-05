using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace UniversityStoreTeam08
{
    class MakeAdjustmentVoucherController
    {

        public static void makeNewAdjustmentVoucher(List<MakeAdjustmentItem> adjustmentList)
        {
            AdjustmentVoucher adjustmentVoucher = new AdjustmentVoucher();
            adjustmentVoucher.DateCreated = DateTime.Now;
            EntityCollection<AdjustmentVoucherDetail> voucherDetailsCollection = new EntityCollection<AdjustmentVoucherDetail>();

            foreach (MakeAdjustmentItem item in adjustmentList)
            {
                Product p = ProductDAO.getSearchProductbyid(item.ItemNumber);
                double totalPrice = (double) p.AdjustmentVoucherPrice * item.Quantity;
                AdjustmentVoucherDetail avd = new AdjustmentVoucherDetail();
                avd.ItemNumber = item.ItemNumber;
                avd.Status = item.Status.Equals("Excess") ? AdjustmentVoucherEFFacade.STATUS_DECREASE : AdjustmentVoucherEFFacade.STATUS_INCREASE;
                avd.TotalPrice = totalPrice;
                avd.Comment = item.Comment;
                avd.ApprovalStatus = AdjustmentVoucherEFFacade.APPROVAL_STATUS_PENDING;
                avd.Quantity = item.Quantity;

                voucherDetailsCollection.Add(avd);
            }

            adjustmentVoucher.AdjustmentVoucherDetails = voucherDetailsCollection;

            AdjustmentVoucherEFFacade.addNewAdjustVoucherWithArrayVoucherDetails(adjustmentVoucher);
        }



        /**added by lee
         * */
         public static void makeNewAdjustmentVoucher(AdjustmentItemWCF[] adjustmentList)
        {
            AdjustmentVoucher adjustmentVoucher = new AdjustmentVoucher();
            adjustmentVoucher.DateCreated = DateTime.Now;
            EntityCollection<AdjustmentVoucherDetail> voucherDetailsCollection = new EntityCollection<AdjustmentVoucherDetail>();

            foreach (AdjustmentItemWCF item in adjustmentList)
            {
                AdjustmentVoucherDetail avd = new AdjustmentVoucherDetail();
                avd.ItemNumber = item.ItemNumber;
                avd.Status = item.Status.Equals("reduce") ? AdjustmentVoucherEFFacade.STATUS_DECREASE : AdjustmentVoucherEFFacade.STATUS_INCREASE;
                avd.TotalPrice = item.TotalPrice;
                avd.Comment = item.Comment;
                avd.ApprovalStatus = AdjustmentVoucherEFFacade.APPROVAL_STATUS_PENDING;
                avd.Quantity = item.Quantity;

                voucherDetailsCollection.Add(avd);
            }

            adjustmentVoucher.AdjustmentVoucherDetails = voucherDetailsCollection;

            AdjustmentVoucherEFFacade.addNewAdjustVoucherWithArrayVoucherDetails(adjustmentVoucher);
        }
    }



        /** end of lee
         * */

    }

