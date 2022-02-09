using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clinic_web_app.Controllers
{
    public class CustomersController : Controller
    {
        private readonly string uri = "http://localhost:58017/api/CustomerAccounts/";

        // GET: CustomersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            HttpClient client = new HttpClient();
            try
            {
                
                return View();
            }
            catch
            {
                return View();
            }
        }
        
        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string txtEmail, string txtPass)
        {
            HttpClient client = new HttpClient();
            var data = JsonConvert.DeserializeObject<IEnumerable<Customer>>(client.GetStringAsync(uri).Result);
            var cus = data.SingleOrDefault(c => c.Email == txtEmail && c.Password == txtPass);
            if (cus != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
}
