using dt102g_moment2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace dt102g_moment2.Controllers
{
    public class CheckoutController : Controller
    {
        // Change page routing
        [HttpGet("/Kassa")]
        public ActionResult Index()
        {
            // Check if session is set and display number of items
            if (HttpContext.Session.GetString("SelectedItems") != null)
            {
                string NumberOfProducts = HttpContext.Session.GetString("SelectedItems");
                var ProductsObj = JsonConvert.DeserializeObject<ICollection<ProductModel>>(NumberOfProducts);
                ViewData["CartItems"] = ProductsObj.Count;
            }
            else
            {
                ViewData["CartItems"] = 0;
            }

            // Check if session is set and send list of objects to view
            if (HttpContext.Session.GetString("SelectedItems") != null)
            {
                var ProductsObj = JsonConvert.DeserializeObject<List<ProductModel>>(HttpContext.Session.GetString("SelectedItems"));
                
                // Sort list to in case user adds same product more than once
                var sortedList = ProductsObj.OrderBy(name => name.ProductName).ToList();
                ViewData["Products"] = sortedList;
            }
            return View();            
        }

        [HttpPost("/Kassa")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckoutModel model)
        {
            // Check if fields validate
            if (ModelState.IsValid)
            {
                CheckoutModel checkout = new CheckoutModel
                {
                    Name = model.Name,
                    Ssn = model.Ssn,
                    Address = model.Address,
                    Zip = model.Zip,
                    City = model.City,
                    Phone = model.Phone,
                    Email = model.Email
                };

                // Convert items and save in session using Json
                string checkoutStr = JsonConvert.SerializeObject(checkout);
                HttpContext.Session.SetString("checkout", checkoutStr);
                return RedirectToAction(nameof(Result));
            }
            return View();
        }
        [HttpGet("Utcheckning")]
        public ActionResult Result()
        {
            // Get values from model and send to view
            CheckoutModel checkout = new CheckoutModel();
            string checkoutStr = HttpContext.Session.GetString("checkout");
            checkout = JsonConvert.DeserializeObject<CheckoutModel>(checkoutStr);

            ViewBag.Name = checkout.Name;
            ViewBag.Ssn = checkout.Ssn;
            ViewBag.Address = checkout.Address;
            ViewBag.Zip = checkout.Zip;
            ViewBag.City = checkout.City;
            ViewBag.Phone = checkout.Phone;
            ViewBag.Email = checkout.Email;
            HttpContext.Session.Remove("SelectedItems");
            ViewData["CartItems"] = 0;
            return View();
        }

        // Empty Products session when button pressed
        public ActionResult Reset(IFormCollection fc)
        {
            if (fc["Reset"] == "Töm varukorgen")
            {
                HttpContext.Session.Remove("SelectedItems");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
