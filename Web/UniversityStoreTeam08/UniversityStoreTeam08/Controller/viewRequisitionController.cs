using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class viewRequisitionController
    {
        public static string getApprovedByWho(string depName)
        {
            return RequisitionDAO.getApprovedBy(depName);
        }


        public static List<ConsolidatedRequisitionListDetail> getPendingReqList(string dept)
        {
            return ConsolidatedRequisitionListEFFacade.getAllImpendingList(dept); 

        }



        //--- new methods --//

        public static List<Department> GetAllDepartments()
        {
            return DepartmentDAO.GetAllDepartments();
        }


        public static List<ConsolidatedRequisitionListDetail> getAllImpendingList(string dep)
        {
            return ConsolidatedRequisitionListEFFacade.getAllImpendingList(dep);
        }

        public static string getDeptHead(string dept)
        {
            return EmployeeDAO.getDeptHead(dept);
        }

        //--sreeja--//
        public static List<ConsolidatedRequisitionList> getPendingReqAllDepts()
        {
            return ConsolidatedRequisitionListEFFacade.getPendingReqAllDepts();
        }
        //--sreeja--//
    }
}
