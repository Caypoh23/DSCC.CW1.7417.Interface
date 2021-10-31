using DSCC._7417.Interface.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DSCC._7417.Interface.Controllers
{
    public class ProductController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            List<Product> productInfo = new List<Product>();

            HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Products");

            if (response.IsSuccessStatusCode)
            {
                var PrResponse = await response.Content.ReadAsStringAsync();

                productInfo = JsonConvert.DeserializeObject<List<Product>>(PrResponse);
            }

            return View(productInfo);
        }


        // GET: Product/Details/5
        // the method takes the details of a particular product using its id 
        public async Task<ActionResult> Details(int id)
        {
            Product product = null;
            HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Products/" + id);

            if (response.IsSuccessStatusCode)
            {
                var PrResponse = await response.Content.ReadAsStringAsync();

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

            HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Products/categories");

            if (response.IsSuccessStatusCode)
            {
                var productResponse = await response.Content.ReadAsStringAsync();

                categories = JsonConvert.DeserializeObject<List<Category>>(productResponse);
            }
            // create new product
            var productViewModel = new Product();
            // category was also included to be able to select a particular category from dropdown list
            // select list was used to implement this logic 
            productViewModel.Categories = new SelectList(categories, "Id", "CategoryName");
            return View(productViewModel);
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                var result = await HttpHelper.HttpClientHelper.PostAsJsonAsync("api/Products/", product);
                if (result.IsSuccessStatusCode)
                {
                    // go to index page if a new product is added
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
            HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Products/" + id);
            List<Category> categories = new List<Category>();

            // response is also made for products category
            // this allows to get a list of categories from api
            // get method for categories was created in products controller in api to get all the categories
            HttpResponseMessage categoriesRes = await HttpHelper.HttpClientHelper.GetAsync("api/Products/categories");

            if (categoriesRes.IsSuccessStatusCode)
            {
                var categoriesResponse = await categoriesRes.Content.ReadAsStringAsync();

                categories = JsonConvert.DeserializeObject<List<Category>>(categoriesResponse);
            }
            if (response.IsSuccessStatusCode)
            {
                var productResponse = await response.Content.ReadAsStringAsync();
                // get the product and convert it to json object
                product = JsonConvert.DeserializeObject<Product>(productResponse);
                product.Categories = new SelectList(categories, "Id", "CategoryName");
            }
            else
            {
                // return to index if an item is added
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            try
            {
                var result = await HttpHelper.HttpClientHelper.PutAsJsonAsync("api/Products/" + product.Id, product);
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

        // GET: Product/Delete/5
        // gets id of an item and removes it from the list
        public async Task<ActionResult> Delete(int id)
        {

            Product product = null;
            HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Products/" + id);

            if (response.IsSuccessStatusCode)
            {
                var PrResponse = await response.Content.ReadAsStringAsync();

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

                HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Products/" + id);
                Product product = null;
                if (response.IsSuccessStatusCode)
                {
                    var PrResponse = await response.Content.ReadAsStringAsync();
                    //Deserializing the response recieved from web api and storing into the Product list
                    product = JsonConvert.DeserializeObject<Product>(PrResponse);
                }

                var result = await HttpHelper.HttpClientHelper.DeleteAsync("api/Products/" + prod.Id);
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
