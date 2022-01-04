using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRestaurant.Models;

namespace WebRestaurant.Repositories
{
    public class ItemRepository
    {
        private RestaurantDbEntities objRestaurantDbEntities;
        public ItemRepository()
        {
            objRestaurantDbEntities = new RestaurantDbEntities();
        }
        public IEnumerable<SelectListItem> GetAllItems()
        {

            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in objRestaurantDbEntities.Items
                                  select new SelectListItem()
                                  {
                                      Text = obj.ItemName,
                                      Value = obj.ItemId.ToString(),
                                      Selected = false

                                  }).ToList();
            return objSelectListItems;
        }

    }
}