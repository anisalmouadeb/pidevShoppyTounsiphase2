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
    public class ClientAccountController : Controller
    {
        HttpClient Client;
        string baseAddress;
        public ClientAccountController()
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
        // GET: ClientAccount
        public ActionResult Index()
        {
            return View();
        }

        // GET: ClientAccount/Details/5
        public ActionResult Details(int id)
        {
           
            return View();
        }
        public ActionResult MyAccount()
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

        public ActionResult UpdateAccount()
        {
            return View();
        }

            [HttpPost]
        public ActionResult UpdateAccount(FormCollection collection, User u)
        {
       
            try
            {
                var id = u.userId;
              
                Debug.WriteLine(id);
                Debug.WriteLine(u.email);
                Debug.WriteLine(u.name);
                Debug.WriteLine(u.sex);
                Debug.WriteLine(u.age);
                // TODO: Add update logic here

                var APIresponse = Client.PutAsJsonAsync<User>(baseAddress + "updateUser", u).GetAwaiter().GetResult();

                 if (APIresponse.IsSuccessStatusCode)
                 {
                     JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
                     LoginController.TokenConnect = response.accessToken;

                     Debug.WriteLine(u.name);
                     Debug.WriteLine(u.userId);

                     return RedirectToAction("MyAccount");
                 }
                else
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
                    TempData["message1"] = response.message;
                    Debug.WriteLine(TempData["message1"].ToString());
                    return RedirectToAction("UpdateAccount");
                }



            }
            catch
            {
                return View();
            }
        }

        // GET: ClientAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientAccount/Create
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

        // GET: ClientAccount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientAccount/Edit/5
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

        // GET: ClientAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientAccount/Delete/5
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
