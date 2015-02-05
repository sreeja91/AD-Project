using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class RetreiveStationaryController
    {
       
        static  List<string> depList = new List<string>();

        public static void getAllDepts()
        {
            List<Object> olist2= RequisitionDAO.getAllDepInvolved();
            foreach (Object o in olist2)
            {
                Console.WriteLine(o.ToString().Substring(12,4));
                depList.Add(o.ToString().Substring(12,4));
            }
        }

 
        public static void generateDisbursementList()
        {
            Dictionary<string ,List<ConsolidatedRequisitionListDetail>> consolidatedDictionary = new Dictionary<string,List<ConsolidatedRequisitionListDetail>>();
            List<string> dlist = new List<string>();
            List<ConsolidatedRequisitionList> el;
            el = ConsolidatedRequisitionListEFFacade.getAllDepInvolved();
            foreach (ConsolidatedRequisitionList e in el)
            {
                dlist.Add(e.DepartmentCode);
            }


            List<string> deplist = RequisitionDAO.getAllReqDepartments();
            if (deplist.Count() != 0)
            {



                for (int p = 0; p < deplist.Count(); p++)
                {
                    if (dlist.Contains(deplist[p]))
                    {
                        ConsolidatedRequisitionList cl = ConsolidatedRequisitionListEFFacade.getOpenListForDept(deplist[p]);
                        int listid = cl.ConsolidatedListID;
                        List<string> itemlist = ConsolidatedRequisitionListEFFacade.getAllItemsOfList(listid);
                        List<Object> olist1 = RequisitionDAO.getAllApprovedOfDep(deplist[p]);

                        for (int i = 0; i < olist1.Count(); i++)
                        {
                            string itmeNum;
                            itmeNum = olist1[i].ToString().Substring(15, 4);
                            if (itemlist.Contains(itmeNum))
                            {
                                ConsolidatedRequisitionListEFFacade.addRequestedQtyForItem(listid, itmeNum, Convert.ToInt32(olist1[i].ToString().Substring(32, ((olist1[i].ToString().Length) - 34))));
                            }

                            else
                            {
                                ConsolidatedRequisitionListEFFacade.addDetailToList(listid, itmeNum, Convert.ToInt32(olist1[i].ToString().Substring(32, ((olist1[i].ToString().Length) - 34))), (RequisitionDAO.getTheFirstDatetDate(deplist[p])), 0);
                            }

                        }
                    }
                    if (dlist.Contains(deplist[p]) == false)
                    {

                        List<Object> olist = RequisitionDAO.getAllApprovedDetails(deplist[p]);
                        List<ConsolidatedRequisitionListDetail> crl = new List<ConsolidatedRequisitionListDetail>();
                        for (int i = 0; i < olist.Count(); i++)
                        {
                            ConsolidatedRequisitionListDetail c = new ConsolidatedRequisitionListDetail();
                            c.ItemNumber = olist[i].ToString().Substring(15, 4);
                            c.QuantityRequested = Convert.ToInt32(olist[i].ToString().Substring(32, ((olist[i].ToString().Length) - 34)));
                            c.DateRequest = RequisitionDAO.getTheFirstDatetDate(deplist[p]);
                            c.ActualQuantity = 0;
                            crl.Add(c);
                        }
                        consolidatedDictionary.Add(deplist[p], crl);

                    }
                }
                ConsolidatedRequisitionListEFFacade.createNewConsolidatedRequisitionList(consolidatedDictionary);
                RequisitionDAO.changeApprovedToClosed();


            }

        }

        public static void generateConsolidatedListFromUnfullfilled()
        {
            List<string> dlist = new List<string>();
            List<ConsolidatedRequisitionList> el;
            el = ConsolidatedRequisitionListEFFacade.getAllDepInvolved();
            foreach (ConsolidatedRequisitionList e in el)
            {
                dlist.Add(e.DepartmentCode);
            }
              

            List<string> deplist = UnfulfilledDAO.getAllUnfullfilledDepartments();
            Dictionary<string, List<ConsolidatedRequisitionListDetail>> consolidatedDictionary = new Dictionary<string, List<ConsolidatedRequisitionListDetail>>();
            if (deplist.Count() != 0)
            {
                


               for(int p=0; p< deplist.Count();p++)
               {
                   if (dlist.Contains(deplist[p]))
                   {
                       ConsolidatedRequisitionList cl = ConsolidatedRequisitionListEFFacade.getOpenListForDept(deplist[p]);
                       int listid = cl.ConsolidatedListID;
                       List<string> itemlist = ConsolidatedRequisitionListEFFacade.getAllItemsOfList(listid);
                       List<Object> olist1 = UnfulfilledDAO.getAllUnfuffilledOfDep(deplist[p]);
                       
                       for (int i = 0; i < olist1.Count(); i++)
                       {
                           string itmeNum;                           
                           itmeNum = olist1[i].ToString().Substring(15, 4);
                           if (itemlist.Contains(itmeNum))
                           {
                               ConsolidatedRequisitionListEFFacade.addRequestedQtyForItem(listid, itmeNum, Convert.ToInt32(olist1[i].ToString().Substring(32, ((olist1[i].ToString().Length) - 34))));
                           }

                           else
                           {
                               ConsolidatedRequisitionListEFFacade.addDetailToList(listid, itmeNum, Convert.ToInt32(olist1[i].ToString().Substring(32, ((olist1[i].ToString().Length) - 34))), (UnfulfilledDAO.getTheFirstDatetDate(deplist[p])), 0);
                           }                        
                       }
                   }

                   if(dlist.Contains(deplist[p])==false)
                   {
                       List<Object> olist2 = UnfulfilledDAO.getAllUnfuffilledOfDep(deplist[p]);

                   List<ConsolidatedRequisitionListDetail> crl = new List<ConsolidatedRequisitionListDetail>();
                   for (int i = 0; i < olist2.Count(); i++)
                   {
                       ConsolidatedRequisitionListDetail c = new ConsolidatedRequisitionListDetail();
                       c.ItemNumber = olist2[i].ToString().Substring(15, 4);
                       c.QuantityRequested = Convert.ToInt32(olist2[i].ToString().Substring(32, ((olist2[i].ToString().Length) - 34)));
                       c.DateRequest = UnfulfilledDAO.getTheFirstDatetDate(deplist[p]);
                       c.ActualQuantity = 0;
                       crl.Add(c);

                   }

                   consolidatedDictionary.Add(deplist[p], crl);
                   ConsolidatedRequisitionListEFFacade.createNewConsolidatedRequisitionList(consolidatedDictionary);


                   }
                   


               }
              
               UnfulfilledDAO.changeOpenToClosed();

            }
            


 
        }

        public static RetrievalForm getRetrivalData(string productNum)
        {
            RetrievalForm rf = new RetrievalForm();
            Product p = ProductDAO.getSearchProductbyid(productNum);
            rf.productDescription = p.Description.ToString();
            rf.availableQty = Convert.ToInt32(p.Stock.TotalInventoryBalance);
            rf.needeQty = ConsolidatedRequisitionListEFFacade.getNeededQty(productNum);
            List<string> depList =ConsolidatedRequisitionListEFFacade.getAllDepInvolved(productNum);
            List<int> qtyList = ConsolidatedRequisitionListEFFacade.getQtyNeedeOfDep(depList, productNum);
            rf.departments = depList;
            rf.depNeededQty = qtyList;
            List<int> tempDistList = new List<int>();
            int tempStock = rf.availableQty;

            for (int i = 0; i < depList.Count(); i++)
            {
                if (tempStock < rf.depNeededQty[i])
                {
                    tempDistList.Add(tempStock);
                    tempStock = 0;

                }

                if (tempStock > rf.depNeededQty[i])
                {
                    tempDistList.Add(rf.depNeededQty[i]);
                    tempStock = tempStock - rf.depNeededQty[i];
                }

            }
            rf.depAvailQty = tempDistList;

            return rf;


        }

        public static void updateStationaryCollection(string itemNum, int[] actualQty, string[] depCode)
        {
            List<string> itemList = new List<string>();
            List<Product> plist = ConsolidatedRequisitionListEFFacade.getAllProductsToCollect();
            foreach (Product p in plist)
            {
                itemList.Add(p.ItemNumber);
            }
            List<string> depCodeList = depCode.ToList<string>();
           
            List<int> qtyList = ConsolidatedRequisitionListEFFacade.getQtyNeedeOfDep(depCodeList, itemNum);

            if (itemList.Contains(itemNum))
            {
                for (int j = 0; j < actualQty.Length; j++)
                {

                    int i = ConsolidatedRequisitionListEFFacade.getDepOpenConsolidatedIDfromProd(itemNum, depCode[j]);
                    ConsolidatedRequisitionListEFFacade.updateStationaryCollection(i, itemNum, actualQty[j]);
                    int deff = qtyList[j] - actualQty[j];
                    if (deff > 0)
                    {
                        UnfulfilledDAO.raiseAdjustmentFromCollection(itemNum, depCode[j], deff);
 
                    }
                   
 
                }
                
            }








            int stockRed=0;
            for (int i = 0; i < actualQty.Length; i++)
            {
                stockRed = actualQty[i] + stockRed;
            }

            ProductDAO.UpdateStockReduce(itemNum, stockRed);
            

        }

        public static void finishCollection()
        {
            ConsolidatedRequisitionListEFFacade.finisheCollection();
        }

        /** added by lee
         * */

        public static List<ConsolidatedRequisitionListDetail> getConsolidatedItemsForOpenNoDuplicates()
        {
            List<ConsolidatedRequisitionList> consoList = ConsolidatedRequisitionListEFFacade.getAllOpenList();
            List<ConsolidatedRequisitionListDetail> detailList = new List<ConsolidatedRequisitionListDetail>();

            foreach (ConsolidatedRequisitionList consoReqList in consoList)
            {
                foreach (ConsolidatedRequisitionListDetail detail in consoReqList.ConsolidatedRequisitionListDetails)
                {
                    bool isduplicate = false;
                    foreach (ConsolidatedRequisitionListDetail finalDetail in detailList)
                    {
                        if (finalDetail.ItemNumber.Equals(detail.ItemNumber))
                            isduplicate = true;
                    }

                    if (!isduplicate)
                        detailList.Add(detail);
                }
            }

            return detailList;
        }


        /**end of lee
         * */



    }
}
