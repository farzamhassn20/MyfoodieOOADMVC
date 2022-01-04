using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRestaurant.Models;
using WebRestaurant.Repositories;
using WebRestaurant.ViewModel;

namespace WebRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantDbEntities objRestaurantDbEntities;
       
        public HomeController(){
            objRestaurantDbEntities = new RestaurantDbEntities();

        }

        // GET: Home


        public ActionResult Index()
        {
            CustomerRepository objCustomerRespository = new CustomerRepository();
            ItemRepository objItemRespository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRespository = new PaymentTypeRepository();
            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objCustomerRespository.GetAllCustomers(),objItemRespository.GetAllItems(),objPaymentTypeRespository.GetAllPaymentType());

            return View(objMultipleModels);
           
        }
        [HttpGet]
        public JsonResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = (decimal)objRestaurantDbEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(UnitPrice,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objorderRepository = new OrderRepository();
            objorderRepository.AddOrder(objOrderViewModel);
            return Json(data: "Your Order has been successfully Placed", JsonRequestBehavior.AllowGet);

        }


        public ActionResult Addmenu()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Addmenu(Item it)
        {
            if (ModelState.IsValid)
            {
                objRestaurantDbEntities.Items.Add(it);
                objRestaurantDbEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(it);

        }

        public ActionResult AddCustomer()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddCustomer(Customer cus)
        {
            if (ModelState.IsValid)
            {
                objRestaurantDbEntities.Customers.Add(cus);
                objRestaurantDbEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cus);

        }


        public ActionResult Showmenu()
        {
            return View(objRestaurantDbEntities.Items.ToList());
        }

        public ActionResult ShowOrderDetails()
        {
            return View(objRestaurantDbEntities.OrderDetails.ToList());
        }

        public ActionResult ShowTransactions()
        {
            return View(objRestaurantDbEntities.Transactions.ToList());
        }

        public ActionResult HomePage()
        {
            Total();
            return View();
        }

        public void Total()
        {
            ViewBag.CustomersCount = objRestaurantDbEntities.Customers.Count();
            ViewBag.OrdersCount = objRestaurantDbEntities.Orders.Count();
            ViewBag.ItemsCount = objRestaurantDbEntities.Items.Count();

        }
        public class OrderViewModel
        {
            public int OrderId { get; set; }
            public int PaymentTypeId { get; set; }
            public int CustomerId { get; set; }
            public string OrderNumber { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal FinalTotal { get; set; }

            public IEnumerable<OrderDetailViewModel> ListOfOrderDetailViewModel { get; set; }

        }

        public class OrderDetailViewModel
        {
            public int OrderDetailId { get; set; }
            public int ItemId { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Quantity { get; set; }
            public decimal Discount { get; set; }

            public decimal Total { get; set; }

 

        }
    }

    
}