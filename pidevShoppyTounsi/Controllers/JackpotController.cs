using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class JackpotController : Controller
    {
        HttpClient Client;
        string baseAddress;
        public JackpotController()
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
        // GET: Jackpot
        public ActionResult Index()
        {
            return View();
        }

        // GET: Jackpot/Details/5
        public ActionResult Details()
        {
            HttpResponseMessage response = Client.GetAsync("retriveJackpot/1").Result;
            Jackpot jackpot;
            if (response.IsSuccessStatusCode)
            {
                jackpot = response.Content.ReadAsAsync<Jackpot>().Result;
            }
            else
            {
                jackpot = null;
            }


            return View(jackpot);
        }

        // GET: Jackpot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jackpot/Create
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

        // GET: Jackpot/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Jackpot/Edit/5
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

        // GET: Jackpot/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Jackpot/Delete/5
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