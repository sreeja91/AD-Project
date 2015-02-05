using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class EditPickUpController
    {
        public static string GetRepresentativeName(String de)
        {
            return DepartmentDAO.GetRepresentativeName(de);
        }

        public static string GetCollectionPoint(String den)
        {
            return DepartmentDAO.GetCollectionPoint(den);
        }
        public static List<Object> getAllEmployeesOfThisHead(String depH)
        {
            return DepartmentDAO.getAllEmployeesOfThisHead(depH);
        }
        public static List<CollectionPoint> getAllCollectionPoins()
        {
            return DepartmentDAO.getAllCollectionPoins();
        }

        //////****************Sreeja********************///////
        public static void setCollectionPt(string collecpt, string deph)
        {
            DepartmentDAO.setCollectionPt(collecpt, deph);

        }

        public static void setRepname(string repno, string deph)
        {
            DepartmentDAO.setRepname(repno, deph);
        }
        //////****************Sreeja********************///////
    }
}
