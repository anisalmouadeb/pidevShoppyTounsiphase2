using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList;
using pidevShoppyTounsi.Models.Login;

namespace pidevShoppyTounsi.Controllers
{
    [RedirectingAction]
    public class ProviderController : Controller
    {
        HttpClient Client;
        string baseAddress;

        public ProviderController()
        {
            Client = new HttpClient();
            baseAddress = "http://localhost:8081/";
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
      
        // GET: Provider
        public ActionResult Index(String searchString, string sortOrder, string currentFilter, int? page)
        {
         
           
            HttpResponseMessage response = Client.GetAsync("getAllProviders").Result;
            IEnumerable<Provider> providers;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.NoteSortParm = sortOrder == "Note" ? "note_desc" : "Note";
            ViewBag.DeleviryFeesSortParm = sortOrder == "DeleviryFees" ? "DeleviryFees_desc" : "DeleviryFees";
            ViewBag.SeuilMontantSortParm = sortOrder == "SeuilMontant" ? "SeuilMontant_desc" : "SeuilMontant";
            ViewBag.ReductionPercentageSortParm = sortOrder == "ReductionPercentage" ? "ReductionPercentage_desc" : "ReductionPercentage";
            ViewBag.DisponibilitySortParm = sortOrder == "Disponibility" ? "Disponibility_desc" : "Disponibility";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (response.IsSuccessStatusCode)
            {
                providers = response.Content.ReadAsAsync<IEnumerable<Provider>>().Result;
                
                if (!String.IsNullOrEmpty(searchString))
                {
                    providers = providers.Where(s => s.name.Contains(searchString)
                                           || s.email.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        providers = providers.OrderByDescending(s => s.name);
                        break;
                    case "Email":
                        providers = providers.OrderBy(s => s.email);
                        break;
                    case "email_desc":
                        providers = providers.OrderByDescending(s => s.email);
                        break;
                    case "Note":
                        providers = providers.OrderBy(s => s.note);
                        break;
                    case "note_desc":
                        providers = providers.OrderByDescending(s => s.note);
                        break;
                    case "DeleviryFees":
                        providers = providers.OrderBy(s => s.deleviryFees);
                        break;
                    case "DeleviryFees_desc":
                        providers = providers.OrderByDescending(s => s.deleviryFees);
                        break;
                    case "SeuilMontant":
                        providers = providers.OrderBy(s => s.seuilMontant);
                        break;
                    case "SeuilMontant_desc":
                        providers = providers.OrderByDescending(s => s.seuilMontant);
                        break;
                    case "ReductionPercentage":
                        providers = providers.OrderBy(s => s.reductionPercentage);
                        break;
                    case "ReductionPercentage_desc":
                        providers = providers.OrderByDescending(s => s.reductionPercentage);
                        break;
                    case "Disponibility":
                        providers = providers.OrderBy(s => s.Disponibility);
                        break;
                    case "Disponibility_desc":
                        providers = providers.OrderByDescending(s => s.Disponibility);
                        break;
                    default:
                        providers = providers.OrderBy(s => s.name);
                        break;
                }




            }
            else
            {
                providers = null;

            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
        
            return View(providers.ToPagedList(pageNumber,pageSize));
        }

        // GET: Provider/Details/5
        public ActionResult Details(long id)
        {


            HttpResponseMessage response = Client.GetAsync("getProviderById/"+id).Result;
            Provider provider;

            if (response.IsSuccessStatusCode)
            {
                provider = response.Content.ReadAsAsync<Provider>().Result;
            }
            else
            {
                provider = null;

            }

            return View(provider);
        }

        // GET: Provider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provider/Create
        [HttpPost]
        public ActionResult Create(Provider provider)
        {
            try
            {
                // TODO: Add insert logic here
                var APIresponse = Client.PostAsJsonAsync<Provider>(baseAddress + "addProvider",provider).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Provider/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Provider/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, Provider provider)
        {
            try
            {
                HttpResponseMessage response = Client.GetAsync("getProviderById/" + id).Result;
                Provider provider2;

                if (response.IsSuccessStatusCode)
                {
                    provider2 = response.Content.ReadAsAsync<Provider>().Result;
                }
                else
                {
                    provider2 = null;

                }
                ViewBag.name = provider2.name;
                Console.WriteLine(ViewBag.name);
                // TODO: Add update logic here
                provider.providerId = id;
                var APIresponse = Client.PutAsJsonAsync<Provider>(baseAddress + "updateProvider", provider).ContinueWith(putTask => putTask.Result.EnsureSuccessStatusCode());
             
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Provider/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = Client.GetAsync("getProviderById/" + id).Result;
            Provider provider;

            if (response.IsSuccessStatusCode)
            {
                provider = response.Content.ReadAsAsync<Provider>().Result;
            }
            else
            {
                provider = null;

            }
            return View(provider);
        }

        // POST: Provider/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var APIresponse = Client.DeleteAsync(baseAddress + "deleteProviderById/"+id).ContinueWith(deleteTask => deleteTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
