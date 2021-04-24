using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using pidevShoppyTounsi.Models.Login;
using pidevShoppyTounsi.Models;
using System.Diagnostics;

namespace pidevShoppyTounsi.Controllers
{
    public class LoginController : Controller
    {
        public static String TokenConnect = "";
        public static long ConnectedId = 0;
        public static String ConnectedName = "";
        public static String ConnectedRole = "";
      
        
        
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
                    ConnectedId = response.id;
                    ConnectedName = response.username;
                    ConnectedRole = response.roles.ElementAt(0);

                    if (response.roles.Contains("ROLE_ADMIN"))
                    {
                        return RedirectToAction("../User");
                    }
                    

                    return RedirectToAction("../Home");
                }
                else
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
                    TempData ["message"] = response.message;
                    return RedirectToAction("LoginRequest");
                }



            }
            catch
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
               
                return RedirectToAction("Verify");
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

     

        public ActionResult RestPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RestPassword(RestPassword forgetPassword)
        {
            try
            {
                var APIresponse = Client.PostAsJsonAsync<RestPassword>("http://localhost:8081/reset", forgetPassword).GetAwaiter().GetResult();


                if (APIresponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("LoginRequest");
                }
                else
                {
                    TempData["message"] = APIresponse.StatusCode;
                    return RedirectToAction("RestPassword");
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
           
            try
            {
                LoginRequest l = new LoginRequest();
                l.username = forgetPassword.username;
                l.password = null;
                Debug.WriteLine(l.username);
                Debug.WriteLine(l.password);
                

                var APIresponse = Client.PostAsJsonAsync<LoginRequest>("http://localhost:8081/forget", l).GetAwaiter().GetResult();
               
                
                if (APIresponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("RestPassword");
                }
                else
                {
                    TempData["message"] = APIresponse.StatusCode;
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