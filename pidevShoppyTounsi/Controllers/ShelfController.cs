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
        }
        // GET: Shelf
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
            List<Product> products= new List<Product> {}; ;
            IEnumerable<Category> cats;
         
            cats = shelf.category;
          
            foreach(Category cat in cats)
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


        public ActionResult ListOfCategory(int id ) 
        {
            ViewBag.shelfid = id;
            HttpResponseMessage response = Client.GetAsync("getShelfById/" + id).Result;
            Shelf shelf;
            HttpResponseMessage response2 = Client.GetAsync("category/getAllCategory").Result;
            IEnumerable<Category> cats2;
            IList<Category> cats3= new List<Category> { };
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
            foreach(Category c in cats2)
            {
                foreach(Category c2 in cats)
                {
                    if(c.categoryId!=c2.categoryId)
                    {
                        cats3.Add(c);
                    }
                }
            }
       
            ViewBag.shelfname = shelf.shelfname;
            ViewBag.ProductId = new SelectList(cats3, "categoryId", "name");
            return View(cats);
        }



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
                shelfRatings  = response.Content.ReadAsAsync<IEnumerable<ShelfRating>>().Result;

            }
            else
            {
                shelfRatings = null;

            }
            List<ShelfRating> ratings = new List<ShelfRating> { }; 
       

            foreach (ShelfRating r in shelfRatings)
            {
                if(r.shelf.shelfId==shelf.shelfId)
                    ratings.Add(r);
                    
                
            }
            ViewBag.shelfname = shelf.shelfname;

            return View(ratings);
        }
        // GET: Shelf/Details/5
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shelf/Edit/5
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

        // GET: Shelf/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        
     

             
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



        // POST: Shelf/Delete/5
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
