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
    }
}