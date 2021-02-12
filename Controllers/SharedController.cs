using dt102g_moment2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace dt102g_moment2.Controllers
{
    public class SharedController : Controller
    {
        // Method to get number of items in cart
        public int GetNrItems()
        {
            if (HttpContext.Session.GetString("SelectedItems") != null)
            {
                string NumberOfProducts = HttpContext.Session.GetString("SelectedItems");
                var ProductsObj = JsonConvert.DeserializeObject<ICollection<ProductModel>>(NumberOfProducts);
                return ProductsObj.Count;
            }
            else
            {
                return 0;
            }
        }
    }
}
