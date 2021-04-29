﻿using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class DeliveryController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public DeliveryController()
        {
            baseAddress = "http://localhost:8081/delivery/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg2MjAyOTgsImV4cCI6MTYxMjcxNDM4M30.geYwLgC7H47ALR2JqQrG8u5pYcK88QgxB3TYVgQhlcs";
            // httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }
        // GET: Question_Satisfaction
        public ActionResult GetAllDeliverys()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getallDelivery").GetAwaiter().GetResult();
            if (tokenResponse.IsSuccessStatusCode)
            {
                var deliverys = tokenResponse.Content.ReadAsAsync<IEnumerable<Delivery>>().Result;

                return View(deliverys);
            }
            else
            {

            }
            {
                var deliverys = tokenResponse.Content.ReadAsAsync<IEnumerable<Claim>>().GetAwaiter().GetResult();

                return View(deliverys);
            }

        }
        public ActionResult getDeliveryById(int id)
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getDeliveryById/" + id).GetAwaiter().GetResult();
            if (tokenResponse.IsSuccessStatusCode)
            {
                var delivery = tokenResponse.Content.ReadAsAsync<Delivery>().Result;

                return View(delivery);
            }
            else
            {
                var delivery = tokenResponse.Content.ReadAsAsync<Delivery>().GetAwaiter().GetResult();

                return View(delivery);
            }

        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Delivery delivery)
        {

            try
            {

                var APIResponse = httpClient.PostAsJsonAsync<Delivery>(baseAddress + "addDelivery/", delivery).GetAwaiter().GetResult();
                TempData["seccussmessage"] = "save seccuss";
                var message = APIResponse.ToString();
                if (APIResponse.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetAllDeliverys");
                }
                else
                {
                    return RedirectToAction("Create");
                }

            }
            catch
            {
                return View();
            }

        }




        public ActionResult Edit(int id)
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getDelivery/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var delivery = tokenResponse.Content.ReadAsAsync<Delivery>().Result;
                return View(delivery);
            }
            else
            {
                return View(new Delivery());
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Delivery delivery)
        {
            try
            {
                // TODO: Add update logic here

                // TODO: Add update logic here
                //question_Satisfaction.id = id;

                // var APIresponse = httpClient.PutAsJsonAsync<Question_Satisfaction>(baseAddress+"Updatequestion/"+id, question_Satisfaction).GetAwaiter().GetResult();
                var APIresponse = httpClient.PutAsJsonAsync<Delivery>(baseAddress + "updateDelivery/" + id, delivery).ContinueWith(putTask => putTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("getallClaim");
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = httpClient.DeleteAsync("deleteDelivery/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("getallDelivery");
            }
            return RedirectToAction("getallDelivery");

        }

    }

}
