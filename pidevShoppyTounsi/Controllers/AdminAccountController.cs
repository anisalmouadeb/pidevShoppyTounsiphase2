using pidevShoppyTounsi.Models;
using pidevShoppyTounsi.Models.Login;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    [RedirectingAction]
    public class AdminAccountController : Controller
    {

        HttpClient Client;
        string baseAddress;
        public AdminAccountController()
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
        // GET: AdminAccount
        public ActionResult Index()
        {



            return View();
        }


        public ActionResult Home()
        {
            HttpResponseMessage response = Client.GetAsync("getSumOutlay").Result;

            var x = 0;
            if (response.IsSuccessStatusCode)
            {
                x = response.Content.ReadAsAsync<int>().Result;
            }
            else
            {
                x = 0;

            }
            ViewBag.sumoutLay = x;
            Debug.WriteLine(x);
            HttpResponseMessage response2 = Client.GetAsync("getAllUsers").Result;
            IEnumerable<User> users;

            if (response.IsSuccessStatusCode)
            {
                users = response2.Content.ReadAsAsync<IEnumerable<User>>().Result;

            }
            else
            {
                users = null;
                // this is a comment 
            }var nbu = 0;
            foreach(User u in users)
            {if(u.name!= "admin")
                nbu++;
            }
            ViewBag.nbuser = nbu;
            Debug.WriteLine(x);
            HttpResponseMessage response3 = Client.GetAsync("getAllOrders").GetAwaiter().GetResult(); ;
            IEnumerable<Order> orders;

            if (response.IsSuccessStatusCode)
            {
                orders = response3.Content.ReadAsAsync<IEnumerable<Order>>().Result;

            }
            else
            {
                orders = null;
                // this is a comment 
            }
            var amount = 0.0;
            foreach (Order o in orders)
            {

                amount = amount+o.orderAmount;
            }
            //getShelfRevenu

            HttpResponseMessage response4 = Client.GetAsync("getShelfRevenu").GetAwaiter().GetResult(); ;
            IEnumerable<ShelfRevenu> shelfrv;

            if (response.IsSuccessStatusCode)
            {
                shelfrv = response4.Content.ReadAsAsync<IEnumerable<ShelfRevenu>>().Result;

            }
            else
            {
                shelfrv = null;
                // this is a comment 
            }

            Debug.WriteLine(shelfrv.ElementAt(0));
            ViewBag.shelfs = shelfrv;


            ViewBag.amount = amount;
            Debug.WriteLine(amount);













            HttpResponseMessage response5 = Client.GetAsync("getAllEntry").Result;
            IEnumerable<Entry> entries;


            if (response5.IsSuccessStatusCode)
            {
                entries = response5.Content.ReadAsAsync<IEnumerable<Entry>>().Result;

            }
            else
            {
                entries = null;
            }
            List<double> montants = new List<double>() { 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 7; i++)
            {
                montants[i] = 0;
            }
            for (int i = 0; i < 7; i++)
            {
                double mt = 0;
                foreach (Entry e in entries)
                {
                    if (e.entryDate.Day.Equals(DateTime.Now.Day - i) && e.entryDate.Month.Equals(DateTime.Now.Month) && e.entryDate.Year.Equals(DateTime.Now.Year))
                    {
                        mt = mt + e.montant;
                    }

                }
                montants[i] = mt;
                Debug.WriteLine(montants[i]);
            }
            ViewBag.amounts = montants;







            return View();
        }
        // GET: AdminAccount/Details/5
        public ActionResult Details()
        {
            Debug.WriteLine(LoginController.TokenConnect);
            HttpResponseMessage response = Client.GetAsync("getMyInfo").Result;
            User user;
            Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                user = response.Content.ReadAsAsync<User>().Result;
            }
            else
            {
                user = null;

            }
            return View(user);
        }

        // GET: AdminAccount/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Logout()
        {
            LoginController.TokenConnect = "";
            Debug.WriteLine(LoginController.ConnectedRole);
            if(LoginController.ConnectedRole == "ROLE_CLIENT")
            {
              return  RedirectToAction("../Home");
            }
            return RedirectToAction("../Home");
        }

        // POST: AdminAccount/Create
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

        // GET: AdminAccount/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: AdminAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                // TODO: Add update logic here
                var APIresponse = Client.PutAsJsonAsync<User>(baseAddress + "updateUser", user).GetAwaiter().GetResult(); 

                if (APIresponse.IsSuccessStatusCode)
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
                    LoginController.TokenConnect = response.accessToken;
                    LoginController.ConnectedName = response.username;
                    LoginController.ConnectedId = response.id;
                    LoginController.ConnectedRole = response.roles.ElementAt(0);



                    return RedirectToAction("Details");
                }



                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminAccount/Delete/5
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
