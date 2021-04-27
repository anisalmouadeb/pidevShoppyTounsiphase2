using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class HomeController : Controller
    {

        HttpClient Client;
        string baseAddress;

        public HomeController()
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


        public ActionResult Index()
        {
            HttpResponseMessage response = Client.GetAsync("allProducts").Result;
            IEnumerable<Product> prods;
            if (response.IsSuccessStatusCode)
            {
                prods = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            }
            else
            {
                prods = null;

            }
            ViewBag.prods = prods;
            return View();
        }

        public ActionResult Products()
        {
            HttpResponseMessage response = Client.GetAsync("allProducts").Result;
            IEnumerable<Product> prods;
            if (response.IsSuccessStatusCode)
            {
                prods = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            }
            else
            {
                prods = null;

            }
            ViewBag.prods = prods;
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}