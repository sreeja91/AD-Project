using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class LoginController
    {

        //EmployeeDAO dao = new EmployeeDAO();
        public static Employee validateLogin(string username, string password)
        {
            Employee emp = EmployeeDAO.getEmployeeinfo(username);
            
            if (emp == null)
                return null;
            else if (password==emp.Password)
            {
                return emp;
            }
            else
                return null;
        
        }


        public static Employee getEmployeeinfo(string username)
        {
            Employee emp = EmployeeDAO.getEmployeeinfo(username);
            return emp;
        
        }
        /**** added by zhu
         * */
        public static bool verifyUserName(String uname, String pass)
        {
            return EmployeeDAO.validatelogin(uname, pass);
        }

        //public static string getRole(string userna)
        //{
        //    return EmployeeDAO.getRole(userna);
        //}
        public static Employee getUser(string userna)
        {
            return EmployeeDAO.getEmpoyeeByUserName(userna);
        }

        /** end by zhu
         * */


    }
}