using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class adjustmentVoucherDAO
    {
        //public static UniversityStoreEntities CONTEXT = new UniversityStoreEntities();

        public static string STATUS_INCREASE = "increase";
        public static string STATUS_DECREASE = "reduce";

        public static string APPROVAL_STATUS_PENDING = "pending";
        public static string APPROVAL_STATUS_APPROVED = "approved";
      
        /////*********Wu************/////
        public static List<Object> getPendingAdjustmentVoucher()
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
            var query = from x in CONTEXT.AdjustmentVoucherDetails
                        where x.Status == APPROVAL_STATUS_PENDING
                        select new
                        {
                            x.VoucherNumber,
                            x.AdjustmentVoucher.DateCreated
                        };
            return query.ToList<Object>();                             
        }

        public static List<Object> getAdjustmentVoucherDetail(int vocherNo)
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
            var q = from x in CONTEXT.AdjustmentVoucherDetails
                    where x.VoucherNumber == vocherNo
                    select new
                    {
                        x.ItemNumber,
                        x.Quantity,
                        x.Comment
                    };
            return q.ToList<Object>();
        }

        //public static DateTime getDate(int VoucherNo)
        //{
        //    var q2 = from x in CONTEXT.AdjustmentVouchers
        //             where x.VoucherNumber == VoucherNo
        //             select x;

            

        //    DateTime av =(DateTime) q2.First<AdjustmentVoucher>().DateCreated;
        //    return av;       
            
       public static void deleteAdjustmentVoucherDetail(int voucherNo)
       {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
           var a=from x in CONTEXT.AdjustmentVoucherDetails
                 where x.VoucherNumber==voucherNo
                 select x;

           AdjustmentVoucherDetail d= a.First<AdjustmentVoucherDetail>();
           CONTEXT.DeleteObject(d);
           CONTEXT.SaveChanges();
           
       }

        public static void deleteAdjustmentVoucher(int voucher)
       {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
           var b = from x in CONTEXT.AdjustmentVouchers
                   where x.VoucherNumber == voucher
                   select x;

           AdjustmentVoucher q = b.First<AdjustmentVoucher>();
           CONTEXT.DeleteObject(q);
           CONTEXT.SaveChanges();
       }

        /////*********Wu************/////

        /////*********Come From WCF************/////

        public static List<AdjustmentVoucher> getAllVouchersForStoreManager()
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
            var query = (from v in CONTEXT.AdjustmentVouchers
                         from vd in CONTEXT.AdjustmentVoucherDetails
                         where v.VoucherNumber == vd.VoucherNumber && vd.TotalPrice >= 250 && vd.ApprovalStatus == APPROVAL_STATUS_PENDING
                         select v).Distinct();

            return query.ToList<AdjustmentVoucher>();
        }

        public static List<AdjustmentVoucher> getAllVouchersForStoreSupervisor()
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
            var query = (from v in CONTEXT.AdjustmentVouchers
                         from vd in CONTEXT.AdjustmentVoucherDetails
                         where v.VoucherNumber == vd.VoucherNumber && vd.TotalPrice < 250 && vd.ApprovalStatus == APPROVAL_STATUS_PENDING
                         select v).Distinct();

            return query.ToList<AdjustmentVoucher>();
        }

        public static List<AdjustmentVoucherDetail> getItemsForManagerApproval(int voucherNumber)
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
            var query = from x in CONTEXT.AdjustmentVoucherDetails
                        where x.TotalPrice >= 250 && x.ApprovalStatus == APPROVAL_STATUS_PENDING && x.VoucherNumber == voucherNumber
                        select x;

            return query.ToList<AdjustmentVoucherDetail>();
        }

        public static List<AdjustmentVoucherDetail> getItemsForSupervisorApproval(int voucherNumber)
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
            var query = from x in CONTEXT.AdjustmentVoucherDetails
                        where x.TotalPrice < 250 && x.ApprovalStatus == APPROVAL_STATUS_PENDING && x.VoucherNumber == voucherNumber
                        select x;

            return query.ToList<AdjustmentVoucherDetail>();
        }
        //Addidng a function-Ashwin

        public static List<AdjustmentVoucherDetail> getitemsfroapproval(int vouchernumber)
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
            var q = from x in CONTEXT.AdjustmentVoucherDetails
                    where x.ApprovalStatus == APPROVAL_STATUS_PENDING && x.VoucherNumber == vouchernumber
                    select x;
            return q.ToList<AdjustmentVoucherDetail>();
        }
        //added a function -ashwin
        public static void approvestatusadjustmentvoucher(int voucherno)
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
            var q = from x in CONTEXT.AdjustmentVoucherDetails
                    where x.ApprovalStatus == APPROVAL_STATUS_PENDING && x.VoucherNumber == voucherno
                    select x;
            List<AdjustmentVoucherDetail> list = q.ToList<AdjustmentVoucherDetail>();

            foreach (AdjustmentVoucherDetail ad in list)
            {
                ad.ApprovalStatus = APPROVAL_STATUS_APPROVED;
                CONTEXT.SaveChanges();
            }
        }

        public static void approveVoucherDetailItemsBySupervisor(List<AdjustmentVoucherDetail> list)
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();

            foreach (AdjustmentVoucherDetail detail in list)
            {
                var query = from x in CONTEXT.AdjustmentVoucherDetails
                            where x.VoucherNumber == detail.VoucherNumber && detail.ItemNumber == x.ItemNumber && x.ApprovalStatus == detail.ApprovalStatus
                            select x;

                AdjustmentVoucherDetail voucherDetailObject = query.First();
                int qnty=Convert.ToInt32(voucherDetailObject.Quantity);
                voucherDetailObject.ApprovalStatus = APPROVAL_STATUS_APPROVED;
                var q = from x in CONTEXT.Products
                        where x.ItemNumber == detail.ItemNumber
                        select x;
                Product prod=q.First<Product>();
                if (voucherDetailObject.Status == STATUS_INCREASE)
                    ProductDAO.UpdateStock(prod.ItemNumber, qnty);
                else
                    ProductDAO.UpdateStockReduce(prod.ItemNumber, qnty);
               
                CONTEXT.SaveChanges();
            }

            //foreach (AdjustmentVoucherDetail detail in itemsApproved)
            //{
            //    AdjustmentVoucherDetail d = CONTEXT.AdjustmentVoucherDetails.Where(x => x.VoucherNumber == detail.VoucherNumber).First();
            //    d.ApprovalStatus = APPROVAL_STATUS_APPROVED;
            //    CONTEXT.SaveChanges();
            //}

        }

        public static void approveVoucherDetailItemsByManager(List<AdjustmentVoucherDetail> list)
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();

            foreach (AdjustmentVoucherDetail detail in list)
            {
                var query = from x in CONTEXT.AdjustmentVoucherDetails
                            where x.VoucherNumber == detail.VoucherNumber && detail.ItemNumber == x.ItemNumber && x.ApprovalStatus == detail.ApprovalStatus
                            select x;

                AdjustmentVoucherDetail voucherDetailObject = query.First();
                int qnty = Convert.ToInt32(voucherDetailObject.Quantity);
                voucherDetailObject.ApprovalStatus = APPROVAL_STATUS_APPROVED;
                var q = from x in CONTEXT.Products
                        where x.ItemNumber == detail.ItemNumber
                        select x;
                Product prod = q.First<Product>();
                if (voucherDetailObject.Status == STATUS_INCREASE)
                    ProductDAO.UpdateStock(prod.ItemNumber, qnty);
                else
                    ProductDAO.UpdateStockReduce(prod.ItemNumber, qnty);

                CONTEXT.SaveChanges();
            }

        }


        public static void addNewAdjustVoucherWithArrayVoucherDetails(AdjustmentVoucher voucher)
        {  UniversityStoreEntities CONTEXT = new UniversityStoreEntities();
            CONTEXT.AddToAdjustmentVouchers(voucher);
            CONTEXT.SaveChanges();
        }
          
     }
  }
