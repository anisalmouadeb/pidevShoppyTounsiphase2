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
            return RedirectToAction("../Login/LoginRequest");
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
