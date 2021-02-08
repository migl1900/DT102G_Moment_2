﻿using dt102g_moment2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dt102g_moment2.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            // Display number of items in cart
            ViewData["CartItems"] = GetNrItems();

            // Read and parse JSON-file
            var JsonStr = System.IO.File.ReadAllText("products.json");
            var JsonObj = ConvertJson(JsonStr);

            // Send object to view
            return View(JsonObj);
        }
        [HttpGet("/Produkt")]
        [HttpPost("/Produkt")]
        public IActionResult Product([FromQuery] int ProductId, IFormCollection fc)
        {
            // Read and parse JSON-file and select value based on id
            var JsonStr = System.IO.File.ReadAllText("products.json");
            var JsonObj = ConvertJson(JsonStr);

            // Get specific product and save as object
            ProductModel product = new ProductModel();
            product.ProductImage = JsonObj.First(x => x.ProductId == ProductId).ProductImage;
            product.ProductName = JsonObj.First(x => x.ProductId == ProductId).ProductName;
            product.ProductDescription = JsonObj.First(x => x.ProductId == ProductId).ProductDescription;
            product.ProductPrice = JsonObj.First(x => x.ProductId == ProductId).ProductPrice;
            ViewBag.product = product;

            // Set bought items from user
            int Quantity = Convert.ToInt32(fc["quantity"]);

            // Create new list from model
            List<ProductModel> SelectedProducts = new List<ProductModel>();

            // If session exist get parse items to list
            if (HttpContext.Session.GetString("SelectedItems") != null)
            {
                SelectedProducts = JsonConvert.DeserializeObject<List<ProductModel>>(HttpContext.Session.GetString("SelectedItems"));
            }

            // Check that submit is pressed
            if (fc["submit"] == "Lägg i varukorgen")
            {
                // Add item to list chosen number of times
                for (int i = 0; i < Quantity; i++)
                {
                    SelectedProducts.Add(product);
                }

                // Save list to session as Json string
                string ProductStr = JsonConvert.SerializeObject(SelectedProducts);
                HttpContext.Session.SetString("SelectedItems", ProductStr);
            }

            // Display number of items in cart
            ViewData["CartItems"] = GetNrItems();

            return View();
        }

        // Method to convert Json to IEnumerable
        public IEnumerable<ProductModel> ConvertJson(string JsonStr)
        {
            var JsonObj = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(JsonStr);
            return JsonObj;
        }

        // Method to get number of items in cart
        public int GetNrItems()
        {
            if(HttpContext.Session.GetString("SelectedItems") != null)
            {
                string NumberOfProducts = HttpContext.Session.GetString("SelectedItems");
                var ProductsObj = JsonConvert.DeserializeObject<ICollection<ProductModel>>(NumberOfProducts);
                return ProductsObj.Count;
            } else
            {
                return 0;
            }
        }
    }
}
