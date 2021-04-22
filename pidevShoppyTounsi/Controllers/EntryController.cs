using PagedList;
using pidevShoppyTounsi.Models;
using pidevShoppyTounsi.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    [RedirectingAction]
    public class EntryController : Controller
    {

        HttpClient Client;
        string baseAddress;

        public EntryController()
        {
            Client = new HttpClient();
            baseAddress = "http://localhost:8081/";
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Entry
        public ActionResult Index()
        {

            HttpResponseMessage response = Client.GetAsync("getAllEntry").Result;
            IEnumerable<Entry> entries;
          

            if (response.IsSuccessStatusCode)
            {
                entries = response.Content.ReadAsAsync<IEnumerable<Entry>>().Result;
               
            }
            else
            {
                entries = null;
            }
         
            return View(entries);
        }

        // GET: Entry/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = Client.GetAsync("getEntryById/" + id).Result;
            Entry entry;

            if (response.IsSuccessStatusCode)
            {
                entry = response.Content.ReadAsAsync<Entry>().Result;
            }
            else
            {
                entry = null;

            }

            return View(entry);
        }

        // GET: Entry/Create
        public ActionResult Create()
        {
            
            HttpResponseMessage response = Client.GetAsync("getAllProviders").Result;
            IEnumerable<Provider> providers;
            HttpResponseMessage response2 = Client.GetAsync("allProducts").Result;
            IEnumerable<Product> products;
            if (response.IsSuccessStatusCode)
            {
                providers = response.Content.ReadAsAsync<IEnumerable<Provider>>().Result;
                providers.ToList().RemoveAll(x => x.Disponibility == false);
            }
            else
            {
                providers = null;
            }
          
            if (response2.IsSuccessStatusCode)
            {
                products = response2.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            }
            else
            {
                products = null;
            }
            ViewBag.ProviderId = new SelectList(providers, "ProviderId", "name");
            ViewBag.ProductId = new SelectList(products, "ProductId", "name");
            return View();
        }

        // POST: Entry/Create
        [HttpPost]
        public ActionResult Create(Entry entry)
        {
            try
            {

                // TODO: Add insert logic here
                System.Diagnostics.Debug.WriteLine(entry.provider.providerId);
                var APIresponse = Client.PostAsJsonAsync<Entry>(baseAddress + "addEntry", entry).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Entry/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Entry/Edit/5
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

        // GET: Entry/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = Client.GetAsync("getEntryById/" + id).Result;
            Entry entry;

            if (response.IsSuccessStatusCode)
            {
                entry = response.Content.ReadAsAsync<Entry>().Result;
            }
            else
            {
                entry = null;

            }

            return View(entry);
        }

        // POST: Entry/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
          
            try
            {
               
                // TODO: Add delete logic here
                var APIresponse = Client.DeleteAsync(baseAddress + "deleteEntryById/" + id).ContinueWith(deleteTask => deleteTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            catch
            {
               
                return View();
            }
        }
    }
}
