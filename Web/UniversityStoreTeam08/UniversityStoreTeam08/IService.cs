using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UniversityStoreTeam08
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/allcollectionpointlist", ResponseFormat = WebMessageFormat.Json)]
        CollectionPointWCF[] allCollectionPointList();

        [OperationContract]
        [WebGet(UriTemplate = "/deptforcollectionpoint/{collectionpointid}", ResponseFormat = WebMessageFormat.Json)]
        DepartmentWCF[] deptsForCollectionPoint(string collectionpointid);

        [OperationContract]
        [WebGet(UriTemplate = "/test", ResponseFormat = WebMessageFormat.Json)]
        bool test();

        [OperationContract]
        [WebGet(UriTemplate = "/Supplier", ResponseFormat = WebMessageFormat.Json)]
        SupplierWCF[] supplierlist();

        [OperationContract]
        [WebGet(UriTemplate = "/getallproducts", ResponseFormat = WebMessageFormat.Json)]
        ProductWCF[] getAllProducts();

        /***added by nitin
         * ***/
        [OperationContract]
        [WebGet(UriTemplate = "/getPendingReqLstForEmp/{empId}", ResponseFormat = WebMessageFormat.Json)]
        ReqList[] getPendingReqLstForEmp(string empID);

        [OperationContract]
        [WebGet(UriTemplate = "/getReqListDetails/{reqListId}", ResponseFormat = WebMessageFormat.Json)]
        ReqDetail[] getReqListDetails(string reqListId);

        [OperationContract]
        [WebGet(UriTemplate = "/submitApproval/{reqListId}", ResponseFormat = WebMessageFormat.Json)]
        void submitApproval(string reqListId);


        [OperationContract]
        [WebInvoke(UriTemplate = "/submitRejected", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void submitRejected(RejectStationeryRequestWCF reject);

        [OperationContract]
        [WebGet(UriTemplate = "/getProductFromName/{pname}", ResponseFormat = WebMessageFormat.Json)]
        ProductWCF getProductFromName(string pname);

        [OperationContract]
        [WebGet(UriTemplate = "/addToCart/{itemNum}/{qty}/{empNum}", ResponseFormat = WebMessageFormat.Json)]
        void addToCart(string itemNum, string qty, string empNum);

        [OperationContract]
        [WebGet(UriTemplate = "/viewCart/{empNum}", ResponseFormat = WebMessageFormat.Json)]
        CartWCF[] viewCart(string empNum);

        [OperationContract]
        [WebGet(UriTemplate = "/deteteItemFromCart/{itemNum}/{empNum}", ResponseFormat = WebMessageFormat.Json)]
        void deteteItemFromCart(string itemNum,string empNum);

        [OperationContract]
        [WebGet(UriTemplate = "/submitCart/{empNum}", ResponseFormat = WebMessageFormat.Json)]
        void submitCart(string empNum);

        [OperationContract]
        [WebGet(UriTemplate = "/getCartSize/{empNum}", ResponseFormat = WebMessageFormat.Json)]
        CartWCF getCartSize(string empNum);




        /***end
         * */

        /*** added by zhu 
         * **/
        [OperationContract]
        [WebGet(UriTemplate = "/getCurrentDelegateNAME/{empNum}", ResponseFormat = WebMessageFormat.Json)]
        List<CurrentDelegateWCF> getCurrentDelegate(string empNum);

        [OperationContract]
        [WebGet(UriTemplate = "/getEmployeeList/{empNum}", ResponseFormat = WebMessageFormat.Json)]
        List<DeEmployeeListWCF> getEmployeeList(string empNum);

        [OperationContract]
        [WebGet(UriTemplate = "/cancelDelegate/{empno}", RequestFormat = WebMessageFormat.Json)]
        void CancelDelegate(string empno);

        [OperationContract]
        [WebGet(UriTemplate = "/CreateDelegate/{empno}", RequestFormat = WebMessageFormat.Json)]
        void CreateDelegate(string empno);


        [OperationContract]
        [WebGet(UriTemplate = "/verifyUserName/{user}/{passwrd}", ResponseFormat = WebMessageFormat.Json)]
        LoginWCF verifyUserName(string user, string passwrd);


        /*end by  zhu
         * */


        /** added by ashwin
         * */

       

        [OperationContract]
        [WebGet(UriTemplate = "/getEmployeeDetails/{empNo}", ResponseFormat = WebMessageFormat.Json)]
        EmployeeNameWCF getEmployeeDetails(string empNo);

       

        //[OperationContract]
        //[WebGet(UriTemplate = "/getawaitingitemfordept/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
        //ProductWCF[] getAwaitingItemsForDept(string deptCode);

        [OperationContract]
        [WebGet(UriTemplate = "/purchaseorder/{SID}", ResponseFormat = WebMessageFormat.Json)]
        PurchaseOrderWCF[] purchaseorderlist(string SID);
        [OperationContract]
        [WebGet(UriTemplate = "/purchaseorderitem/{ponumb}", ResponseFormat = WebMessageFormat.Json)]
        PurchaseOrderDetailsWCF[] purchaseorderitem(string ponumb);
        [OperationContract]
        [WebGet(UriTemplate = "/approvepurchaseorder/{ponumb}", ResponseFormat = WebMessageFormat.Json)]
        ApproveResWCF[] changeapprovestatus(string ponumb);

        [OperationContract]
        [WebGet(UriTemplate = "/adjustmentvoucher/{role}", ResponseFormat = WebMessageFormat.Json)]
        AdjustmentVoucherWCF[] adjustmentvoucherlist(string role);
        [OperationContract]
        [WebGet(UriTemplate = "/adjustmentvouchermanager/{vouchernumber}", ResponseFormat = WebMessageFormat.Json)]
        AdjustmentVoucherDetailsWCF[] adjsutmentvoucheritemsmanager(string vouchernumber);
        [OperationContract]
        [WebGet(UriTemplate = "/adjustmentvouchersupervisor/{vouchernumber}", ResponseFormat = WebMessageFormat.Json)]
        AdjustmentVoucherDetailsWCF[] adjsutmentvoucheritemssupervisor(string vouchernumber);
        [OperationContract]
        [WebGet(UriTemplate = "/adjustmentvoucherupdatestatus/{voucherno}", ResponseFormat = WebMessageFormat.Json)]
        AdjustmentVoucherStatusUpdateWCF updatestatusadjustmentvoucher(string voucherno);
        [OperationContract]
        [WebGet(UriTemplate = "/getallemployees/{deptcode}", ResponseFormat = WebMessageFormat.Json)]
        EmployeeWCF[] getallemployees(string deptcode);
        [OperationContract]
        [WebGet(UriTemplate = "/getdepartmentdetails/{deptcode}", ResponseFormat = WebMessageFormat.Json)]
        DepartmentWCF getdepartmentdetails(string deptcode);

        [OperationContract]
        [WebGet(UriTemplate = "/updatedepartmentcollection/{deptcode}/{colname}/{empname}", ResponseFormat = WebMessageFormat.Json)]
        UpdateCollectionPointWCF updatecollectionpoint(string deptcode, string colname, string empname);

        [OperationContract]
        [WebGet(UriTemplate = "/getdepartmentfromempid/{empid}", ResponseFormat = WebMessageFormat.Json)]
        DepartmentWCF getdepartmentfromempid(string empid);

        [OperationContract]
        [WebGet(UriTemplate = "/adjustmentvoucherupdatestatussuper/{voucherno}", ResponseFormat = WebMessageFormat.Json)]
        AdjustmentVoucherStatusUpdateWCF updatestatusadjustmentvouchersuper(string voucherno);

        /** end by ashwin
         * */

        /** added by lee
         * */



        [OperationContract]
        [WebInvoke(UriTemplate = "/checkrepauthorisation", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        AuthResult[] checkRepAuthorisation(EmployeeCredentials empCredetials);

        [OperationContract]
        [WebGet(UriTemplate = "/getawaitingitemfordept/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
        ProductWCF[] getAwaitingItemsForDept(string deptCode);

        [OperationContract]
        [WebInvoke(UriTemplate = "/acknowledgecollectitems", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        AuthResult[] acknowledgecollectitems(ProductWCF[] itemsList);

        [OperationContract]
        [WebGet(UriTemplate = "/searchproduct/{searchTerm}", ResponseFormat = WebMessageFormat.Json)]
        ProductWCF[] searchProduct(string searchTerm);

        [OperationContract]
        [WebInvoke(UriTemplate = "/newadjustment", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        AuthResult[] newAdjustment(AdjustmentItemWCF[] list);

        [OperationContract]
        [WebGet(UriTemplate = "/getallconsoawaiting", ResponseFormat = WebMessageFormat.Json)]
        ProductWCF[] getAllConsoOpen();

        [OperationContract]
        [WebGet(UriTemplate = "/getretrievalform/{itemnumber}", ResponseFormat = WebMessageFormat.Json)]
        RetrievalFormWCF getRetrievalForm(string itemnumber);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updateactualqtyfordeptproduct", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void updateActualQtyForDeptProduct(UpdateActualQtyWCF qtyItem);

        [OperationContract]
        [WebGet(UriTemplate = "/finishcollection", ResponseFormat = WebMessageFormat.Json)]
        bool finishCollection();



        /**end by lee
         * */

        
    }

    /***added by nitin
        ***/
    [DataContract]
    public class CartWCF
    {
        string itemUnit;

        [DataMember]
        public string ItemUnit
        {
            get { return itemUnit; }
            set { itemUnit = value; }
        }


        string itemDes;

        [DataMember]
        public string ItemDes
        {
            get { return itemDes; }
            set { itemDes = value; }
        }


        string itemCode;

        [DataMember]
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        int qty;

        [DataMember]
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        string empNum;

        [DataMember]
        public string EmpNum
        {
            get { return empNum; }
            set { empNum = value; }
        }

        int cartSize;

        [DataMember]
        public int CartSize
        {
            get { return cartSize; }
            set { cartSize = value; }
        }

    }


    [DataContract]
    public class ReqDetail
    {
        int reqLNum;

         [DataMember]
        public int ReqLNum
        {
            get { return reqLNum; }
            set { reqLNum = value; }
        }
        string itemNum;

         [DataMember]
        public string ItemNum
        {
            get { return itemNum; }
            set { itemNum = value; }
        }

        
        private string itemName;

        [DataMember]
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        int qty;

        [DataMember]
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
    }




    [DataContract]
    public class ReqList 
    {
        int reqListNum;
        string empName;
        string dateReq;

        [DataMember]
        public string EmpName
        {
            get { return empName; }
            set { empName = value; }
        }

        [DataMember]
        public int ReqListNum
        {
            get { return reqListNum; }
            set { reqListNum = value; }
        }

        [DataMember]
        public string DateReq
        {
            get { return dateReq; }
            set { dateReq = value; }
        }
        
        
    }

    [DataContract]
    public class CollectionPointWCF 
    {
        string collectionPointID;
        string name;

        [DataMember]
        public string CollectionPointID
        {
            get { return collectionPointID; }
            set { collectionPointID = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    [DataContract]
    public class DepartmentWCF 
    {
        string departmentCode;
        string departmentName;
        string contactName;
        string phoneNo;
        string faxNo;
        string headEmployeeNumber;
        string collectionPointID;
        string representativeEmpNo;

        [DataMember]
        public string DepartmentCode
        {
            get { return departmentCode; }
            set { departmentCode = value; }
        }

        [DataMember]
        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }

        [DataMember]
        public string ContactName
        {
            get { return contactName; }
            set { contactName = value; }
        }

        [DataMember]
        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }

        [DataMember]
        public string FaxNo
        {
            get { return faxNo; }
            set { faxNo = value; }
        }

        [DataMember]
        public string HeadEmployeeNumber
        {
            get { return headEmployeeNumber; }
            set { headEmployeeNumber = value; }
        }

        [DataMember]
        public string CollectionPointID
        {
            get { return collectionPointID; }
            set { collectionPointID = value; }
        }

        [DataMember]
        public string RepresentativeEmpNo
        {
            get { return representativeEmpNo; }
            set { representativeEmpNo = value; }
        }
    }


      [DataContract]
    public class SupplierWCF
    {
        string supplierCode;
        string name;
        string contactName;
        string phoneNo;
        string faxNo;
        string address;
        string gstRegistrationNo;


    [DataMember]
        public string SupplierCode
        {
            get { return supplierCode; }
            set {supplierCode = value; }
        }
        
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string ContactName
        {
            get { return contactName; }
            set { contactName = value; }
        }
        [DataMember]
        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }

        [DataMember]
        public string FaxNo
        {
            get { return faxNo; }
            set { faxNo = value; }
        }
        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        [DataMember]
        public string GstRegistrationNo
        {
            get { return gstRegistrationNo; }
            set { gstRegistrationNo = value; }
        }

    }


      [DataContract]
      public class ProductWCF
      {
          private string itemNumber;
          private string category;
          private string description;
          private int reorderLevel;
          private int reorderQuantity;
          private string unitOfMeasure;
          private int bin;
          private string supplier1ID;
          private string supplier2ID;
          private string supplier3ID;
          private double adjustmentVoucherPrice;
          private int stock;

          //for consolidated list details properties
          private int consolidatedListId;
          private int quantityRequested;
          private int actualQuantityAccepted;

          [DataMember]
          public string ItemNumber
          {
              get { return itemNumber; }
              set { itemNumber = value; }
          }

          [DataMember]
          public string Category
          {
              get { return category; }
              set { category = value; }
          }

          [DataMember]
          public string Description
          {
              get { return description; }
              set { description = value; }
          }

          [DataMember]
          public int ReorderLevel
          {
              get { return reorderLevel; }
              set { reorderLevel = value; }
          }

          [DataMember]
          public int ReorderQuantity
          {
              get { return reorderQuantity; }
              set { reorderQuantity = value; }
          }

          [DataMember]
          public string UnitofMeasure
          {
              get { return unitOfMeasure; }
              set { unitOfMeasure = value; }
          }

          [DataMember]
          public int Bin
          {
              get { return bin; }
              set { bin = value; }
          }

          [DataMember]
          public string Supplier1ID
          {
              get { return supplier1ID; }
              set { supplier1ID = value; }
          }

          [DataMember]
          public string Supplier2ID
          {
              get { return supplier2ID; }
              set { supplier2ID = value; }
          }

          [DataMember]
          public string Supplier3ID
          {
              get { return supplier3ID; }
              set { supplier3ID = value; }
          }

          [DataMember]
          public double AdjustmentVoucherPrice
          {
              get { return adjustmentVoucherPrice; }
              set { adjustmentVoucherPrice = value; }
          }

          [DataMember]
          public int Stock
          {
              get { return stock; }
              set { stock = value; }
          }

          [DataMember]
          public int QuantityRequested
          {
              get { return quantityRequested; }
              set { quantityRequested = value; }
          }

          [DataMember]
          public int ConsolidatedListID
          {
              get { return consolidatedListId; }
              set { consolidatedListId = value; }
          }

          [DataMember]
          public int ActualQuantityAccepted
          {
              get { return actualQuantityAccepted; }
              set { actualQuantityAccepted = value; }
          }
      }



    /***** added by zhu
     * */


      [DataContract]
      public class CurrentDelegateWCF
      {
          private string name;
          private string id;

          [DataMember]
          public string Id
          {
              get { return id; }
              set { id = value; }
          }

          [DataMember]
          public string Name
          {
              get { return name; }
              set { name = value; }
          }
      }

      [DataContract]
      public class DeEmployeeListWCF
      {
          private string name;
          private string id;

          [DataMember]
          public string Id
          {
              get { return id; }
              set { id = value; }
          }

          [DataMember]
          public string Name
          {
              get { return name; }
              set { name = value; }
          }

      }

      [DataContract]
      public class LoginWCF
      {
          //private string userName;
          //private string password;
          private string role;
          private string empnumber;
          private string departmentID;
          private string status;


          [DataMember]
          public string Status
          {
              get { return status; }
              set { status = value; }
          }



          [DataMember]
          public string EmployeeNumber
          {
              get { return empnumber; }
              set { empnumber = value; }
          }

          [DataMember]
          public string Role
          {
              get { return role; }
              set { role = value; }
          }
          [DataMember]
          public string DepartmentID
          {
              get { return departmentID; }
              set { departmentID = value; }
          }
      }

      //[DataContract]
      //public class Login
      //{
      //    private bool successRe;

      //    [DataMember]
      //    public bool SuccessRe
      //    {
      //        get { return successRe; }
      //        set { successRe = value; }
      //    }
      //} 


    /*** end by zhu
     * */

    /*** added by ashwin
     * */
      [DataContract]
      public class ApproveResWCF
      {
          private bool successResult;

          [DataMember]
          public bool SuccessResult
          {
              get { return successResult; }
              set { successResult = value; }
          }
      }

     

             
      [DataContract]
      public class PurchaseOrderWCF
      {
          int poNumber;
          string supplierCode;
          string dateCreated;
          string status;

          [DataMember]
          public int PoNumber
          {
              get { return poNumber; }
              set { poNumber = value; }
          }

          [DataMember]
          public string SupplierCode
          {
              get { return supplierCode; }
              set { supplierCode = value; }
          }

          [DataMember]
          public string DateCreated
          {
              get { return dateCreated; }
              set { dateCreated = value; }
          }
          [DataMember]
          public string Status
          {
              get { return status; }
              set { status = value; }
          }
      }
      //Added for Purchase ORder Details
      [DataContract]
      public class PurchaseOrderDetailsWCF
      {
          int poNumber;
          string itemNumber;
          string description;
          int quantity;
          double price;


          [DataMember]
          public int PoNumber
          {
              get { return poNumber; }
              set { poNumber = value; }
          }

          [DataMember]
          public string ItemNumber
          {
              get { return itemNumber; }
              set { itemNumber = value; }
          }

          [DataMember]
          public string Description
          {
              get { return description; }
              set { description = value; }
          }
          [DataMember]
          public int Quantity
          {
              get { return quantity; }
              set { quantity = value; }
          }
          [DataMember]
          public double Price
          {
              get { return price; }
              set { price = value; }
          }
      }
      [DataContract]
      public class AdjustmentVoucherWCF
      {
          int voucherNumber;
          String date;
          [DataMember]
          public int VoucherNumber
          {
              get { return voucherNumber; }
              set { voucherNumber = value; }
          }
          [DataMember]
          public String Date
          {
              get { return date; }
              set { date = value; }
          }

      }
      [DataContract]
      public class AdjustmentVoucherDetailsWCF
      {
          int voucherNumber;
          string itemNumber;
          string status;
          int quantity;
          double totalPrice;
          string comment;
          string approvalStatus;

          [DataMember]
          public int VoucherNumber
          {
              get { return voucherNumber; }
              set { voucherNumber = value; }
          }

          [DataMember]
          public String Status
          {
              get { return status; }
              set { status = value; }
          }

          [DataMember]
          public int Quantity
          {
              get { return quantity; }
              set { quantity = value; }
          }

          [DataMember]
          public double TotalPrice
          {
              get { return totalPrice; }
              set { totalPrice = value; }
          }

          [DataMember]
          public string Comment
          {
              get { return comment; }
              set { comment = value; }
          }
          [DataMember]
          public string ApprovalStatus
          {
              get { return approvalStatus; }
              set { approvalStatus = value; }
          }
          [DataMember]
          public string ItemNumber
          {
              get { return itemNumber; }
              set { itemNumber = value; }
          }
      }
      [DataContract]
      public class AdjustmentVoucherStatusUpdateWCF
      {
          private bool successResult;

          [DataMember]
          public bool SuccessResult
          {
              get { return successResult; }
              set { successResult = value; }
          }


      }
      [DataContract]
      public class approveadjust
      {
          private String voucherNo;
          private String role;
          [DataMember]
          public String VoucherNo
          {
              get { return voucherNo; }
              set { voucherNo = value; }
          }
          [DataMember]
          public String Role
          {
              get
              {
                  return role;
              }
              set
              {
                  role = value;
              }
          }
      }


      [DataContract]
      public class EmployeeWCF
      {
          private string employeeNumber;
          private string departmentCode;
          private string email;
          private string userName;
          private string password;
          private string name;
          private string role;
          private string delagate;
          [DataMember]
          public string EmployeeNumber
          {
              get { return employeeNumber; }
              set { employeeNumber = value; }
          }

          [DataMember]
          public string DepartmentCode
          {
              get { return departmentCode; }
              set { departmentCode = value; }
          }

          [DataMember]
          public string Email
          {
              get { return email; }
              set { email = value; }
          }

          [DataMember]
          public string UserName
          {
              get { return userName; }
              set { userName = value; }
          }

          [DataMember]
          public string Password
          {
              get { return password; }
              set { password = value; }
          }

          [DataMember]
          public string Name
          {
              get { return name; }
              set { name = value; }
          }

          [DataMember]
          public string Role
          {
              get { return role; }
              set { role = value; }
          }

          [DataMember]
          public string Delagate
          {
              get { return delagate; }
              set { delagate = value; }
          }


      }


      [DataContract]
      public class EmployeeNameWCF
      {
          private string name;

          [DataMember]
          public string Name
          {
              get { return name; }
              set { name = value; }
          }

      }

      [DataContract]
      public class UpdateCollectionPointWCF
      {
          private bool successResult;
          [DataMember]
          public bool SuccessResult
          {
              get { return successResult; }
              set { successResult = value; }
          }
      }

      
      

    /** end by ashwin
     * */

    /** added by lee
     * */

      [DataContract]
      public class AuthResult
      {
          private bool successResult;

          [DataMember]
          public bool SuccessResult
          {
              get { return successResult; }
              set { successResult = value; }
          }
      }

      [DataContract]
      public class EmployeeCredentials
      {
          private string repUsername;
          private string repPassword;
          private string departmentCode;

          [DataMember]
          public string DepartmentCode
          {
              get { return departmentCode; }
              set { departmentCode = value; }
          }

          [DataMember]
          public string RepUsername
          {
              get { return repUsername; }
              set { repUsername = value; }
          }

          [DataMember]
          public string RepPassword
          {
              get { return repPassword; }
              set { repPassword = value; }
          }
      }
      [DataContract]
      public class AdjustmentItemWCF
      {
          private string itemNumber;
          private string status;
          private int quantity;
          private double totalPrice;
          private string comment;


          [DataMember]
          public string ItemNumber
          {
              get { return itemNumber; }
              set { itemNumber = value; }
          }

          [DataMember]
          public string Status
          {
              get { return status; }
              set { status = value; }
          }

          [DataMember]
          public int Quantity
          {
              get { return quantity; }
              set { quantity = value; }
          }

          [DataMember]
          public double TotalPrice
          {
              get { return totalPrice; }
              set { totalPrice = value; }
          }

          [DataMember]
          public string Comment
          {
              get { return comment; }
              set { comment = value; }
          }
      }

      [DataContract]
      public class RetrievalFormWCF
      {
          private int needeQty;
          private int availableQty;
          private string[] departments;
          private int[] depNeededQty;
          private int[] depAvailQt;

          [DataMember]
          public int NeededQty
          {
              get { return needeQty; }
              set { needeQty = value; }
          }

          [DataMember]
          public int AvailableQty
          {
              get { return availableQty; }
              set { availableQty = value; }
          }
          [DataMember]
          public string[] Departments
          {
              get { return departments; }
              set { departments = value; }
          }

          [DataMember]
          public int[] DepNeededQty
          {
              get { return depNeededQty; }
              set { depNeededQty = value; }
          }

          [DataMember]
          public int[] DepAvailQt
          {
              get { return depAvailQt; }
              set { depAvailQt = value; }
          }
      }

      [DataContract]
      public class UpdateActualQtyWCF
      {
          private string itemNumber;
          private string[] departments;
          private int[] availableQty;

          [DataMember]
          public string ItemNumber
          {
              get { return itemNumber; }
              set { itemNumber = value; }
          }

          [DataMember]
          public string[] Departments
          {
              get { return departments; }
              set { departments = value; }
          }

          [DataMember]
          public int[] AvailableQuantity
          {
              get { return availableQty; }
              set { availableQty = value; }
          }
      }

      [DataContract]
      public class RejectStationeryRequestWCF
      {
          private string requisitionListID;
          private string comment;

          [DataMember]
          public string RequisitionListID
          {
              get { return requisitionListID; }
              set { requisitionListID = value; }
          }

          [DataMember]
          public string Comment
          {
              get { return comment; }
              set { comment = value; }
          }

      }

    /** end by lee
     * */



}
