using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class EventController : Controller
    {

        HttpClient Client;
        string baseAddress;
        public EventController()
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
        // GET: Event
        public ActionResult Index()
        {
            HttpResponseMessage response = Client.GetAsync("retriveAllEvents").Result;
            IEnumerable<Event> events;
            if (response.IsSuccessStatusCode)
            {
                events = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
            }
            else
            {
                events = null;
            }
            return View(events);
        }
        public ActionResult MyEvents()
        {
            HttpResponseMessage response = Client.GetAsync("retriveMyEvents").Result;
            IEnumerable<Event> events;
            if (response.IsSuccessStatusCode)
            {
                events = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
            }
            else
            {
                events = null;
            }
            return View(events);
        }
        public ActionResult DispoEvents()
        {
            HttpResponseMessage response = Client.GetAsync("retriveDispoEvents").Result;
            IEnumerable<Event> events;
            if (response.IsSuccessStatusCode)
            {
                events = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
            }
            else
            {
                events = null;
            }
            return View(events);
        }
        public ActionResult suggestionEvents()
        {
            HttpResponseMessage response = Client.GetAsync("retriveMySuggestionsEvents").Result;
            IEnumerable<Event> events;
            if (response.IsSuccessStatusCode)
            {
                events = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
            }
            else
            {
                events = null;
            }
            return View(events);
        }
        public ActionResult BestEvent()
        {
            HttpResponseMessage response = Client.GetAsync("retriveBestEvent").Result;
            Event ev;
            if (response.IsSuccessStatusCode)
            {
                ev = response.Content.ReadAsAsync<Event>().Result;
            }
            else
            {
                ev = null;
            }


            return View(ev);

        }
        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = Client.GetAsync("retriveEvent/" + id).Result;
            Event ev;
            if (response.IsSuccessStatusCode)
            {
                ev = response.Content.ReadAsAsync<Event>().Result;
            }
            else
            {
                ev = null;
            }


            return View(ev);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(Event ev)
        {
            try
            {
                // TODO: Add insert logic here
                var APIresponse = Client.PostAsJsonAsync<Event>("addEvent", ev).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var path = "c://..";
                // TODO: Add update logic here
                var APIresponse = Client.PutAsJsonAsync<Object>("closeEvent/" + id, path).ContinueWith(putTask => putTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Participate(int id)
        {

            HttpResponseMessage response = Client.GetAsync("retriveEvent/" + id).Result;
            Event ev;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<Event>().Result;
            }
            else
            {
                ViewBag.result = "error";
            }
            return View();

        }

        [HttpPost]
        public ActionResult Participate(int id, Event ev)
        {

            try
            {
                // TODO: Add update logic here
                var APIresponse = Client.PutAsJsonAsync<Object>("participateEvent/" + id, ev).ContinueWith(putTask => putTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
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