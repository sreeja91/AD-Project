using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class adjustmentVoucherController
    {
         public static List<Object> getAllPendingAdjustmentVoucher()
        {
            return adjustmentVoucherDAO.getPendingAdjustmentVoucher();
        }

        public static List<Object> getTheAdjustmentVoucherDetail(int vocherNo)
         {
             return adjustmentVoucherDAO.getAdjustmentVoucherDetail(vocherNo);
         }

       

        public static void deleteObjectDetail(int voucherNo)
          {
              adjustmentVoucherDAO.deleteAdjustmentVoucherDetail(voucherNo);
          }

        public static void deleteObject(int voucher)
        {
            adjustmentVoucherDAO.deleteAdjustmentVoucher(voucher);
        }

         public static void UpdatenegativeStock(String Pid, int qnt)
        {
            ProductDAO.UpdateStockReduce(Pid,qnt);
        }

         public static void UpdatePositiveStock(String Pid, int qnt)
         {
             ProductDAO.UpdateStock(Pid,qnt);
         }
    }
}