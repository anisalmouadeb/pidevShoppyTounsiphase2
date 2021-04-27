using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class DonationController : Controller
    {
        HttpClient Client;
        string baseAddress;
        public DonationController()
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
        // GET: Donation
        public ActionResult Index()
        {
            HttpResponseMessage response = Client.GetAsync("retriveAllDonations").Result;
            IEnumerable<Donation> donations;
            if (response.IsSuccessStatusCode)
            {
                donations = response.Content.ReadAsAsync<IEnumerable<Donation>>().Result;
            }
            else
            {
                donations = null;
            }
            return View(donations);
        }
        public ActionResult MyDonations()
        {
            HttpResponseMessage response = Client.GetAsync("retriveMyDonations").Result;
            IEnumerable<Donation> donations;
            if (response.IsSuccessStatusCode)
            {
                donations = response.Content.ReadAsAsync<IEnumerable<Donation>>().Result;
            }
            else
            {
                donations = null;
            }
            return View(donations);
        }
        public ActionResult Rappel()
        {
            HttpResponseMessage response = Client.GetAsync("rappelDonations").Result;
            IEnumerable<String> mails;
            if (response.IsSuccessStatusCode)
            {
                mails = response.Content.ReadAsAsync<IEnumerable<String>>().Result;
            }
            else
            {
                mails = null;
            }
            return RedirectToAction("Index");
        }

        // GET: Donation/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = Client.GetAsync("retriveDonation/" + id).Result;
            Donation donation;
            if (response.IsSuccessStatusCode)
            {
                donation = response.Content.ReadAsAsync<Donation>().Result;
            }
            else
            {
                donation = null;
            }


            return View(donation);
        }
        public ActionResult MonthDonation()
        {
            HttpResponseMessage response = Client.GetAsync("retriveMonthDonation").Result;
            Donation donation;
            if (response.IsSuccessStatusCode)
            {
                donation = response.Content.ReadAsAsync<Donation>().Result;
            }
            else
            {
                donation = null;
            }


            return View(donation);
        }
        public ActionResult AllTimeDonation()
        {
            HttpResponseMessage response = Client.GetAsync("retriveAllTimeDonation").Result;
            Donation donation;
            if (response.IsSuccessStatusCode)
            {
                donation = response.Content.ReadAsAsync<Donation>().Result;
            }
            else
            {
                donation = null;
            }


            return View(donation);
        }

        // GET: Donation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donation/Create
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

        // GET: Donation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Donation/Edit/5
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

        // GET: Donation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Donation/Delete/5
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