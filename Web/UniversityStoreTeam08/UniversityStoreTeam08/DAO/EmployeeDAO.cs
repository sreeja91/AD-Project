using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class EmployeeDAO
    {

        static String ROLE_EMPLOYEE = "Employee";
        static String ROLE_DELEGATE_HEAD = "DelegateHead";
        static String ROLE_HEAD = "Head";

        public static List<Employee> getCurrentDelegate(String depHeadNumber)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var query = from x in EN.Employees
                        where x.EmployeeNumber == depHeadNumber
                        select x;

            string e = query.First<Employee>().DepartmentCode;


            var query2 = from x in EN.Employees
                         where x.DepartmentCode == e
                         where x.Role == ROLE_DELEGATE_HEAD
                         select x
                         ;

            List<Employee> c = query2.ToList<Employee>();
            return c;

            //List<Employee> emp = query2.ToList<Employee>();

            // if (emp.Count!=0)
            // {
            //     Employee employee = query2.First<Employee>();

            //     String empname = employee.Name;

            //     return ;
            // }
            // else
            //     return null;

        }
        public static void RemoveDelegate(String empno)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q1 = from x in EN.Employees
                     where x.EmployeeNumber == empno
                     select x;


            Employee e = q1.First<Employee>();
            e.Role = ROLE_EMPLOYEE;
            EN.SaveChanges();

        }

        public static void createDelegate(String empno)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q = from x in EN.Employees
                    where x.EmployeeNumber == empno
                    select x;


            Employee e = q.First<Employee>();
            e.Role = ROLE_DELEGATE_HEAD;
            EN.SaveChanges();

        }

        public static List<Object> searchByEmployeeName(String empname, String depHeadNumber)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var query = from x in EN.Employees
                        where x.EmployeeNumber == depHeadNumber
                        select x;
            string e = query.First<Employee>().DepartmentCode;


            var query2 = from x in EN.Employees
                         where x.DepartmentCode == e
                           && x.Name.Contains(empname)
                           && x.Role == ROLE_EMPLOYEE
                         select new
                         {
                             x.EmployeeNumber,
                             x.Name,
                             x.Email
                         };

            List<Object> objs = query2.ToList<Object>();
            return objs;


        }

        public static List<Object> getEmployeeList(String depHeadNumber)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var query = from x in EN.Employees
                        where x.EmployeeNumber == depHeadNumber
                        select x;
            string e = query.First<Employee>().DepartmentCode;


            var query2 = from x in EN.Employees
                         where x.DepartmentCode == e
                         where x.Role == ROLE_EMPLOYEE
                         select new
                         {
                             x.EmployeeNumber,
                             x.Name,
                             x.Email
                         };

            List<Object> c = query2.ToList<Object>();
            return c;
        }


        //////****************Wu********************///////

        public static string getClerkName(string clerk, string username)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var query = from x in EN.Employees
                        where x.Role == clerk && x.UserName == username
                        select x.Name;
            return query.ToString();
        }

        //////****************Wu********************///////

        //////****************Sreeja********************///////
        public static string getDepCodetOfEmp(string empNumber)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q = from x in EN.Employees
                    where x.EmployeeNumber == empNumber
                    select x;
            Employee el = q.First<Employee>();
            return el.DepartmentCode.ToString();
        }

        public static string getDeptHead(string dept)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q1 = from x in EN.Employees
                     where x.Role == ROLE_HEAD
                     where x.DepartmentCode == dept
                     select x;

            string e = q1.First<Employee>().DepartmentCode;

            var q2 = from x in EN.Employees
                     where x.DepartmentCode == e
                     where x.Role == ROLE_HEAD
                     select x;

            string name = q2.First<Employee>().Name;
            return name;
        }
        
        public static Employee getEmployeeinfo(string username)
        {

            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q = from x in EN.Employees
                    where x.UserName == username
                    select x;

            if (q.Count() != 0)
            {
                Employee emp = q.First<Employee>();
                return emp;
            }
            else
            {
                return null;
            }

        }

        //////****************Sreeja********************///////     

        //////****************Tiger********************///////  
        public static Employee getEmployeeinfoByEmployeeNumber(string empNo)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q = from x in EN.Employees
                    where x.EmployeeNumber == empNo
                    select x;

            if (q.Count() != 0)
            {
                Employee emp = q.First<Employee>();
                return emp;
            }
            else
            {
                return null;
            }
        }
        //////****************Sreeja********************///////  
         
        
        /****added by zhu
         * */
        public static bool verifyUserName(String uname, String pass)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            return EmployeeDAO.verifyUserName(uname, pass);
        }
        //public static string getRole(string userna)
        //{
        //    return EmployeeDAO.getRole(userna);
        //}
        //public static List<Employee> getUser(string userna)
        //{
        //    return EmployeeDAO.getUser(userna);
        //}

        public static Employee getEmpoyeeByUserName(string userName)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q = from x in EN.Employees
                    where x.UserName == userName
                    select x;
            return q.First<Employee>();
        }

        public static string getDelegateName(String empNum)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var query = from x in EN.Employees
                        where x.EmployeeNumber == empNum
                        select x;

            string e = query.First<Employee>().DepartmentCode;

            var query2 = from x in EN.Employees
                         where x.DepartmentCode == e
                         where x.Role == ROLE_DELEGATE_HEAD
                         select x;

            string c = query2.First<Employee>().Name;

            return c;
        }

        public static List<Employee> getEmployee(String depHeadNumber)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var query = from x in EN.Employees
                        where x.EmployeeNumber == depHeadNumber
                        select x;

            string e = query.First<Employee>().DepartmentCode;


            var query2 = from x in EN.Employees
                         where x.DepartmentCode == e
                         where x.Role == ROLE_EMPLOYEE
                         select x
                         ;

            List<Employee> c = query2.ToList<Employee>();
            return c;

        }


        /** end by zhu
         * */


        /** added by ashwin
         * */
        public static Employee getEmployeeByid(string empNo)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q = from x in EN.Employees
                    where x.EmployeeNumber == empNo
                    select x;

            return q.First<Employee>();
        }
        public static List<Employee> getallemployees(String deptcode)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q = from x in EN.Employees
                    where x.DepartmentCode == deptcode
                    select x;
            List<Employee> emp = q.ToList<Employee>();
            return emp;

        }


        public static Employee getemployeefromname(string s)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q = from x in EN.Employees
                    where x.Name.Contains(s)
                    select x;
            Employee e = q.First<Employee>();
            return e;
        }
        public static bool validatelogin(string user, string pass)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q = from x in EN.Employees
                    where x.UserName==user && x.Password==pass
                    select x;
            if ((q.ToArray<Employee>().Count()) > 0)
            {
                return true;
            }
            else return false;

        }

        /** end by ashwin
         * */

        public static Employee getDepHeadEmployee(string dept)
        {
            UniversityStoreEntities EN = new UniversityStoreEntities();
            var q1 = from x in EN.Employees
                     where x.Role == ROLE_HEAD
                     where x.DepartmentCode == dept
                     select x;

            return q1.First<Employee>();


        }
                                                                                                
    }
}
