using PagedList;
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
  
    public class ShelfController : Controller
    {
        HttpClient Client;
        string baseAddress;

        public ShelfController()
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
        [RedirectingAction]      // GET: Shelf
        public ActionResult Index()
        {
            HttpResponseMessage response = Client.GetAsync("getAllShelfs").Result;
            IEnumerable<Shelf> shelfs;

            if (response.IsSuccessStatusCode)
            {
                shelfs = response.Content.ReadAsAsync<IEnumerable<Shelf>>().Result;




            }
            else
            {
                shelfs = null;

            }

            return View(shelfs);
        }


       
        public ActionResult ListShelfs()
        {
            HttpResponseMessage response = Client.GetAsync("getShelfs").Result;
            IEnumerable<Shelf> shelfs;

            if (response.IsSuccessStatusCode)
            {
                shelfs = response.Content.ReadAsAsync<IEnumerable<Shelf>>().Result;




            }
            else
            {
                shelfs = null;

            }
            HttpResponseMessage response2 = Client.GetAsync("getAllRating").Result;
            IEnumerable<ShelfRating> shelfRatings;

            if (response2.IsSuccessStatusCode)
            {
                shelfRatings = response2.Content.ReadAsAsync<IEnumerable<ShelfRating>>().Result;

            }
            else
            {
                shelfRatings = null;

            }
            ViewBag.shelfRating = shelfRatings;
            Debug.WriteLine(shelfRatings.Count());
            return View(shelfs);
        }

        [RedirectingAction]

        public ActionResult ListOfProducts(int id)
        {
            HttpResponseMessage response = Client.GetAsync("getShelfById/" + id).Result;
            Shelf shelf;


            if (response.IsSuccessStatusCode)
            {
                shelf = response.Content.ReadAsAsync<Shelf>().Result;
            }
            else
            {
                shelf = null;

            }
            List<Product> products = new List<Product> { }; ;
            IEnumerable<Category> cats;

            cats = shelf.category;

            foreach (Category cat in cats)
            {

                foreach (Product p in cat.product)
                {
                    Debug.WriteLine(p.productId);
                    products.Add(p);
                    Debug.WriteLine(products.ElementAt(0));
                }
            }
            ViewBag.shelfname = shelf.shelfname;

            return View(products);
        }
        [RedirectingAction]
        public ActionResult ShelfListOfProducts(int id)
        {
            HttpResponseMessage response = Client.GetAsync("getShelfById/" + id).Result;
            Shelf shelf;


            if (response.IsSuccessStatusCode)
            {
                shelf = response.Content.ReadAsAsync<Shelf>().Result;
            }
            else
            {
                shelf = null;

            }
            List<Product> products = new List<Product> { }; ;
            IEnumerable<Category> cats;
            ViewBag.shelfName = shelf.shelfname;
            ViewBag.per = shelf.reductionPercantage;
            if (shelf.type != ShelfType.PROMO)
            { ViewBag.type = 1; }

            cats = shelf.category;

            foreach (Category cat in cats)
            {

                foreach (Product p in cat.product)
                {
                    Debug.WriteLine(p.productId);
                    products.Add(p);
                    Debug.WriteLine(products.ElementAt(0));
                }
            }
            ViewBag.shelfname = shelf.shelfname;

            return View(products);
        }

        [RedirectingAction]
        public ActionResult ListOfCategory(int id)
        {
            ViewBag.shelfid = id;
            HttpResponseMessage response = Client.GetAsync("getShelfById/" + id).Result;
            Shelf shelf;
            HttpResponseMessage response2 = Client.GetAsync("getAllCategory").Result;
            IEnumerable<Category> cats2;
            IList<Category> cats3 = new List<Category> { };
            if (response2.IsSuccessStatusCode)
            {
                cats2 = response2.Content.ReadAsAsync<IEnumerable<Category>>().Result;
            }
            else
            {
                cats2 = null;
            }



            if (response.IsSuccessStatusCode)
            {
                shelf = response.Content.ReadAsAsync<Shelf>().Result;
            }
            else
            {
                shelf = null;

            }

            IEnumerable<Category> cats;
            cats = shelf.category;
            foreach (Category c in cats2)
            {
                foreach (Category c2 in cats)
                {
                    if (c.categoryId != c2.categoryId)
                    {
                        cats3.Add(c);
                    }
                }
            }

            ViewBag.shelfname = shelf.shelfname;
            ViewBag.CategoryId = new SelectList(cats3, "categoryId", "name");
            return View(cats);
        }


        [RedirectingAction]
        public ActionResult ListOfRating(int id)
        {
            HttpResponseMessage response2 = Client.GetAsync("getShelfById/" + id).Result;
            Shelf shelf;
            if (response2.IsSuccessStatusCode)
            {
                shelf = response2.Content.ReadAsAsync<Shelf>().Result;
            }
            else
            {
                shelf = null;

            }
            HttpResponseMessage response = Client.GetAsync("getAllRating").Result;
            IEnumerable<ShelfRating> shelfRatings;

            if (response.IsSuccessStatusCode)
            {
                shelfRatings = response.Content.ReadAsAsync<IEnumerable<ShelfRating>>().Result;

            }
            else
            {
                shelfRatings = null;

            }
            List<ShelfRating> ratings = new List<ShelfRating> { };


            foreach (ShelfRating r in shelfRatings)
            {
                if (r.shelf.shelfId == shelf.shelfId)
                    ratings.Add(r);


            }
            ViewBag.shelfname = shelf.shelfname;

            return View(ratings);
        }
        // GET: Shelf/Details/5
        [RedirectingAction]
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = Client.GetAsync("getShelfById/" + id).Result;
            Shelf shelf;

            if (response.IsSuccessStatusCode)
            {
                shelf = response.Content.ReadAsAsync<Shelf>().Result;
            }
            else
            {
                shelf = null;

            }
            return View(shelf);
        }

        // GET: Shelf/Create
        [RedirectingAction]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shelf/Create
        [HttpPost]
        public ActionResult Create(Shelf shelf)
        {
            try
            {
                var APIresponse = Client.PostAsJsonAsync<Shelf>(baseAddress + "addShelf", shelf).GetAwaiter().GetResult();
                // TODO: Add insert logic here
                String message = APIresponse.ToString();
                if (APIresponse.IsSuccessStatusCode)
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;

                    return RedirectToAction("Index");
                }
                else
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
                    TempData["message"] = response.message;
                    return RedirectToAction("Create");
                }
            }

            catch
            {
                return View();
            }
        }

        // GET: Shelf/Edit/5
        [RedirectingAction]
        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Shelf/Edit/5
        [RedirectingAction]
        public ActionResult AddRate(AddRate r)

        {
           // addShelfRating / 1 / 1
          
            var APIresponse = Client.PostAsJsonAsync<Shelf>(baseAddress + "addShelfRating/" + r.id + "/" + r.rate,null).GetAwaiter().GetResult();
            Debug.WriteLine(APIresponse.StatusCode);
            return RedirectToAction("ListShelfs");
        }
       
        [RedirectingAction]
        [HttpPost]
        public ActionResult Edit(Shelf shelf,int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
             
              

                
                ViewBag.name = shelf.shelfname;
                Debug.WriteLine(shelf.shelfname);
                Debug.WriteLine(id);
                // TODO: Add update logic here
                shelf.shelfId = id;

                var APIresponse = Client.PutAsJsonAsync<Shelf>(baseAddress + "updateShelf", shelf).ContinueWith(putTask => putTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shelf/Delete/5
        [RedirectingAction]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = Client.GetAsync("getShelfById/" + id).Result;
            Shelf shelf;

            if (response.IsSuccessStatusCode)
            {
                shelf = response.Content.ReadAsAsync<Shelf>().Result;
            }
            else
            {
                shelf = null;

            }
            return View(shelf);
        }



        [RedirectingAction]
        public ActionResult DeleteCategory(int id, int shelfId)
        {
            try
            {
                Debug.WriteLine(shelfId);
                Debug.WriteLine(id);


                var APIresponse = Client.PutAsJsonAsync<Shelf>(baseAddress + "daffecterCategoryAShelf/" + id + "/"+ shelfId, null).GetAwaiter().GetResult();
                // TODO: Add insert logic here
                String message = APIresponse.ToString();
                if (APIresponse.IsSuccessStatusCode)
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;

                    return RedirectToAction("ListOfCategory/"+ shelfId);
                }
                else
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;
                  
                    return RedirectToAction("ListOfCategory/"+ shelfId);
                }

               
            }
            catch
            {
                return RedirectToAction("ListOfCategory/"+ shelfId);
            }
        }
        [RedirectingAction]
        public ActionResult AddCategory(int shelfId)

        {
            ViewBag.shelfid = shelfId;
            HttpResponseMessage response = Client.GetAsync("getShelfById/" + shelfId).Result;
            Shelf shelf;
            HttpResponseMessage response2 = Client.GetAsync("getAllCategory").Result;
            IEnumerable<Category> cat1;
           
            if (response2.IsSuccessStatusCode)
            {
                cat1 = response2.Content.ReadAsAsync<IEnumerable<Category>>().Result;
            }
            else
            {
                cat1 = null;
            }
          


            if (response.IsSuccessStatusCode)
            {
                shelf = response.Content.ReadAsAsync<Shelf>().Result;
            }
            else
            {
                shelf = null;

            }
            IList<Category> cat2 = new List<Category> { };
            IEnumerable<Category> cats3;
         
            cats3 = shelf.category;
            foreach (Category c in cat1)
            {
                foreach (Category c2 in cats3)
                {
                    if(c.categoryId!=c2.categoryId)
                    {
                        Debug.WriteLine("add" + c.name + c2.name);
                        cat2.Add(c);
                    }
                }


            }
            ViewBag.id = shelf.shelfId;
            ViewBag.shelfname = shelf.shelfname;
            ViewBag.CategoryId = new SelectList(cat2, "categoryId", "name");
            return View();
        }
        [RedirectingAction]
        [HttpPost]
        public ActionResult AddCategory(FormCollection collection, int shelfId)
        {
            try
            {


                var value = collection["category"];

                Debug.WriteLine(value);

                var APIresponse = Client.PutAsJsonAsync<Shelf>(baseAddress + "affecterCategoryAShelf/" + value + "/" + shelfId, null).GetAwaiter().GetResult();
                // TODO: Add insert logic here
                String message = APIresponse.ToString();
                if (APIresponse.IsSuccessStatusCode)
                {
                    JwtResponse response = APIresponse.Content.ReadAsAsync<JwtResponse>().Result;

                    return RedirectToAction("ListOfCategory/" + shelfId);
                }
                else
                {
                    return RedirectToAction("ListOfCategory/" + shelfId);
                }


            }
            catch
            {
                return RedirectToAction("ListOfCategory/" + shelfId);
            }
        }

        // POST: Shelf/Delete/5
        [RedirectingAction]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var APIresponse = Client.DeleteAsync(baseAddress + "deleteShelfById/" + id).ContinueWith(deleteTask => deleteTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }
    }
}
