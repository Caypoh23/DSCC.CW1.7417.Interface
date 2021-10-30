using DSCC._7417.Interface.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSCC._7417.Interface.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:37013")
        };
        // GET: Home
        public async Task<ActionResult> Index()
        {
            List<Product> ProdInfo = new List<Product>();

            HttpResponseMessage Res = await _httpClient.GetAsync("api/Products");

            if (Res.IsSuccessStatusCode)
            {
                var PrResponse = await Res.Content.ReadAsStringAsync();

                ProdInfo = JsonConvert.DeserializeObject<List<Product>>(PrResponse);
            }
            return View(ProdInfo);

        }


        // GET: Product/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Product product = null;
            HttpResponseMessage Res = await _httpClient.GetAsync("api/Products/" + id);

            if (Res.IsSuccessStatusCode)
            {
                var PrResponse = await Res.Content.ReadAsStringAsync();

                product = JsonConvert.DeserializeObject<Product>(PrResponse);
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }


        // GET: Product/Create
        public async Task<ActionResult> Create()
        {
            List<Category> categories = new List<Category>();

            HttpResponseMessage Res = await _httpClient.GetAsync("api/Products/categories");

            if (Res.IsSuccessStatusCode)
            {
                var PrResponse = await Res.Content.ReadAsStringAsync();

                categories = JsonConvert.DeserializeObject<List<Category>>(PrResponse);
            }
            var productViewModel = new Product();
            productViewModel.Categories = new SelectList(categories, "Id", "CategoryName");
            return View(productViewModel);
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync("api/Products/", product);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }
        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Product product = null;
            HttpResponseMessage Res = await _httpClient.GetAsync("api/Products/" + id);
            List<Category> categories = new List<Category>();

            HttpResponseMessage categoriesRes = await _httpClient.GetAsync("api/Products/categories");

            if (categoriesRes.IsSuccessStatusCode)
            {
                var PrResponse = await categoriesRes.Content.ReadAsStringAsync();

                categories = JsonConvert.DeserializeObject<List<Category>>(PrResponse);
            }
            if (Res.IsSuccessStatusCode)
            {
                var PrResponse = await Res.Content.ReadAsStringAsync();

                product = JsonConvert.DeserializeObject<Product>(PrResponse);
                product.Categories = new SelectList(categories, "Id", "CategoryName");
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Product prod)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync("api/Products/" + prod.Id, prod);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(prod);
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            Product product = null;
            HttpResponseMessage Res = await _httpClient.GetAsync("api/Products/" + id);

            if (Res.IsSuccessStatusCode)
            {
                var PrResponse = await Res.Content.ReadAsStringAsync();

                product = JsonConvert.DeserializeObject<Product>(PrResponse);
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }
        // POST: Product/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Product prod)
        {
            try
            {

                HttpResponseMessage Res = await _httpClient.GetAsync("api/Products/" + id);
                Product product = null;
                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = await Res.Content.ReadAsStringAsync();
                    //Deserializing the response recieved from web api and storing into the Product list
                    product = JsonConvert.DeserializeObject<Product>(PrResponse);
                }

                var result = await _httpClient.DeleteAsync("api/Products/" + prod.Id);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(product);
            }
            catch
            {
                return View();
            }
        }
    }
}
