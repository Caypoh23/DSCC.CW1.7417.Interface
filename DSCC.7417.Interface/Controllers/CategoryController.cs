using DSCC._7417.Interface.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSCC._7417.Interface.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Categories
        private readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:37013")
        };
        // GET: Home
        public async Task<ActionResult> Index()
        {
            List<Category> categoryInfo = new List<Category>();

            HttpResponseMessage Res = await _httpClient.GetAsync("api/Categories");

            if (Res.IsSuccessStatusCode)
            {
                var PrResponse = await Res.Content.ReadAsStringAsync();

                categoryInfo = JsonConvert.DeserializeObject<List<Category>>(PrResponse);
            }
            return View(categoryInfo);

        }
        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Category category = null;
            HttpResponseMessage Res = await _httpClient.GetAsync("api/Categories/" + id);

            if (Res.IsSuccessStatusCode)
            {
                var PrResponse = await Res.Content.ReadAsStringAsync();

                category = JsonConvert.DeserializeObject<Category>(PrResponse);
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Categories/Edit/5
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

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Categories/Delete/5
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
