using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace UniversityStoreTeam08
{
    class AdjustmentVoucherEFFacade
    {
       // private static UniversityStoreEntities context = new UniversityStoreEntities();

        public static string EMPLOYEE_ROLE_MANAGER = "StoreManager";
        public static string EMPLOYEE_ROLE_SUPERVISOR = "StoreSupervisor";

        public static string STATUS_INCREASE = "increase";
        public static string STATUS_DECREASE = "reduce";

        public static string APPROVAL_STATUS_PENDING = "pending";
        public static string APPROVAL_STATUS_APPROVED = "approved";

        public static List<AdjustmentVoucher> getAllVouchersForStoreManager()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = (from v in context.AdjustmentVouchers
                        from vd in context.AdjustmentVoucherDetails
                        where v.VoucherNumber == vd.VoucherNumber && vd.TotalPrice >= 250 && vd.ApprovalStatus == APPROVAL_STATUS_PENDING
                        select v).Distinct();

            return query.ToList<AdjustmentVoucher>();
        }

        public static List<AdjustmentVoucher> getAllVouchersForStoreSupervisor()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = (from v in context.AdjustmentVouchers
                         from vd in context.AdjustmentVoucherDetails
                         where v.VoucherNumber == vd.VoucherNumber && vd.TotalPrice < 250 && vd.ApprovalStatus == APPROVAL_STATUS_PENDING
                         select v).Distinct();

            return query.ToList<AdjustmentVoucher>();
        }

        public static List<AdjustmentVoucherDetail> getItemsForManagerApproval(int voucherNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.AdjustmentVoucherDetails
                        where x.TotalPrice >= 250 && x.ApprovalStatus == APPROVAL_STATUS_PENDING && x.VoucherNumber == voucherNumber
                        select x;

            return query.ToList<AdjustmentVoucherDetail>();
        }
       
        public static List<AdjustmentVoucherDetail> getItemsForSupervisorApproval(int voucherNumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.AdjustmentVoucherDetails
                        where x.TotalPrice < 250 && x.ApprovalStatus == APPROVAL_STATUS_PENDING && x.VoucherNumber == voucherNumber
                        select x;

            return query.ToList<AdjustmentVoucherDetail>();
        }

        public static void approveVoucherDetailItemsBySupervisor(List<AdjustmentVoucherDetail> list)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();

            foreach (AdjustmentVoucherDetail detail in list)
            {
                var query = from x in context.AdjustmentVoucherDetails
                            where x.VoucherNumber == detail.VoucherNumber && detail.ItemNumber == x.ItemNumber && x.ApprovalStatus == detail.ApprovalStatus
                            select x;

                AdjustmentVoucherDetail voucherDetailObject = query.First();
                voucherDetailObject.ApprovalStatus = APPROVAL_STATUS_APPROVED;

                context.SaveChanges();
            }

            //foreach (AdjustmentVoucherDetail detail in itemsApproved)
            //{
            //    AdjustmentVoucherDetail d = context.AdjustmentVoucherDetails.Where(x => x.VoucherNumber == detail.VoucherNumber).First();
            //    d.ApprovalStatus = APPROVAL_STATUS_APPROVED;
            //    context.SaveChanges();
            //}

        }

        public static void approveVoucherDetailItemsByManager(List<AdjustmentVoucherDetail> list)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            foreach (AdjustmentVoucherDetail detail in list)
            {
                var query = from x in context.AdjustmentVoucherDetails
                            where x.VoucherNumber == detail.VoucherNumber && detail.ItemNumber == x.ItemNumber && x.ApprovalStatus == detail.ApprovalStatus
                            select x;

                AdjustmentVoucherDetail voucherDetailObject = query.First();
                voucherDetailObject.ApprovalStatus = APPROVAL_STATUS_APPROVED;

                context.SaveChanges();
            }

        }


        public static void addNewAdjustVoucherWithArrayVoucherDetails(AdjustmentVoucher voucher)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            context.AddToAdjustmentVouchers(voucher);
            context.SaveChanges();
        }


        //////////////////////****************AShwin//////////////////////
        public static List<AdjustmentVoucher> CheckEmployeeRole(String Role)
        {
            List<AdjustmentVoucher> adjustvouch = new List<AdjustmentVoucher>();
            if (Role == EMPLOYEE_ROLE_SUPERVISOR)
            {
                adjustvouch = AdjustmentVoucherEFFacade.getAllVouchersForStoreSupervisor();
            }
            else
            {
                adjustvouch = AdjustmentVoucherEFFacade.getAllVouchersForStoreManager();
            }
            return adjustvouch;
        }

        public static void approvestatusadjustmentvoucher(List<AdjustmentVoucherDetail> list)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            //var q = from x in context.AdjustmentVoucherDetails
              //      where x.ApprovalStatus == APPROVAL_STATUS_PENDING && x.VoucherNumber == voucherno
                //    select x;
          //  List<AdjustmentVoucherDetail> list = q.ToList<AdjustmentVoucherDetail>();

            foreach (AdjustmentVoucherDetail ad in list)
            {
                ad.ApprovalStatus = APPROVAL_STATUS_APPROVED;
                context.SaveChanges();
            }
        }
        public static List<AdjustmentVoucherDetail> getitemsforapproval(int vouchernumber)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.AdjustmentVoucherDetails
                    where x.ApprovalStatus == APPROVAL_STATUS_PENDING && x.VoucherNumber == vouchernumber
                    select x;
            return q.ToList<AdjustmentVoucherDetail>();
        }

    }
}
