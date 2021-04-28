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
    public class CartController : Controller
    {
        HttpClient Client;
        string baseAddress;
        public static IEnumerable<OrderLine> orderLines;
        public static float amount;
        public CartController()
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

        [HttpPost]
        public ActionResult Add(AddToCart c)
        {
            try
            {
                HttpResponseMessage response = Client.GetAsync("getUserById/" + LoginController.ConnectedId).Result;
                User user;

                if (response.IsSuccessStatusCode)
                {
                    user = response.Content.ReadAsAsync<User>().Result;
                }
                else
                {
                    user = null;

                }
                HttpResponseMessage response2 = Client.GetAsync("getProductById/" + c.id).Result;
                Product Product;

                if (response2.IsSuccessStatusCode)
                {
                    Product = response2.Content.ReadAsAsync<Product>().Result;
                }
                else
                {
                    Product = null;

                }
                OrderLine l = new OrderLine() { };
                l.quantity = c.quantity;
                l.shoppingCart = user.shoppingcart;
                Debug.WriteLine("sh");
                Debug.WriteLine(l.shoppingCart.shoppingCartId);
                l.product = Product;
                Debug.WriteLine("product");
                Debug.WriteLine(l.product.productId);
                Debug.WriteLine("quantity");
                Debug.WriteLine(c.quantity);
             
                // TODO: Add delete logic here
               var APIresponse = Client.PostAsJsonAsync<OrderLine>(baseAddress + "addOrderLine", l).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("MyCart");
            }
            catch
            {
                return RedirectToAction("MyCart");
            }
        }
        public ActionResult MyCart()
        {
            Debug.WriteLine(LoginController.ConnectedId);
            HttpResponseMessage response = Client.GetAsync("getOrderLineByUser/" + LoginController.ConnectedId).Result;
            IEnumerable<OrderLine> ords;

            if (response.IsSuccessStatusCode)
            {
                ords = response.Content.ReadAsAsync<IEnumerable<OrderLine>>().Result;
            }
            else
            {
                ords = null;

            }
            CartController.orderLines = ords;
            ViewBag.ords = ords;

            HttpResponseMessage response3 = Client.GetAsync("getUserById/" + LoginController.ConnectedId).Result;
            User user;

            if (response3.IsSuccessStatusCode)
            {
                user = response3.Content.ReadAsAsync<User>().Result;
            }
            else
            {
                user = null;

            }
            HttpResponseMessage response2 = Client.GetAsync("getTotalAmount/" + user.shoppingcart.shoppingCartId).Result;

            float a = 0;
            if (response2.IsSuccessStatusCode)
            {
                a = response2.Content.ReadAsAsync<float>().Result;
            }
            else
            {
                a = 0;
                // comment 
            }

            CartController.amount = a;


            return View();
          
            
         
          
        }
   
            [HttpPost]
        public ActionResult DeleteOrderLine(int id)
        {
            return View();


        }
        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
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

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
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

        // GET: Cart/Delete/5


        // POST: Cart/Delete/5
      
        public ActionResult Total()
        {
          
            return View();
        }
            public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var APIresponse = Client.DeleteAsync(baseAddress + "DeleteOrderLine/" + id).ContinueWith(deleteTask => deleteTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("MyCart");
            }
            catch
            {
                return RedirectToAction("MyCart");
            }
        }

        public ActionResult Preconfirm()
        {

            return View();
        }
        public ActionResult Payment()
        {

            return View();
        }

        public ActionResult OrderComplete()
        {

            return View();
        }
        public ActionResult CancelOrder()
        {

            return View();
        }


    }







}

