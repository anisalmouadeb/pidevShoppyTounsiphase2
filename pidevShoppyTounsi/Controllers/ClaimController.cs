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
    public class ClaimController : Controller
    {

        HttpClient httpClient;
        string baseAddress;
        public ClaimController()
        {
            baseAddress = "http://localhost:8081/claim/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg2MjAyOTgsImV4cCI6MTYxMjcxNDM4M30.geYwLgC7H47ALR2JqQrG8u5pYcK88QgxB3TYVgQhlcs";
            // httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }
        // GET: Question_Satisfaction
        public ActionResult GetAllClaim()
        {

            List<Claim> claimss = null;
            var tokenResponse = httpClient.GetAsync(baseAddress + "getallClaim").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var claims = tokenResponse.Content.ReadAsAsync<IEnumerable<Claim>>().Result;

                return View(claims);
            }
            else
            {

                return View(claimss);
            }

        }
        public ActionResult getClaimById(int id)
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getClaimById/" + id).GetAwaiter().GetResult();
            if (tokenResponse.IsSuccessStatusCode)
            {
                var claim = tokenResponse.Content.ReadAsAsync<Claim>().Result;

                return View(claim);
            }
            else
            {
                var claim = tokenResponse.Content.ReadAsAsync<Claim>().GetAwaiter().GetResult();

                return View(claim);
            }

        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Claim claim)
        {

            try
            {

                var APIResponse = httpClient.PostAsJsonAsync<Claim>(baseAddress + "addClaim/", claim).GetAwaiter().GetResult();
                TempData["seccussmessage"] = "save seccuss";
                var message = APIResponse.ToString();
                if (APIResponse.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetAllClaim");
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




        [HttpPost]
        public ActionResult Edit(int id, Claim claim)
        {
            try
            {

                // var APIresponse = httpClient.PutAsJsonAsync<Question_Satisfaction>(baseAddress+"Updatequestion/"+id, question_Satisfaction).GetAwaiter().GetResult();
                var APIresponse = httpClient.PutAsJsonAsync<Claim>(baseAddress + "updateClaim/" + id, claim).ContinueWith(putTask => putTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("getallClaim");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var tokenResponse = httpClient.DeleteAsync(baseAddress + "deleteClaim/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("getallClaim");
            }
            return RedirectToAction("getallClaim");

        }
    }
}