using pidevShoppyTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace pidevShoppyTounsi.Controllers
{
    public class DeliveryManController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public DeliveryManController()
        {
            baseAddress = "http://localhost:8081/deliveryman/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg2MjAyOTgsImV4cCI6MTYxMjcxNDM4M30.geYwLgC7H47ALR2JqQrG8u5pYcK88QgxB3TYVgQhlcs";
            // httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }
        // GET: Question_Satisfaction
        public ActionResult GetAllDeliveryMans()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getallDeliveryMans").GetAwaiter().GetResult();
            if (tokenResponse.IsSuccessStatusCode)
            {
                var deliverymans = tokenResponse.Content.ReadAsAsync<IEnumerable<DeliveryMan>>().Result;

                return View(deliverymans);
            }
            else
            {
                var deliverymans = tokenResponse.Content.ReadAsAsync<IEnumerable<DeliveryMan>>().GetAwaiter().GetResult();

                return View(deliverymans);
            }

        }
        public ActionResult getDeliveryManById(int id)
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getDeliveryManById/" + id).GetAwaiter().GetResult();
            if (tokenResponse.IsSuccessStatusCode)
            {
                var deliveryman = tokenResponse.Content.ReadAsAsync<IEnumerable<DeliveryMan>>().Result;

                return View(deliveryman);
            }
            else
            {
                var deliveryman = tokenResponse.Content.ReadAsAsync<IEnumerable<DeliveryMan>>().GetAwaiter().GetResult();

                return View(deliveryman);
            }

        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeliveryMan deliveryman)
        {

            try
            {

                var APIResponse = httpClient.PostAsJsonAsync<DeliveryMan>(baseAddress + "addDeliveryMan/", deliveryman).GetAwaiter().GetResult();
                TempData["seccussmessage"] = "save seccuss";
                var message = APIResponse.ToString();
                if (APIResponse.IsSuccessStatusCode)
                {

                    return RedirectToAction("getallDeliveryMans");
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
            var tokenResponse = httpClient.GetAsync(baseAddress + "getDeliveryMan/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var deliveryman = tokenResponse.Content.ReadAsAsync<DeliveryMan>().Result;
                return View(deliveryman);
            }
            else
            {
                return View(new DeliveryMan());
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, DeliveryMan deliveryman)
        {
            try
            {
                // TODO: Add update logic here

                // TODO: Add update logic here
                //question_Satisfaction.id = id;

                // var APIresponse = httpClient.PutAsJsonAsync<Question_Satisfaction>(baseAddress+"Updatequestion/"+id, question_Satisfaction).GetAwaiter().GetResult();
                var APIresponse = httpClient.PutAsJsonAsync<DeliveryMan>(baseAddress + "updateDeliveryMan/" + id, deliveryman).ContinueWith(putTask => putTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("getallDeliveryMans");
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = httpClient.DeleteAsync("deleteDeliveryMan/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("getallDeliveryMans");
            }
            return RedirectToAction("getallDeliveryMans");

        }
    }
}