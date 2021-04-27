using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class DiscountTokenController : Controller
    {
        HttpClient Client;
        string baseAddress;
        public DiscountTokenController()
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
        // GET: DiscountToken
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyDiscountTokens()
        {
            HttpResponseMessage response = Client.GetAsync("retriveMyDiscountTokens").Result;
            IEnumerable<DiscountToken> discountTokens;
            if (response.IsSuccessStatusCode)
            {
                discountTokens = response.Content.ReadAsAsync<IEnumerable<DiscountToken>>().Result;
            }
            else
            {
                discountTokens = null;
            }
            return View(discountTokens);
        }


        // GET: DiscountToken/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = Client.GetAsync("retriveDiscountToken/" + id).Result;
            DiscountToken discountToken;
            if (response.IsSuccessStatusCode)
            {
                discountToken = response.Content.ReadAsAsync<DiscountToken>().Result;
            }
            else
            {
                discountToken = null;
            }


            return View(discountToken);
        }

        // GET: DiscountToken/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiscountToken/Create
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

        // GET: DiscountToken/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DiscountToken/Edit/5
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

        // GET: DiscountToken/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DiscountToken/Delete/5
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
