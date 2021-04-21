using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class OrderController : Controller
    {
        HttpClient Client;
        string baseAddress;

        public OrderController()
        {
            Client = new HttpClient();
            baseAddress = "http://localhost:8081/";
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Order
        public ActionResult Index()
        {
            HttpResponseMessage response = Client.GetAsync("getAllOrders").Result;
            IEnumerable<Order> orders;

            if (response.IsSuccessStatusCode)
            {
                orders = response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
            }
            else
            {
                orders = null;
                // comment 
            }

            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
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

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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
        // GET: GetOrderOftheMonth
        public ActionResult GetOrderOftheMonth()
        {

            HttpResponseMessage response = Client.GetAsync("GetOrderOftheMonth").Result;
            Order orders;

            if (response.IsSuccessStatusCode)
            {
                orders = response.Content.ReadAsAsync<Order>().Result;
            }
            else
            {
                orders = null;

            }

            return View(orders);
        }
        // GET: GetStarUserOftheMonth
        public ActionResult GetStarUserOftheMonth()
        {

            HttpResponseMessage response = Client.GetAsync("GetStarUserOftheMonth").Result;
            User  users;

            if (response.IsSuccessStatusCode)
            {
                users = response.Content.ReadAsAsync<User>().Result;
            }
            else
            {
                users = null;

            }

            return View(users);
        }
    }
}
