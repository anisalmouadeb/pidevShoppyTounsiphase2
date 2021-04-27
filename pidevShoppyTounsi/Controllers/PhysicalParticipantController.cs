using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class PhysicalParticipantController : Controller
    {
        // GET: PhysicalParticipant
        HttpClient Client;
        string baseAddress;
        public PhysicalParticipantController()
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
        // GET: PhysicalParticipant
        public ActionResult Index()
        {
            return View();
        }

        // GET: PhysicalParticipant/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        public ActionResult ofEvent(int id)
        {
            HttpResponseMessage response = Client.GetAsync("retriveEventPhysicalParticipant/" + id).Result;
            IEnumerable<PhysicalParticipant> physicalParticipants;
            if (response.IsSuccessStatusCode)
            {
                physicalParticipants = response.Content.ReadAsAsync<IEnumerable<PhysicalParticipant>>().Result;
            }
            else
            {
                physicalParticipants = null;
            }
            return View(physicalParticipants);
        }
        // GET: PhysicalParticipant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhysicalParticipant/Create
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

        // GET: PhysicalParticipant/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PhysicalParticipant/Edit/5
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

        // GET: PhysicalParticipant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PhysicalParticipant/Delete/5
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