using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class DelegateAuthorityController
    {
        public static List<Object> getEmployeeList(String depHeadNumber)
        {
            return EmployeeDAO.getEmployeeList(depHeadNumber);
        
        }

        public static List<Employee> getCurrentDelegate(String depHeadNumber)
        {
            return EmployeeDAO.getCurrentDelegate(depHeadNumber);
        }

        public static void RemoveDelegate(String empname)
        {
            EmployeeDAO.RemoveDelegate(empname);
        
        }

        public static void createDelegate(String empname)
        {
            EmployeeDAO.createDelegate(empname);
        
        }

        public static List<Object> searchByEmployeeName(String empname,String depHeadNumber)
        {
            return EmployeeDAO.searchByEmployeeName(empname,depHeadNumber);
        
        }

        /** added by zhu
         * */
        public static string getDelegateName(String empNum)
        {
            return EmployeeDAO.getDelegateName(empNum);
        }

        public static List<Employee> getEmployee(String depHeadNumber)
        {
            return EmployeeDAO.getEmployee(depHeadNumber);
        }

        /** end by zhu
         * */

    }
}