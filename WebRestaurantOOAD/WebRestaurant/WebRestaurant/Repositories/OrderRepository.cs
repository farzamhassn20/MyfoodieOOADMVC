using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebRestaurant.Models;
using static WebRestaurant.Controllers.HomeController;

namespace WebRestaurant.Repositories
{
    public class OrderRepository { 
        private RestaurantDbEntities objRestaurantDbEntities;

    public OrderRepository()
        {
            objRestaurantDbEntities = new RestaurantDbEntities();

        }
        public bool AddOrder(OrderViewModel objOrderViewModel) {
            Order objOrder = new Order();
            objOrder.CustomerId = objOrderViewModel.CustomerId;
            objOrder.FinalTotal = objOrderViewModel.FinalTotal;
            objOrder.OrderDate = DateTime.Now;
            objOrder.OrderNumber = string.Format("{0:ddmmmyyyyhhmmss}", DateTime.Now);
            objOrder.PaymentTypeId = objOrderViewModel.PaymentTypeId;
            objRestaurantDbEntities.Orders.Add(objOrder);
            objRestaurantDbEntities.SaveChanges();
            int OrderId = objOrder.OrderId;

            foreach(var item in objOrderViewModel.ListOfOrderDetailViewModel)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Discount = item.Discount;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.Total = item.Total;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objRestaurantDbEntities.OrderDetails.Add(objOrderDetail);
                objRestaurantDbEntities.SaveChanges();


                Transaction objTransaction = new Transaction();
                objTransaction.Itemid = item.ItemId;
                objTransaction.Quantity = item.Quantity;
                objTransaction.TransactionDate = DateTime.Now;
                objTransaction.TypeId = 2;
                objRestaurantDbEntities.Transactions.Add(objTransaction);
                objRestaurantDbEntities.SaveChanges();
            }



            return true;
        }
    }
}