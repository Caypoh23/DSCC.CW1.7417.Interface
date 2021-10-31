using DSCC._7417.Interface.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DSCC._7417.Interface.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            List<Category> categoryInfo = new List<Category>();

            HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var PrResponse = await response.Content.ReadAsStringAsync();

                categoryInfo = JsonConvert.DeserializeObject<List<Category>>(PrResponse);
            }
            return View(categoryInfo);

        }


        // GET: Categories/Details/5
        // the method takes the details of a particular category using its id 
        public async Task<ActionResult> Details(int id)
        {
            Category category = null;
            HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Categories/" + id);

            if (response.IsSuccessStatusCode)
            {
                var PrResponse = await response.Content.ReadAsStringAsync();

                category = JsonConvert.DeserializeObject<Category>(PrResponse);
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Create
        public async Task<ActionResult> Create()
        {
            // create new category
            var category = new Category();

            HttpResponseMessage result = await HttpHelper.HttpClientHelper.GetAsync("api/Categories/");

            if (result.IsSuccessStatusCode)
            {
                await result.Content.ReadAsStringAsync();
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // POST: Categories/Create
        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                var result = await HttpHelper.HttpClientHelper.PostAsJsonAsync("api/Categories/", category);
                if (result.IsSuccessStatusCode)
                {
                    // if the category successfully added, then return in index page
                    return RedirectToAction("Index");
                }

                return View(category);
            }
            catch
            {
                return View(category);
            }
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Category category = null;
            HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Categories/" + id);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Category category)
        {
            try
            {
                var result = await HttpHelper.HttpClientHelper.PutAsJsonAsync("api/Categories/" + category.Id, category);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(category);
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Delete/5
        // gets id of an item and removes it from the list
        public async Task<ActionResult> Delete(int id)
        {

            Category category = null;
            HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Categories/" + id);

            if (response.IsSuccessStatusCode)
            {
                var PrResponse = await response.Content.ReadAsStringAsync();

                category = JsonConvert.DeserializeObject<Category>(PrResponse);
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Category category)
        {
            try
            {

                HttpResponseMessage response = await HttpHelper.HttpClientHelper.GetAsync("api/Categories/" + id);
                Category categoryEntity = null;
                if (response.IsSuccessStatusCode)
                {
                    var PrResponse = await response.Content.ReadAsStringAsync();
                    //Deserializing the response recieved from web api and storing into the category list
                    categoryEntity = JsonConvert.DeserializeObject<Category>(PrResponse);
                }

                var result = await HttpHelper.HttpClientHelper.DeleteAsync("api/Categories/" + category.Id);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(categoryEntity);
            }
            catch
            {
                return View();
            }
        }
    }
}
