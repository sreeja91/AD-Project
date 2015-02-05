using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UniversityStoreTeam08
{

    public class Service : IService
    {

        private string EMPLOYEE_ROLE_SUPERVISOR = "StoreSupervisor";
        private string EMPLOYEE_ROLE_MANAGER = "StoreManager";
        public ProductWCF[] getAllProducts()
        {
            List<Product> allProductsList = ProductDAO.selectAllProductList();
            List<ProductWCF> productWCFList = new List<ProductWCF>();
            foreach (Product p in allProductsList)
            {

                ProductWCF pwcf = new ProductWCF();
                pwcf.ItemNumber = p.ItemNumber;
                pwcf.Description = p.Description;
                pwcf.Category = p.Category;
                pwcf.ReorderLevel = p.ReorderLevel != null ? (int)p.ReorderLevel : 0;
                pwcf.ReorderQuantity = p.ReorderQuantity != null ? (int)p.ReorderQuantity : 0;
                pwcf.UnitofMeasure = p.UnitOfMeasure;
                pwcf.Bin = p.Bin != null ? (int)p.Bin : 0;
                pwcf.Supplier1ID = p.Supplier1ID;
                pwcf.Supplier2ID = p.Supplier2ID;
                pwcf.Supplier3ID = p.Supplier3ID;
                pwcf.AdjustmentVoucherPrice = p.AdjustmentVoucherPrice != null ? (double)p.AdjustmentVoucherPrice : 0.0;
                pwcf.Stock = p.Stock.TotalInventoryBalance != null ? (int)p.Stock.TotalInventoryBalance : 0;

                pwcf.ConsolidatedListID = 0;
                pwcf.ActualQuantityAccepted = 0;
                pwcf.QuantityRequested = 0;

                productWCFList.Add(pwcf);
            }

            return productWCFList.ToArray<ProductWCF>();
        }


        public bool test()
        {
            return true;
        }
        public CollectionPointWCF[] allCollectionPointList()
        {
            List<CollectionPointWCF> list = new List<CollectionPointWCF>();

            List<CollectionPoint> cpList = DeliverRequestController.getAllCollectionPoints();
            foreach (CollectionPoint cp in cpList)
            {
                CollectionPointWCF cpWCF = new CollectionPointWCF();
                cpWCF.CollectionPointID = cp.CollectionPointID;
                cpWCF.Name = cp.Name;
                list.Add(cpWCF);
            }

            return list.ToArray<CollectionPointWCF>();
        }

        public DepartmentWCF[] deptsForCollectionPoint(string collectionpointid)
        {
            List<Department> deptList = DeliverRequestController.getAllDeptForCollectionPointID(collectionpointid);

            List<DepartmentWCF> depList = new List<DepartmentWCF>();

            foreach (Department dept in deptList)
            {
                DepartmentWCF deptWCF = new DepartmentWCF();
                deptWCF.DepartmentCode = dept.DepartmentCode;
                deptWCF.DepartmentName = dept.DepatmentName;
                deptWCF.ContactName = dept.ContactName;
                deptWCF.PhoneNo = dept.PhoneNo;
                deptWCF.FaxNo = dept.FaxNo;
                deptWCF.HeadEmployeeNumber = dept.HeadsEmployeeNumber;
                deptWCF.CollectionPointID = dept.CollectionPointID;
                deptWCF.RepresentativeEmpNo = dept.RepresentativeEmpNo;

                depList.Add(deptWCF);
            }

            return depList.ToArray<DepartmentWCF>();
        }

        public SupplierWCF[] supplierlist()
        {
            List<Supplier> supplist = SupplierDAO.getSuppliers();
            List<SupplierWCF> sup = new List<SupplierWCF>();

            foreach (Supplier sw in supplist)
            {
                SupplierWCF supwcf = new SupplierWCF();
                supwcf.SupplierCode = sw.SupplierCode;
                supwcf.Name = sw.Name;
                supwcf.ContactName = sw.ContactName;
                supwcf.PhoneNo = sw.PhoneNo;
                supwcf.FaxNo = sw.FaxNo;
                supwcf.Address = sw.Address;
                supwcf.GstRegistrationNo = sw.GSTRegistrationNo;

                sup.Add(supwcf);



            }
            return sup.ToArray<SupplierWCF>();
        }

        /*** added by nitin
         *****/

        public static void addItemToCart(string itemNum, int qty, string empNum)  ///for WCF
        {
            CartDAO.addToCart(itemNum, qty, empNum);
        }

        public static List<RequestStationaryCart> viewCartW(string empNumber)
        {
            return CartDAO.viewCartWCF(empNumber);

        }

        //public static int getCartSize(string empNum)
        //{
        //    return CartDAO.getCartSize(empNum);
        //}


        public CartWCF getCartSize(string empNum)
        {
            CartWCF c = new CartWCF();
            c.CartSize = RequestStationaryControler.getCartSize(empNum);
            return c;

        }

        public ProductWCF getProductFromName(string pname)
        {
            List<ProductWCF> pppl = new List<ProductWCF>();
            List<Product> plist = new List<Product>();
            plist.Add(ProductDAO.gethProductbyName(pname));
            foreach (Product p in plist)
            {
                ProductWCF pwcf = new ProductWCF();
                pwcf.ItemNumber = p.ItemNumber;
                pwcf.Description = p.Description;
                pwcf.Category = p.Category;
                pwcf.ReorderLevel = p.ReorderLevel != null ? (int)p.ReorderLevel : 0;
                pwcf.ReorderQuantity = p.ReorderQuantity != null ? (int)p.ReorderQuantity : 0;
                pwcf.UnitofMeasure = p.UnitOfMeasure;
                pwcf.Bin = p.Bin != null ? (int)p.Bin : 0;
                pwcf.Supplier1ID = p.Supplier1ID;
                pwcf.Supplier2ID = p.Supplier2ID;
                pwcf.Supplier3ID = p.Supplier3ID;
                pwcf.AdjustmentVoucherPrice = p.AdjustmentVoucherPrice != null ? (double)p.AdjustmentVoucherPrice : 0.0;
                pwcf.Stock = p.Stock.TotalInventoryBalance != null ? (int)p.Stock.TotalInventoryBalance : 0;

                pwcf.ConsolidatedListID = 0;
                pwcf.ActualQuantityAccepted = 0;
                pwcf.QuantityRequested = 0;

                pppl.Add(pwcf);
            }
            return pppl[0];


        }

        public ReqList[] getPendingReqLstForEmp(string empId)
        {
            List<RequisitionList> rl = new List<RequisitionList>();
            List<ReqList> rql = new List<ReqList>();
            rl = ApproveRequestController.getAllPendingReq(empId);
            foreach (RequisitionList r in rl)
            {
                ReqList rr = new ReqList();
                rr.ReqListNum = r.RequisitionListNumber;
                rr.EmpName = r.Employee.Name;
                rr.DateReq = (r.DateCreated).ToString();
                rql.Add(rr);

            }
            return rql.ToArray<ReqList>();
        }

        public ReqDetail[] getReqListDetails(string reqListId)
        {
            List<RequisitionDetail> rd = new List<RequisitionDetail>();
            List<ReqDetail> rrd = new List<ReqDetail>();
            rd = ApproveRequestController.reqDetailsW(reqListId);
            foreach (RequisitionDetail r in rd)
            {
                ReqDetail rr = new ReqDetail();
                rr.ReqLNum = r.RequisitionListNumber;
                rr.ItemName = r.Product.Description;
                rr.Qty = Convert.ToInt32(r.Quantity);
                rrd.Add(rr);

            }
            return rrd.ToArray<ReqDetail>();


        }

        public void submitApproval(string reqListId)
        {
            ApproveRequestController.submitApproval(reqListId,"mobile");


        }

        public void submitRejected(RejectStationeryRequestWCF reject)
        {
            ApproveRequestController.submitRejected(reject.RequisitionListID, reject.Comment);
        }


        public void addToCart(string itemNum, string qty, string empNum)
        {
            int qt = Convert.ToInt32(qty);
            RequestStationaryControler.addItemToCart(itemNum, qt, empNum);
        }


        public CartWCF[] viewCart(string empNum)
        {
            List<RequestStationaryCart> rlist = new List<RequestStationaryCart>();
            List<CartWCF> clist = new List<CartWCF>();

            rlist = RequestStationaryControler.viewCartW(empNum);
            foreach (RequestStationaryCart i in rlist)
            {
                CartWCF cf = new CartWCF();
                cf.ItemCode = i.ItemCode;
                cf.Qty = i.Quantity;
                cf.EmpNum = i.EmployeeNumber;
                cf.ItemDes = ProductDAO.getSearchProductbyid(i.ItemCode).Description.ToString();
                cf.ItemUnit = ProductDAO.getSearchProductbyid(i.ItemCode).UnitOfMeasure.ToString();
                clist.Add(cf);

            }

            return clist.ToArray<CartWCF>();

        }

        public void deteteItemFromCart(string itemNum, string empNum)
        {
            RequestStationaryControler.deleteSelectedFromCart(itemNum, empNum);

        }

        public void submitCart(string empNum)
        {
            RequestStationaryControler.submitCart(empNum);
        }


        /*** added by zhu
         * */

        public LoginWCF verifyUserName(string user, string pass)
        {
            //            bool result = EmployeeDAO.verifyUserName(user,pass);
            bool result = LoginController.verifyUserName(user, pass);
            LoginWCF dele = new LoginWCF();
            if (result == true)
            {
                Employee e = LoginController.getUser(user);
                
               
                   
                    dele.Status = "true";
                    dele.Role = e.Role;
                    dele.EmployeeNumber = e.EmployeeNumber;
                    dele.DepartmentID = e.DepartmentCode;


                    return dele;
            }
            else
            {
                 dele.Status="false";
                 return dele;
            }

        }


        public List<CurrentDelegateWCF> getCurrentDelegate(string empNum)
        {

            List<Employee> emplist = DelegateAuthorityController.getCurrentDelegate(empNum);

            List<CurrentDelegateWCF> cuWCF = new List<CurrentDelegateWCF>();
            foreach (Employee e in emplist)
            {
                CurrentDelegateWCF dele = new CurrentDelegateWCF();
                dele.Name = e.Name;
                dele.Id = e.EmployeeNumber;
                cuWCF.Add(dele);
            }
            return cuWCF;
        }

        public void CancelDelegate(string empno)
        {
            DelegateAuthorityController.RemoveDelegate(empno);
        }

        public void CreateDelegate(string empno)
        {
            DelegateAuthorityController.createDelegate(empno);
        }

        public List<DeEmployeeListWCF> getEmployeeList(string empNum)
        {
            //List<Object> emplist = EmployeeDAO.getEmployeeList(empNum);
            //List<DeEmployeeListWCF> empli = new List<DeEmployeeListWCF>();
            //foreach (Employee e in emplist)
            //{
            //    DeEmployeeListWCF emname = new DeEmployeeListWCF();
            //    emname.Name = e.Name;
            //    emname.Id = e.EmployeeNumber;
            //    empli.Add(emname);
            //}
            //return empli;
            List<Employee> emplist = DelegateAuthorityController.getEmployee(empNum);
            List<DeEmployeeListWCF> empli = new List<DeEmployeeListWCF>();
            foreach (Employee e in emplist)
            {
                DeEmployeeListWCF emname = new DeEmployeeListWCF();
                emname.Name = e.Name;
                emname.Id = e.EmployeeNumber;
                empli.Add(emname);
            }
            return empli;
        }

        /** end by zhu
         * */

        /*** added by ashwin
       * */
        public DepartmentWCF getdepartmentdetails(String deptCode)
        {
            Department dept = DeliverRequestController.getdepartmentdetails(deptCode);
            DepartmentWCF depwcf = new DepartmentWCF();
            depwcf.CollectionPointID = dept.CollectionPointID;
            depwcf.ContactName = dept.ContactName;
            depwcf.DepartmentCode = dept.DepartmentCode;
            depwcf.FaxNo = dept.FaxNo;
            depwcf.HeadEmployeeNumber = dept.HeadsEmployeeNumber;
            depwcf.PhoneNo = dept.PhoneNo;
            depwcf.RepresentativeEmpNo = dept.RepresentativeEmpNo;
            depwcf.DepartmentName = dept.DepatmentName;
            return depwcf;
        }


       
        //||||||||||||||||
        public PurchaseOrderWCF[] purchaseorderlist(string SID)
        {
            List<PurchaseOrder> po = PurchaseOrderController.getsupplies(SID);
            List<PurchaseOrderWCF> ponew = new List<PurchaseOrderWCF>();
            foreach (PurchaseOrder p in po)
            {
                PurchaseOrderWCF powcf = new PurchaseOrderWCF();
                powcf.PoNumber = p.PONumber;
                powcf.SupplierCode = p.SupplierCode;
                powcf.DateCreated = Convert.ToString(p.DateCreated);
                powcf.Status = p.Status;
                ponew.Add(powcf);

            }
            return ponew.ToArray<PurchaseOrderWCF>();

        }

        //|||||||||||||||||||||||||||||
        public PurchaseOrderDetailsWCF[] purchaseorderitem(string ponumb)
        {
            int ponumber = Convert.ToInt32(ponumb);
            List<PurchaseOrderDetail> pod = PurchaseOrderController.AcceptSupply(ponumber);
            List<PurchaseOrderDetailsWCF> podnew = new List<PurchaseOrderDetailsWCF>();
            foreach (PurchaseOrderDetail po in pod)
            {
                PurchaseOrderDetailsWCF podwcf = new PurchaseOrderDetailsWCF();
                podwcf.PoNumber = po.PONumber;
                podwcf.ItemNumber = po.ItemNumber;
                podwcf.Description = po.Description;
                podwcf.Quantity = Convert.ToInt32(po.Quantity);
                podwcf.Price = Convert.ToDouble(po.Price);
                podnew.Add(podwcf);
            }
            return podnew.ToArray<PurchaseOrderDetailsWCF>();
        }

        public EmployeeNameWCF getEmployeeDetails(string empNo)
        {
            Employee emp = DeliverRequestController.getEmployee(empNo);
            EmployeeNameWCF name = new EmployeeNameWCF();
            name.Name = emp.Name;

            return name;
        }

        //||||||||||
        //||||||||||ASH
        //||||||||||
        public UpdateCollectionPointWCF updatecollectionpoint(string s1, string s2, string s3)
        {
            Department dep = DeliverRequestController.getdepartmentdetails(s1);
          //  CollectionPoint cp = DeliverRequestController.getcollectionpoint(s2);
            //Employee emp = DeliverRequestController.getEmployeefromname(s3);
            DeliverRequestController.updatedepartmentcollection(dep, s2, s3);
            UpdateCollectionPointWCF upwcf = new UpdateCollectionPointWCF();
            upwcf.SuccessResult = true;
            return upwcf;


        }
        //||||||||
        //||||||||ASH
        //||||||||
        public DepartmentWCF getdepartmentfromempid(string empid)
        {
            Department dep = DeliverRequestController.getdepartmentfromempid(empid);
            DepartmentWCF depwcf = new DepartmentWCF();
            depwcf.DepartmentCode = dep.DepartmentCode;
            depwcf.DepartmentName = dep.DepatmentName;
            depwcf.ContactName = dep.ContactName;
            depwcf.CollectionPointID = dep.CollectionPointID;
            depwcf.FaxNo = dep.FaxNo;
            depwcf.HeadEmployeeNumber = dep.HeadsEmployeeNumber;
            depwcf.PhoneNo = dep.PhoneNo;
            depwcf.RepresentativeEmpNo = dep.RepresentativeEmpNo;
            return depwcf;
        }


        public EmployeeWCF[] getallemployees(String deptcode)
        {
            List<Employee> emp = CollectionPointEFFacade.getallemployees(deptcode);
            List<EmployeeWCF> empwcf = new List<EmployeeWCF>();
            foreach (Employee e in emp)
            {
                EmployeeWCF empwcfnew = new EmployeeWCF();
                empwcfnew.EmployeeNumber = e.EmployeeNumber;
                empwcfnew.DepartmentCode = e.DepartmentCode;
                empwcfnew.Email = e.Email;
                empwcfnew.DepartmentCode = e.DepartmentCode;
                empwcfnew.Delagate = e.Delagate;
                empwcfnew.Name = e.Name;
                empwcfnew.Role = e.Role;
                empwcfnew.UserName = e.UserName;
                empwcf.Add(empwcfnew);

            }
            return empwcf.ToArray<EmployeeWCF>();
        }

       
        //to Approve Issue adjustment voucher
        public AdjustmentVoucherStatusUpdateWCF updatestatusadjustmentvoucher(String voucherno)
        {
            int voucher = Convert.ToInt32(voucherno);
            List<AdjustmentVoucherDetail> adjvouch = adjustmentVoucherDAO.getItemsForManagerApproval(voucher);
           // IssueAdjustmentVoucherController.approveitems(adjvouch);
            adjustmentVoucherDAO.approveVoucherDetailItemsByManager(adjvouch);
            AdjustmentVoucherStatusUpdateWCF adwcf = new AdjustmentVoucherStatusUpdateWCF();
            adwcf.SuccessResult = true;
            return adwcf;

        }
        public AdjustmentVoucherStatusUpdateWCF updatestatusadjustmentvouchersuper(String voucherno)
        {
            int voucher = Convert.ToInt32(voucherno);
            List<AdjustmentVoucherDetail> adjvouch = adjustmentVoucherDAO.getItemsForSupervisorApproval(voucher);
           // IssueAdjustmentVoucherController.approveitems(adjvouch);
            adjustmentVoucherDAO.approveVoucherDetailItemsBySupervisor(adjvouch);
            AdjustmentVoucherStatusUpdateWCF adwcf = new AdjustmentVoucherStatusUpdateWCF();
            adwcf.SuccessResult = true;
            return adwcf;

        }
        //To get the list voucher number pending adjustment
        public AdjustmentVoucherWCF[] adjustmentvoucherlist(String role)
        {
            List<AdjustmentVoucher> adjustvouch = 
            AdjustmentVoucherEFFacade.CheckEmployeeRole(role); 
            List<AdjustmentVoucherWCF> adjustvouchnew = new List<AdjustmentVoucherWCF>();
            foreach (AdjustmentVoucher ad in adjustvouch)
            {
                AdjustmentVoucherWCF a = new AdjustmentVoucherWCF();
                a.VoucherNumber = ad.VoucherNumber;
                a.Date = Convert.ToString(ad.DateCreated);
                adjustvouchnew.Add(a);

            }
            return adjustvouchnew.ToArray<AdjustmentVoucherWCF>();
        }
        //list of itmes in the vouchers
        public AdjustmentVoucherDetailsWCF[] adjsutmentvoucheritemsmanager(String vouchernumber)
        {
            int voucher = Convert.ToInt32(vouchernumber);
            List<AdjustmentVoucherDetail> adjustvouchdet = AdjustmentVoucherEFFacade.getItemsForManagerApproval(voucher);
            List<AdjustmentVoucherDetailsWCF> adjustvouchdetnew = new List<AdjustmentVoucherDetailsWCF>();

            foreach (AdjustmentVoucherDetail ad in adjustvouchdet)
            {
                AdjustmentVoucherDetailsWCF adju = new AdjustmentVoucherDetailsWCF();
                adju.VoucherNumber = ad.VoucherNumber;
                adju.Status = ad.Status;
                adju.Comment = ad.Comment;
                adju.Quantity = Convert.ToInt32(ad.Quantity);
                adju.TotalPrice = Convert.ToDouble(ad.TotalPrice);
                adju.ApprovalStatus = ad.ApprovalStatus;
                adju.ItemNumber = ad.ItemNumber;
                adjustvouchdetnew.Add(adju);
            }
            return adjustvouchdetnew.ToArray<AdjustmentVoucherDetailsWCF>();
        }
        public AdjustmentVoucherDetailsWCF[] adjsutmentvoucheritemssupervisor(String vouchernumber)
        {
            int voucher = Convert.ToInt32(vouchernumber);
            List<AdjustmentVoucherDetail> adjustvouchdet = AdjustmentVoucherEFFacade.getItemsForSupervisorApproval(voucher);
            List<AdjustmentVoucherDetailsWCF> adjustvouchdetnew = new List<AdjustmentVoucherDetailsWCF>();

            foreach (AdjustmentVoucherDetail ad in adjustvouchdet)
            {
                AdjustmentVoucherDetailsWCF adju = new AdjustmentVoucherDetailsWCF();
                adju.VoucherNumber = ad.VoucherNumber;
                adju.Status = ad.Status;
                adju.Comment = ad.Comment;
                adju.Quantity = Convert.ToInt32(ad.Quantity);
                adju.TotalPrice = Convert.ToDouble(ad.TotalPrice);
                adju.ApprovalStatus = ad.ApprovalStatus;
                adju.ItemNumber = ad.ItemNumber;
                adjustvouchdetnew.Add(adju);
            }
            return adjustvouchdetnew.ToArray<AdjustmentVoucherDetailsWCF>();
        }

        //TO CHANGE APPROVE STATUS ACCEPT SUPPLY
        public ApproveResWCF[] changeapprovestatus(string ponumb)
        {
            int ponumber = Convert.ToInt32(ponumb);
            PurchaseOrderController.ChangeStatusToApproved(ponumber);
            ApproveResWCF apwcf = new ApproveResWCF();
            apwcf.SuccessResult = true;
            List<ApproveResWCF> apwcfnew = new List<ApproveResWCF>();
            apwcfnew.Add(apwcf);
            return apwcfnew.ToArray<ApproveResWCF>();

        }
       



        /** end by ashwin
         * */







        /** added by Lee
         * */


        public bool finishCollection()
        {
            RetreiveStationaryController.finishCollection();
            return true;
        }

        public void updateActualQtyForDeptProduct(UpdateActualQtyWCF qtyItem)
        {
            RetreiveStationaryController.updateStationaryCollection(qtyItem.ItemNumber, qtyItem.AvailableQuantity, qtyItem.Departments);
        }

        public RetrievalFormWCF getRetrievalForm(string itemnumber)
        {
            RetrievalForm rf = RetreiveStationaryController.getRetrivalData(itemnumber);
            RetrievalFormWCF rfwcf = new RetrievalFormWCF();

            rfwcf.NeededQty = rf.needeQty;
            rfwcf.AvailableQty = rf.availableQty;
            rfwcf.Departments = rf.departments.ToArray<string>();
            rfwcf.DepNeededQty = rf.depNeededQty.ToArray<int>();
            rfwcf.DepAvailQt = rf.depAvailQty.ToArray<int>();

            return rfwcf;
        }

        public ProductWCF[] getAllConsoOpen()
        {
            RetreiveStationaryController.generateDisbursementList();
            RetreiveStationaryController.generateConsolidatedListFromUnfullfilled();

            List<ConsolidatedRequisitionListDetail> list = RetreiveStationaryController.getConsolidatedItemsForOpenNoDuplicates();
            List<ProductWCF> resultsList = new List<ProductWCF>();

            foreach (ConsolidatedRequisitionListDetail detail in list)
            {
                Product p = detail.Product;
                ProductWCF pWCF = new ProductWCF();

                pWCF.ItemNumber = p.ItemNumber;
                pWCF.Description = p.Description;
                pWCF.Category = p.Category;
                pWCF.ReorderLevel = p.ReorderLevel != null ? (int)p.ReorderLevel : 0;
                pWCF.ReorderQuantity = p.ReorderQuantity != null ? (int)p.ReorderQuantity : 0;
                pWCF.UnitofMeasure = p.UnitOfMeasure;
                pWCF.Bin = p.Bin != null ? (int)p.Bin : 0;
                pWCF.Supplier1ID = p.Supplier1ID;
                pWCF.Supplier2ID = p.Supplier2ID;
                pWCF.Supplier3ID = p.Supplier3ID;
                pWCF.AdjustmentVoucherPrice = p.AdjustmentVoucherPrice != null ? (double)p.AdjustmentVoucherPrice : 0.0;
                pWCF.Stock = p.Stock.TotalInventoryBalance != null ? (int)p.Stock.TotalInventoryBalance : 0;

                pWCF.ConsolidatedListID = detail.ConsolidatedListID;
                pWCF.QuantityRequested = detail.QuantityRequested != null ? (int)detail.QuantityRequested : 0;
                pWCF.ActualQuantityAccepted = detail.ActualQuantity != null ? (int)detail.ActualQuantity : 0;

                resultsList.Add(pWCF);
            }
            return resultsList.ToArray<ProductWCF>();
        }

        public AuthResult[] newAdjustment(AdjustmentItemWCF[] listi)
        {
            MakeAdjustmentVoucherController.makeNewAdjustmentVoucher(listi);

            AuthResult ar = new AuthResult();
            ar.SuccessResult = true;

            List<AuthResult> list = new List<AuthResult>();
            list.Add(ar);

            return list.ToArray<AuthResult>();
        }

        public AuthResult[] checkRepAuthorisation(EmployeeCredentials empCredetials)
        {
            bool successResult = DeliverRequestController.checkAuthorisation(empCredetials.DepartmentCode, empCredetials.RepUsername,
                empCredetials.RepPassword);
            AuthResult ar = new AuthResult();
            ar.SuccessResult = successResult;

            List<AuthResult> list = new List<AuthResult>();
            list.Add(ar);

            return list.ToArray<AuthResult>();
        }


        public ProductWCF[] getAwaitingItemsForDept(string deptCode)
        {
            List<ConsolidatedRequisitionListDetail> detailList = DeliverRequestController.getAllAwaitingItemsForDept(deptCode);
            List<ProductWCF> pWCFList = new List<ProductWCF>();

            foreach (ConsolidatedRequisitionListDetail detail in detailList)
            {
                Product p = detail.Product;
                ProductWCF pWCF = new ProductWCF();

                pWCF.ItemNumber = p.ItemNumber;
                pWCF.Description = p.Description;
                pWCF.Category = p.Category;
                pWCF.ReorderLevel = p.ReorderLevel != null ? (int)p.ReorderLevel : 0;
                pWCF.ReorderQuantity = p.ReorderQuantity != null ? (int)p.ReorderQuantity : 0;
                pWCF.UnitofMeasure = p.UnitOfMeasure;
                pWCF.Bin = p.Bin != null ? (int)p.Bin : 0;
                pWCF.Supplier1ID = p.Supplier1ID;
                pWCF.Supplier2ID = p.Supplier2ID;
                pWCF.Supplier3ID = p.Supplier3ID;
                pWCF.AdjustmentVoucherPrice = p.AdjustmentVoucherPrice != null ? (double)p.AdjustmentVoucherPrice : 0.0;
                pWCF.Stock = p.Stock.TotalInventoryBalance != null ? (int)p.Stock.TotalInventoryBalance : 0;

                pWCF.ConsolidatedListID = detail.ConsolidatedListID;
                pWCF.QuantityRequested = detail.QuantityRequested != null ? (int)detail.QuantityRequested : 0;
                pWCF.ActualQuantityAccepted = detail.ActualQuantity != null ? (int)detail.ActualQuantity : 0;

                pWCFList.Add(pWCF);

            }
            return pWCFList.ToArray<ProductWCF>();
        }



        public AuthResult[] acknowledgecollectitems(ProductWCF[] itemsList)
        {
            List<ConsolidatedRequisitionListDetail> detailList = new List<ConsolidatedRequisitionListDetail>();

            foreach (ProductWCF p in itemsList)
            {
                ConsolidatedRequisitionListDetail detail = new ConsolidatedRequisitionListDetail();
                detail.ConsolidatedListID = p.ConsolidatedListID;
                detail.ItemNumber = p.ItemNumber;
                detail.QuantityRequested = p.QuantityRequested;
                detail.ActualQuantity = p.ActualQuantityAccepted;
                detail.DateRequest = DateTime.Now;

                detailList.Add(detail);
            }

            bool result = DeliverRequestController.acknowledgeCollectItems(detailList);
            List<AuthResult> resultList = new List<AuthResult>();
            AuthResult bo = new AuthResult();
            bo.SuccessResult = result;
            resultList.Add(bo);

            return resultList.ToArray<AuthResult>();
        }


          public ProductWCF[] searchProduct(string searchTerm)
        {
            List<Product> searchResults = new List<Product>();

            Product idSearchResult = null;

            try
            {
                idSearchResult = ProductDAO.getSearchProductbyid(searchTerm);
            }
            catch (InvalidOperationException ex)
            {
            }

            List<Product> nameSearchResults = ProductDAO.getSearchProductbyname(searchTerm);
            List<Product> categorySearchResults = ProductDAO.getSearchProductbyCategory(searchTerm);

            if (idSearchResult != null)
                searchResults.Add(idSearchResult);

            foreach (Product p in nameSearchResults)
            {
                //searchResults.Add(p);

                if (searchResults.Count > 0)
                {
                    bool isrepeat = false;

                    foreach (Product p1 in searchResults)
                    {
                        if (p.ItemNumber.Equals(p1.ItemNumber))
                            isrepeat = true;
                    }

                    if (!isrepeat)
                        searchResults.Add(p);
                }
                else
                {
                    searchResults.Add(p);
                }
            }

            foreach (Product p in categorySearchResults)
            {
                if (searchResults.Count > 0)
                {
                    bool isrepeat = false;
                    foreach (Product p1 in searchResults)
                    {
                        if (!p.ItemNumber.Equals(p1.ItemNumber))
                            isrepeat = true;
                    }

                    if (!isrepeat)
                        searchResults.Add(p);
                }
                else
                {
                    searchResults.Add(p);
                }
            }



            List<ProductWCF> searchResultWCF = new List<ProductWCF>();

            foreach (Product p in searchResults)
            {
                Console.WriteLine(p.ItemNumber);
                Console.WriteLine(p.Description);
                Console.WriteLine("------");

                ProductWCF pwcf = new ProductWCF();
                pwcf.ItemNumber = p.ItemNumber;
                pwcf.Description = p.Description;
                pwcf.Category = p.Category;
                pwcf.ReorderLevel = p.ReorderLevel != null ? (int)p.ReorderLevel : 0;
                pwcf.ReorderQuantity = p.ReorderQuantity != null ? (int)p.ReorderQuantity : 0;
                pwcf.UnitofMeasure = p.UnitOfMeasure;
                pwcf.Bin = p.Bin != null ? (int)p.Bin : 0;
                pwcf.Supplier1ID = p.Supplier1ID;
                pwcf.Supplier2ID = p.Supplier2ID;
                pwcf.Supplier3ID = p.Supplier3ID;
                pwcf.AdjustmentVoucherPrice = p.AdjustmentVoucherPrice != null ? (double)p.AdjustmentVoucherPrice : 0.0;
                pwcf.Stock = p.Stock.TotalInventoryBalance != null ? (int)p.Stock.TotalInventoryBalance : 0;

                pwcf.ConsolidatedListID = 0;
                pwcf.ActualQuantityAccepted = 0;
                pwcf.QuantityRequested = 0;

                searchResultWCF.Add(pwcf);
            }

            return searchResultWCF.ToArray<ProductWCF>();
        }
        /** end by lee
         * */

    }
}

        



