using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mshop.Models
{
    public class ReturnMessage
    {
        public bool status { get; set; }
        public string content { get; set; }
        public string id { get; set; }


    }
    public class CustomerInfo
    {
        public string Customerid { get; set; }
        [Required(ErrorMessage = "Enter Customer Name.")]
        public string CustomerName { get; set; }
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PhNo { get; set; }
    }
    public class EmployeeInfo
    {
        public string Employeeid { get; set; }
        [Required(ErrorMessage = "Enter Employee Name.")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Enter Position.")]
        public string Position { get; set; }
        [Required(ErrorMessage = "salary Required")]
        [DataType(DataType.Currency)]
        [Range(150000, 600000, ErrorMessage = "salary must between 150000 to 600000")]
        public Nullable<double> Salary { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage = "Enter Address.")]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string PhNo { get; set; }
        public byte[] ProfileImage { get; set; }
    }
    public class MobileModelInfo
    {
        public int Typeid { get; set; }
        public string Mobileid { get; set; }
        [Required(ErrorMessage = "Enter Mobile Model.")]
        public string Model { get; set; }
        public string Color { get; set; }
        [Required(ErrorMessage = "Price.")]
        [RegularExpression(@"^[\-\+]?(([0-9]+)([\.,]([0-9]+))?|([\.,]([0-9]+))?)$", ErrorMessage = "Please enter valid price")]
        public Nullable<double> Price { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Stock")]
        [Required]
        public Nullable<int> Stock { get; set; }
        public Nullable<int> Warranty { get; set; }
    }
    public class MobileTypeInfo
    {
        public string Mobileid { get; set; }
        [Required(ErrorMessage = "Enter Mobile Name.")]
        public string MobileName { get; set; }
    }
    public class ModalTempInfo
    {
        public int ACid { get; set; }
        [Required(ErrorMessage = " Brand Name.")]
        public string BrandName { get; set; }
        public string Brand { get; set; }

        [Required(ErrorMessage = "Accessories Type.")]
        public string Type { get; set; }
        public string TypeName { get; set; }
        [Required(ErrorMessage = "Model")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Fill Color")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Price")]
        [RegularExpression(@"^[\-\+]?(([0-9]+)([\.,]([0-9]+))?|([\.,]([0-9]+))?)$", ErrorMessage = "Please enter valid price")]
        public Nullable<double> Aprice { get; set; }
        [Required(ErrorMessage = "warranty")]
        public Nullable<double> warranty { get; set; }
        [Required(ErrorMessage = "Enter Total Amount .")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Amount")]

        public Nullable<double> Amount { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Number")]
        [Required]
        public Nullable<double> Aqty { get; set; }
    }
    public class MobileSaleInfo
    {
        public string Saleid { get; set; }

        public bool Check1 { get; set; }
        public bool Check2 { get; set; }
        public List<ModalTempInfo> Ac { get; set; }
        public string Asid { get; set; }
        public string Customerid { get; set; }
        [Required(ErrorMessage = "Enter Customer Name.")]
        public string CustomerName { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string PhNo { get; set; }
        public string Mobileid { get; set; }
        [Required(ErrorMessage = "Enter Mobile Name.")]
        public string Name { get; set; }
        public string MobileName { get; set; }
        public string Color { get; set; }
        public string Colour { get; set; }
        public string Model { get; set; }
        [Required(ErrorMessage = "Enter Mobile Model")]
        public int Typeid { get; set; }
        [Required(ErrorMessage = "Enter Price .")]
        public Nullable<double> Price { get; set; }
        [Required(ErrorMessage = "Enter Total Amount .")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Total Amount")]

        public Nullable<double> TotalAmount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Number")]
        [Required]
        public Nullable<double> Qty { get; set; }  
        [Required]
        public Nullable<double> Warranty { get; set; }
        [Required]
        public string Employeeid { get; set; }
        //AccSale
        public int Bid { get; set; }
        public string Sale { get; set; }

        public string Employee { get; set; }
        [Required(ErrorMessage = "Enter Customer Name.")]
        public string Customer { get; set; }
        public string Addr { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = " Brand Name")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Accessories Type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Accessories Model")]
        public string Mode { get; set; }
        [Required(ErrorMessage = "Choose Color")]
        public string Colo { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Price")]
        [Required]
        public Nullable<double> Pri { get; set; }


        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Number")]
        [Required]
        public Nullable<double> War { get; set; }

        public Nullable<double> Qt { get; set; }
        public Nullable<double> Total { get; set; }
    }

    public class AccSaleInfo
    {
        public int Bid { get; set; }
        public string Saleid { get; set; }
        public string Customerid { get; set; }
        public string Employeeid { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string PhNo { get; set; }
        public string Aid { get; set; }
        [Required(ErrorMessage = " Brand Name")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Accessories Type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Accessories Model")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Choose Color")]
        public string Color { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Price")]
        [Required]
        public Nullable<double> Price { get; set; }


        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Number")]
        [Required]
        public Nullable<double> Warranty { get; set; }

        public Nullable<double> Qty { get; set; }
        public Nullable<double> TotalAmount { get; set; }
    }
    public class ExtraInfo
    {
        public string Aid { get; set; }
        [Required(ErrorMessage = " Brand Name")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Accessories Type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Accessories Model")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Choose Color")]
        public string Color { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Price")]
        [Required]
        public Nullable<double> Price { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Stock")]
        [Required]
        public Nullable<double> Stock { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Number")]
        [Required]
        public Nullable<double> Warranty { get; set; }

    }

    public class RepairInfo
    {
        public string Rno { get; set; }
        public string Customerid { get; set; }
        [Required(ErrorMessage = "Enter Customer Name.")]
        public string CustomerName { get; set; }
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PhNo { get; set; }
        public string PhoneType { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string IsNo { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> RepairPayment { get; set; }
        public Nullable<double> Projection { get; set; }
        public Nullable<double> TotalAmount { get; set; }
        public string Rn { get; set; }
        public string Customer { get; set; }
        [Required(ErrorMessage = "Enter Customer Name.")]
        public string Name { get; set; }
        public string Add { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Ph { get; set; }
        public string Type { get; set; }
        public string Mod { get; set; }
        public string Col { get; set; }
        public string Des { get; set; }
        public string Is { get; set; }

        public Nullable<System.DateTime> Da { get; set; }
        public Nullable<double> Repair { get; set; }
        public Nullable<double> Project { get; set; }
        public Nullable<double> Total { get; set; }
        public string Saleid { get; set; }

       
       
        public string CName { get; set; }
        public string Caddress { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Phno { get; set; }
     
       
        public string MobileName { get; set; }
        
        public string Colour { get; set; }
        public string Mmodel { get; set; }
       
        public Nullable<double> Price { get; set; }
       
        public Nullable<double> Amount { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive Number")]
        [Required]
        public Nullable<double> Qty { get; set; }
        [Required]
        public Nullable<double> Warranty { get; set; }
        [Required]
        public string Employeeid { get; set; }
    }

    public class OrderInfo
    {
        public string Orderid { get; set; }
        [Required(ErrorMessage = "Enter Customer Name.")]
        public string Customername { get; set; }
        public string Oaddress { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Oph { get; set; }
        public string PhoneType { get; set; }
        public string PhoneModel { get; set; }
        public string PhoneColor { get; set; }
        public Nullable<System.DateTime> NowDate { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        [Required]
        public string Delivery { get; set; }
        public string Employeeid { get; set; }
    }
}