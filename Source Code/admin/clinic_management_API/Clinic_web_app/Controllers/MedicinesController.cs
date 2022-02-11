using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using PagedList;
using Newtonsoft.Json;

namespace Clinic_web_app.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly ClinicDBContext _context;
        public const string CARTKEY = "cart";
        public MedicinesController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: Medicines
        public IActionResult Index()
        {
            var clinicDBContext = _context.Medicines.Include(m => m.Brand).Where(x=>x.Featured==true).OrderByDescending(x=>x.DateCreate);
            return View(clinicDBContext.ToList());
        }
        //public async Task<IActionResult> SortType(string medType, int page=1)
        //{
        //    try
        //    {
        //        var pageSize = 12;
        //        var lsMed = _context.Medicines
        //            .AsNoTracking()
        //            .Where(x => x.Type == medType)
        //            .OrderByDescending(x => x.CreatedAt);
        //        PagedList<Medicine> models = new PagedList<Medicine>(lsMed, page, pageSize);
        //        ViewBag.CurrentPage = page;
        //        return View(models);
        //    }
        //    catch (Exception)
        //    {

        //        return RedirectToAction("Index", "Home");
        //    }
        //}
        //public async Task<IActionResult> SortPrice(int page = 1)
        //{
        //    try
        //    {
        //        var pageSize = 12;
        //        var lsMed = _context.Medicines
        //            .AsNoTracking()
        //            .OrderByDescending(x => x.Price);
        //        PagedList<Medicine> models = new PagedList<Medicine>(lsMed, page, pageSize);
        //        ViewBag.CurrentPage = page;
        //        return View(models);
        //    }
        //    catch (Exception)
        //    {

        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        // GET: Medicines/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .Include(m => m.Brand)
                .FirstOrDefaultAsync(m => m.MedId == id);
            var relatedMed = _context.Medicines
                .Include(x => x.Brand)
                .Where(x => x.Type == medicine.Type && x.MedId != id)
                .Take(3)
                .OrderByDescending(x => x.DateCreate)
                .ToList();
            ViewBag.RelatedMed = relatedMed;
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }
        //list cart
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }
        //remove cart from session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }
        //save cartItem to session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
        //add cart
        [Route("addcart/{medid}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] string medid)
        {
            var medicine = _context.Medicines.Where(m=>m.MedId ==medid)
                .FirstOrDefault();
            if(medicine == null)
            {
                return NotFound();
            }
            //xu li
            var cart = GetCartItems();
            var cartitem = cart.Find(m => m.medicine.MedId == medid);
            if (cartitem != null)
            {
                //if exist +1
                cartitem.quantity++;
            }
            else
            {
                cart.Add(new CartItem(){ quantity = 1, medicine = medicine });
            }
            //save cart to session
            SaveCartSession(cart);

            return RedirectToAction("Cart");
        }
        //remove item in cart
        [Route("/removecart/{medid}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute]string medid)
        {
            //xu li
            var cart = GetCartItems();
            var cartitem = cart.Find(m => m.medicine.MedId == medid);
            if (cartitem != null)
            {
                //exist, remove
                cart.Remove(cartitem);
            }
            SaveCartSession(cart);

            return RedirectToAction("Cart");
        }
        //update cart
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] string medid, [FromForm] int quantity)
        {
            //xu li
            var cart = GetCartItems();
            var cartitem = cart.Find(m => m.medicine.MedId == medid);
            if (cartitem != null)
            {
                //exist, +1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            //return nothing, to call ajax
            return Ok();
        }
        //View cart
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        //check out
        [Route("/checkout")]
        public IActionResult CheckOut(string id, int quantity)
        {

            return View();
        }

        private bool MedicineExists(string id)
        {
            return _context.Medicines.Any(e => e.MedId == id);
        }
    }
}
