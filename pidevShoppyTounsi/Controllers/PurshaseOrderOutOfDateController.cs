using PagedList;
using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class PurshaseOrderOutOfDateController : Controller
    {
        HttpClient Client;
        string baseAddress;


        public PurshaseOrderOutOfDateController()
        {
            Client = new HttpClient();
            baseAddress = "http://localhost:8081/";
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: BonCommande
        public ActionResult Index()
        {
            HttpResponseMessage response = Client.GetAsync("getBonCommandListOutOfDate").Result;
            IEnumerable<BonCommande> bonCommandes;

            if (response.IsSuccessStatusCode)
            {
                bonCommandes = response.Content.ReadAsAsync<IEnumerable<BonCommande>>().Result;


             

               
            }
            else
            {
                bonCommandes = null;

            }
        
            return View(bonCommandes);
        }
        // GET: PurshaseOrderOutOfDate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PurshaseOrderOutOfDate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurshaseOrderOutOfDate/Create
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

        // GET: PurshaseOrderOutOfDate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PurshaseOrderOutOfDate/Edit/5
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

        // GET: PurshaseOrderOutOfDate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PurshaseOrderOutOfDate/Delete/5
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
