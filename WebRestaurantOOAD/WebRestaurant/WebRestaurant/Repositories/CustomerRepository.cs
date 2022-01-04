using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRestaurant.Models;

namespace WebRestaurant.Repositories
{
    public class CustomerRepository
    {

        private RestaurantDbEntities objRestaurantDbEntities;
        public CustomerRepository()
        {
            objRestaurantDbEntities = new RestaurantDbEntities();
        }
        public IEnumerable<SelectListItem> GetAllCustomers()
        {

            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in objRestaurantDbEntities.Customers
                                  select new SelectListItem()
                                  {
                                      Text = obj.CustomerName,
                                      Value = obj.CustomerId.ToString(),
                                      Selected = false

                                  }).ToList();
            return objSelectListItems;
        }
    }
}