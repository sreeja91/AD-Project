using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace UniversityStoreTeam08
{
    class ApproveRequestController
    {
        public static List<Object> populateAllPendingReq(string empNum)
        {
            string s= EmployeeDAO.getDepCodetOfEmp(empNum);
            
            return RequisitionDAO.getAllPendingReqData(s);

 
        }

        public static List<Object> pupulateDetails(string reqListNumber)
        {
          return  RequisitionDAO.getDetialsOfList(reqListNumber);
          

        }

        public static void submitApproval(string reqListNumber, string mobileORweb)
        {
            if (mobileORweb == "web")
            {
                RequisitionDAO.changeReqListStatusApproved(reqListNumber);

            }

            else if (mobileORweb=="mobile")
            {
                Employee e = RequisitionDAO.getEmp(reqListNumber);
                string receiverEmail = e.Email;
                string depCode = e.DepartmentCode;
                Employee head = EmployeeDAO.getDepHeadEmployee(depCode);
                string senderEmail = head.Email;

                MailMessage m = new MailMessage(senderEmail, receiverEmail);
                m.Subject = "";
                m.Body = "Your request has been approved";
                SmtpClient c = new SmtpClient("lynx.iss.nus.edu.sg");
                c.Send(m);
                RequisitionDAO.changeReqListStatusApproved(reqListNumber);

            }
            //RequisitionDAO.changeReqListStatusApproved(reqListNumber);

        }

        public static void submitRejected(string reqListNumber,string comment)
        {
            RequisitionDAO.changeReqListStatusRejected(reqListNumber,comment);

            Employee e = RequisitionDAO.getEmp(reqListNumber);
            string receiverEmail = e.Email;
            string depCode = e.DepartmentCode;
            Employee head = EmployeeDAO.getDepHeadEmployee(depCode);
            string senderEmail = head.Email;

            MailMessage m = new MailMessage(senderEmail, receiverEmail);
            m.Subject = "";
            m.Body = "Your request has been rejected";
            SmtpClient c = new SmtpClient("lynx.iss.nus.edu.sg");
            c.Send(m);
            RequisitionDAO.changeReqListStatusApproved(reqListNumber);


        }


        /** added by nitin for wcf
         * */
        public static List<RequisitionList> getAllPendingReq(string empNum) /// for WCF
        {
            string s = EmployeeDAO.getDepCodetOfEmp(empNum);
            return RequisitionDAO.getAllPendingReqDataW(s);

        }

        public static List<RequisitionDetail> reqDetailsW(string reqListNumber) //for WCF
        {
            return RequisitionDAO.getDetialsOfListW(reqListNumber);

        }

        public static void submitRejected(string reqListNumber)
        {
            RequisitionDAO.changeReqListStatusRejected(reqListNumber,"rejected from mobile");

        }

       

    }
}
