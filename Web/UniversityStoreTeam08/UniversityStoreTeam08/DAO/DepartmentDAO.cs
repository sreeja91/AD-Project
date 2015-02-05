using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class DepartmentDAO
    {
        //public static UniversityStoreEntities context = new UniversityStoreEntities();
        public  static List<Department> GetAllDepartments()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q=from x in context.Departments
                  select x;

            List<Department> dep = q.ToList<Department>();
            return dep;
        }

        //////****************Zyj********************///////

        public static List<Department> getDepartmentsForCollectionPointId(string collectionPointId)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.Departments
                        where x.CollectionPointID == collectionPointId
                        select x;

            return query.ToList<Department>();
        }

        public static Employee getDepartmentRepDetails(string deptCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.Departments
                        where x.DepartmentCode == deptCode
                        select x;

            Department dept = query.First();
            //need to double check employee1 refers to deptrep again
            Employee deptRep = dept.Employee1;

            return deptRep;
        }




        public static string GetRepresentativeName(String emnum)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.Employees
                        where x.EmployeeNumber == emnum
                        select x;

            string e = query.First<Employee>().DepartmentCode;

            var q = from x in context.Departments
                    where x.DepartmentCode == e
                    select x;

            string e2 = q.First<Department>().RepresentativeEmpNo;

            var q2 = from x in context.Employees
                     where x.EmployeeNumber == e2
                     select x;
            string e3 = q2.First<Employee>().Name;

            return e3;
        }

        public static string GetCollectionPoint(String emnum)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();

            var query = from x in context.Employees
                        where x.EmployeeNumber == emnum
                        select x;
            string e = query.First<Employee>().DepartmentCode;
            var q = from x in context.Departments
                    where x.DepartmentCode == e
                    select x;
            string e2 = q.First<Department>().CollectionPointID;
            var q2 = from x in context.CollectionPoints
                     where x.CollectionPointID == e2
                     select x;
            string e3 = q2.First<CollectionPoint>().Name;
            return e3;

        }

        public static List<Object> getAllEmployeesOfThisHead(String depH)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.Employees
                        where x.EmployeeNumber == depH
                        select x;
            string e = query.First<Employee>().DepartmentCode;

            var query2 = from x in context.Employees
                         where x.DepartmentCode == e
                         select new { x.EmployeeNumber, x.Name };

            return query2.ToList<Object>();
        }

        public static List<CollectionPoint> getAllCollectionPoins()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();

            var q = from x in context.CollectionPoints
                    select x;
            List<CollectionPoint> copoint = q.ToList<CollectionPoint>();
            return copoint;
        }

        //////****************Zyj********************///////

        //////****************Sreeja********************///////
        public static void setCollectionPt(string collecpt, string deph)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();

            var q = from x in context.Employees
                    where x.EmployeeNumber == deph
                    select x;
            string depcode = q.First<Employee>().DepartmentCode;

            var q2 = from x in context.Departments
                     where x.DepartmentCode == depcode
                     select x;

            Department dep = q2.First<Department>();
            dep.CollectionPointID = collecpt;
            context.SaveChanges();


        }


        public static void setRepname(string repno, string deph)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.Employees
                    where x.EmployeeNumber == deph
                    select x;
            string depcode = q.First<Employee>().DepartmentCode;

            var q2 = from x in context.Departments
                     where x.DepartmentCode == depcode
                     select x;

            Department dep = q2.First<Department>();
            dep.RepresentativeEmpNo = repno;
            context.SaveChanges();

        }
        //////****************Sreeja********************///////



        /** added by ashwin 
         * */
        public static Department getdepartment(string deptCode)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.Departments
                    where x.DepartmentCode == deptCode
                    select x;
            Department dept = q.First<Department>();
            return dept;


        }
        public static void updatecollectiondetails(Department dep, string s2, string s3)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.Departments
                    where x.DepartmentCode == dep.DepartmentCode
                    select x;
            Department d = q.First<Department>();
            d.CollectionPointID = s2;
            d.RepresentativeEmpNo = s3;
            context.SaveChanges();

        }
        public static Department getdepartmentfromempid(string empid)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.Employees
                    where x.EmployeeNumber == empid
                    select x;
            Employee emp = q.First<Employee>();

            var z = from y in context.Departments
                    where y.DepartmentCode == emp.DepartmentCode
                    select y;
            Department dep = z.First<Department>();
            return dep;

        }

        /** end by ashwin
         * */
    }
}
