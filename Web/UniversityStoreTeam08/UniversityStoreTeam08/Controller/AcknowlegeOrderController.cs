using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class AcknowlegeOrderController
    {
        public static List<Object> getAllOd(int pn)
        {
            return AcknowlegeOrderDAO.getAllOrder(pn);
        }

        public static void  changeStatus(int pn)
        {
            AcknowlegeOrderDAO.changeToAcknowledge(pn);
        }

    }
}