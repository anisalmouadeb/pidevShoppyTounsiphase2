using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using pidevShoppyTounsi.Models.Login;
using pidevShoppyTounsi.Models;

namespace pidevShoppyTounsi.Controllers
{
    public class LoginController : Controller
    {
        public static String TokenConnect = "";
        HttpClient Client;
        string baseAddress;

        public LoginController()
        {
            Client = new HttpClient();
            baseAddress = "http://localhost:8081/api/auth/";
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            if (LoginController.TokenConnect != "")
            {
                Client.DefaultRequestHeaders.Add("Accept", "application/json");

                Client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LoginController.TokenConnect);
            }
           
        }


        // GET: Login
        public ActionResult Index()
        {
            

            HttpResponseMessage response = Client.GetAsync("http://localhost:8081/api/logg/get_All_Logg_User").Result;

            return Content("aa");
        }


        #region signin

        public ActionResult LoginRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginRequest(LoginRequest loginrequest)
        {
            try
            {

               var APIresponse = Client.PostAsJsonAsync<LoginRequest>(baseAddress + "signin", loginrequest).GetAwaiter().GetResult();
                if (APIresponse.IsSuccessStatusCode)
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
                    TokenConnect = response.accessToken;

                    

                    return RedirectToAction("Index");
                }
                else
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
                    TempData ["message"] = response.message;
                    return RedirectToAction("LoginRequest");
                }



            }
            catch(Exception e)
            {
                return View();
            }
        }


        #endregion

        #region sign up 

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpRequest signUpRequest)
        {
            var APIresponse = Client.PostAsJsonAsync<SignUpRequest>(baseAddress + "signup", signUpRequest).GetAwaiter().GetResult();
            if (APIresponse.IsSuccessStatusCode)
            {
                JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
               
                return RedirectToAction("LoginRequest");
            }
            else
            {
                JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
                TempData["message"] = response.message;
                return RedirectToAction("SignUp");
            }
        }


        #endregion
        public ActionResult Verify()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(Verify verify)
        {
            try
            {

            
            int a = verify.code;
            var APIresponse = Client.PostAsJsonAsync<Verify>(baseAddress + "verifyEmail/"+a, null).GetAwaiter().GetResult();
            if (APIresponse.IsSuccessStatusCode)
            {
                return RedirectToAction("LoginRequest");
            }
            else
            {
                
                TempData["message"] = "Please verify your code";
                return RedirectToAction("Verify");
            }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPassword forgetPassword)
        {
            LoginRequest l = new LoginRequest();
            l.username = forgetPassword.username.ToLower();
            Console.WriteLine(l.username);
            try
            {

                var APIresponse = Client.PostAsJsonAsync<LoginRequest>(baseAddress + "forget", l).GetAwaiter().GetResult();
                if (APIresponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("LoginRequest");
                }
                else
                {
                    TempData["message"] = "Verify your Username";
                    return RedirectToAction("ForgetPassword");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}