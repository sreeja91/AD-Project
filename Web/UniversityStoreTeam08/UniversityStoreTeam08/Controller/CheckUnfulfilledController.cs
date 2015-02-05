using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityStoreTeam08
{
    public class CheckUnfulfilledController
    {

        public static List<Object> GetUnfulfilledInfo(String deptHeadNumber)
        {

            return UnfulfilledDAO.GetUnfulfilledInfo(deptHeadNumber);
        
        }

    }
}