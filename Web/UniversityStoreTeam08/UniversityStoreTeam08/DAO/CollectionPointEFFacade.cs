using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class CollectionPointEFFacade
    {

        public static List<CollectionPoint> getCollectionPoint()
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var query = from x in context.CollectionPoints
                        select x;
            List<CollectionPoint> list = query.ToList<CollectionPoint>();

            return list;
        }

        /** added by ashwin 
         * */
        public static List<Employee> getallemployees(String deptc)
        {
            List<Employee> emp = EmployeeDAO.getallemployees(deptc);
            return emp;
        }

        public static CollectionPoint getcollectionpointfromname(String g)
        {
            UniversityStoreEntities context = new UniversityStoreEntities();
            var q = from x in context.CollectionPoints
                    where x.Name.Contains(g)
                    select x;
            CollectionPoint cp = q.First<CollectionPoint>();
            return cp;
        }

        /** end by ashwin
         * */
    }
}
