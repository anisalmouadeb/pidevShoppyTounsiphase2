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
    public class UserController : Controller
    {
        HttpClient Client;
        string baseAddress;

        public UserController()
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

        // GET: User
        public ActionResult Index()
        {
            HttpResponseMessage response = Client.GetAsync("getAllUsers").Result;
            IEnumerable<User> users;
            
            if (response.IsSuccessStatusCode)
            {
                users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
         
            }
            else
            {
                users = null;
                // this is a comment 
            }
         
            return View(users);
        }

        public ActionResult Desactivate(int id)
        {
            try
            {
               
                var APIresponse = Client.PostAsJsonAsync<User>(baseAddress + "api/auth/desactivate_Acount/" + id,null).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                // TODO: Add insert logic here
               

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Activate(int id)
        {
            try
            {

                var APIresponse = Client.PostAsJsonAsync<User>(baseAddress + "api/auth/activate_Acount/" + id, null).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                // TODO: Add insert logic here


                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = Client.GetAsync("getUserById/" + id).Result;
            User user;

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

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        public ActionResult ListOfUser()
        {
            HttpResponseMessage response = Client.GetAsync("getAllUsers").Result;
            IEnumerable<User> users;
            if (response.IsSuccessStatusCode)
            {
                users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            }
            else
            {
                users = null;
            }
                return View(users);
        }
        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
