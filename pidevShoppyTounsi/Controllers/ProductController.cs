using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class ProductController : Controller
    {
        HttpClient Client;
        string baseAddress;

        public ProductController()
        {
            Client = new HttpClient();
            baseAddress = "http://localhost:8081/";
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


        // GET: Product
        public ActionResult Index()
        {
            HttpResponseMessage response = Client.GetAsync("product/allProducts").Result;
            IEnumerable<Product> Products;

            if (response.IsSuccessStatusCode)
            {
                Products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            }
            else
            {
                Products = null;

            }
            foreach(Product p in Products)
            {
                Debug.WriteLine(p.name);
            }
            return View(Products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {

            HttpResponseMessage response = Client.GetAsync("getProductById/" + id).Result;
            Product Product;

            if (response.IsSuccessStatusCode)
            {
                Product = response.Content.ReadAsAsync<Product>().Result;
            }
            else
            {
                Product = null;

            }

            return View(Product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
