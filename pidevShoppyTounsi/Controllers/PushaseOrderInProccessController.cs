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
    public class PushaseOrderInProccessController : Controller
    {
        HttpClient Client;
        string baseAddress;


        public PushaseOrderInProccessController()
        {
            Client = new HttpClient();
            baseAddress = "http://localhost:8081/";
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: BonCommande
        public ActionResult Index()
        {
            HttpResponseMessage response = Client.GetAsync("getBonCommandInProccess").Result;
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
        [HttpPost]
        public ActionResult NotifyProvider(BonCommande b)
        {
            try
            {
                
                HttpResponseMessage response = Client.GetAsync("getProductById/" + b.productName).Result;
                Product Product;

                if (response.IsSuccessStatusCode)
                {
                    Product = response.Content.ReadAsAsync<Product>().Result;
                }
                else
                {
                    Product = null;

                }


                HttpResponseMessage response2 = Client.GetAsync("getProviderById/" + b.providerName).Result;
                Provider provider;

                if (response.IsSuccessStatusCode)
                {
                    provider = response2.Content.ReadAsAsync<Provider>().Result;
                }
                else
                {
                    provider = null;

                }


                // TODO: Add insert logic here
                b.productName = Product.name;
                b.providerName = provider.name;
                var APIresponse = Client.PostAsJsonAsync<BonCommande>(baseAddress + "AddBonCommand", b).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

            public ActionResult NotifyProvider()
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




    }

 

}