using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class MakePaimentController : Controller
    {
        HttpClient Client;
        string baseAddress;
        public MakePaimentController()
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
        // GET: MakePaiment
        public ActionResult Index()
        {
            return View();
        }

        // GET: MakePaiment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Confirm(ConfirmPaiment confirmPaiment)
        {

            String url = confirmPaiment.redirectUrl;
            ViewBag.url = url;

            return View(confirmPaiment);
        }

        // GET: MakePaiment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MakePaiment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, MakePaiment makePaiment)
        {
            ConfirmPaiment confirmPaiment;

            try
            {
                // TODO: Add insert logic here
                var response = Client.PostAsJsonAsync<MakePaiment>("paypal/make/payment", makePaiment).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode()).Result;
                confirmPaiment = response.Content.ReadAsAsync<ConfirmPaiment>().Result;

                return RedirectToAction("Confirm", confirmPaiment);
            }
            catch
            {
                return View();
            }
        }

        // GET: MakePaiment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MakePaiment/Edit/5
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

        // GET: MakePaiment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MakePaiment/Delete/5
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