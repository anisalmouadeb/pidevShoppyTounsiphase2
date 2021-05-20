using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using pidevShoppyTounsi.Models;
using System.Diagnostics;

namespace pidevShoppyTounsi.Controllers
{
    public class MakePController : Controller
    {
        HttpClient Client;
        string baseAddress;
        public MakePController()
        {
            Client = new HttpClient();
            baseAddress = "http://localhost:8081/";
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            if (LoginController.TokenConnect != "")
            {
                Client.DefaultRequestHeaders.Add("Accept", "application/json");

                Client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LoginController.TokenConnect);
            }
        }
        // GET: MakeP
        public ActionResult Index()
        {
            return View();
        }

        // GET: MakeP/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MakeP/Create
        public ActionResult Create(FormCollection collection, MakeP makeP)
        {
            ConfirmP confirmP;
            Debug.WriteLine(makeP.sum);
            try
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
                int a = 0;
                foreach(Order o in orders )
                {
                    a = o.orderId;
                }
                makeP.sum = (int)CartController.amount;
                    
                // TODO: Add insert logic here
                var response2 = Client.PostAsJsonAsync<MakeP>("paypale/makee/paymentee/"+a, makeP).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode()).Result;
                confirmP = response2.Content.ReadAsAsync<ConfirmP>().Result;

                return RedirectToAction("Confirm", confirmP);
            }
            catch
            {
                return View();
            }
        }

        // POST: MakeP/Create
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

        // GET: MakeP/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MakeP/Edit/5
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

        // GET: MakeP/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MakeP/Delete/5
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

        public ActionResult Confirm(ConfirmP confirmP)
        {

            String url = confirmP.redirectUrl;
            ViewBag.url = url;

            return View(confirmP);
        }
        [HttpPost]
        public ActionResult BonA(BonAchat b)
        {
            Debug.WriteLine("nchalh tekhdm");
            Debug.WriteLine(b.code);
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
            int a = 0;
            foreach (Order o in orders)
            {
                a = o.orderId;
            }
            var APIresponse = Client.PostAsJsonAsync<Order>(baseAddress + "PaymentDone/" +a+"/" +b.code , null).GetAwaiter().GetResult();
            Debug.WriteLine(APIresponse.StatusCode);
            return RedirectToAction("../Cart/OrderComplete");
        }
    }
}
