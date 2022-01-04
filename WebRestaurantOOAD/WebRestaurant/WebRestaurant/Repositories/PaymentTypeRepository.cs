using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRestaurant.Models;


namespace WebRestaurant.Repositories
{
    public class PaymentTypeRepository
    {
        private RestaurantDbEntities objRestaurantDbEntities;
        public PaymentTypeRepository(){
            objRestaurantDbEntities = new RestaurantDbEntities();
        }
        public IEnumerable<SelectListItem> GetAllPaymentType()
        {

            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in objRestaurantDbEntities.PaymentTypes
                                  select new SelectListItem()
                                  {
                                      Text=obj.PaymentTypeName,
                                      Value=obj.PaymentTypeId.ToString(),
                                      Selected=true

                                  }).ToList();
            return objSelectListItems;
        }
    }
}