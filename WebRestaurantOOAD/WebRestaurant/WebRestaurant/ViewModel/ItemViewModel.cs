using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRestaurant.ViewModel
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set;}
        public decimal ItemPrice { get; set; }
    }
}