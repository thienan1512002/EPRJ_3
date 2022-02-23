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
using Clinic_web_app.Setting;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Text;

namespace Clinic_web_app.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly ClinicDBContext _context;
        public const string CARTKEY = "cart";
        private readonly MailSettings _setting;
        public MedicinesController(ClinicDBContext context, IOptions<MailSettings> options)
        {
            _context = context;
            _setting = options.Value;
        }

        // GET: Medicines
        public async Task<IActionResult> Index(string? sort, int pageNumber = 1)
        {
            const int pageSize = 8;
            var clinicDBContext = _context.Medicines.Include(m => m.Brand).Where(x => x.Featured == true).OrderByDescending(x => x.DateCreate);
            if (sort == "l2h")
            {
                clinicDBContext = _context.Medicines.Include(m => m.Brand).Where(x => x.Featured == true).OrderBy(x => x.Price);
            }
            else if (sort == "h2l")
            {
                clinicDBContext = _context.Medicines.Include(m => m.Brand).Where(x => x.Featured == true).OrderByDescending(x => x.Price);
            }

            var data = await PaginatedList<Medicine>.CreateAsync(clinicDBContext, pageNumber, pageSize);
            return View(data);
        }
        public async Task<IActionResult> Filter(string? sort, string? type, int pageNumber = 1)
        {
            ViewBag.Type = type;
            const int pageSize = 8;
            var clinicDBContext = _context.Medicines.Include(m => m.Brand).Where(x => x.Type == type).OrderByDescending(x => x.DateCreate);
            if (sort == "l2h")
            {
                clinicDBContext = _context.Medicines.Include(m => m.Brand).Where(x => x.Type == type).OrderBy(x => x.Price);
            }
            else if (sort == "h2l")
            {
                clinicDBContext = _context.Medicines.Include(m => m.Brand).Where(x => x.Type == type).OrderByDescending(x => x.Price);
            }

            var data = await PaginatedList<Medicine>.CreateAsync(clinicDBContext, pageNumber, pageSize);
            return View(data);
        }


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
        [Route("addcart/{id}")]
        public IActionResult AddToCart(string id)
        {
            var medicine = _context.Medicines.Where(m => m.MedId == id)
                .FirstOrDefault();
            if (medicine == null)
            {
                return NotFound();
            }
            //xu li
            var cart = GetCartItems();
            var cartitem = cart.Find(m => m.medicine.MedId == id);
            if (cartitem != null)
            {
                //if exist +1
                cartitem.quantity++;
            }
            else
            {
                cart.Add(new CartItem() { quantity = 1, medicine = medicine });
            }
            //save cart to session
            SaveCartSession(cart);

            return RedirectToAction("Cart");
        }
        //add qty item
        public IActionResult AddQtyCart(string id, int qty)
        {
            var medicine = _context.Medicines.Where(m => m.MedId == id)
                .FirstOrDefault();
            if (medicine == null)
            {
                return NotFound();
            }
            //xu li
            var cart = GetCartItems();
            var cartitem = cart.Find(m => m.medicine.MedId == id);
            if (cartitem != null)
            {
                cartitem.quantity++;
            }
            //save cart to session
            SaveCartSession(cart);

            return RedirectToAction("Cart");
        }
        //minus qty item
        public IActionResult MinusQtyCart(string id, int qty)
        {
            var medicine = _context.Medicines.Where(m => m.MedId == id)
                .FirstOrDefault();
            if (medicine == null)
            {
                return NotFound();
            }
            //xu li
            var cart = GetCartItems();
            var cartitem = cart.Find(m => m.medicine.MedId == id);
            if (cartitem.quantity == 1)
            {
                cart.Remove(cartitem);
            }
            if (cartitem != null)
            {
                cartitem.quantity--;
            }
            //save cart to session
            SaveCartSession(cart);

            return RedirectToAction("Cart");
        }


        //remove item in cart
        [Route("/removecart/{id}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] string id)
        {
            //xu li
            var cart = GetCartItems();
            var cartitem = cart.Find(m => m.medicine.MedId == id);
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
        public async Task<IActionResult> CheckOut()
        {
            var session = HttpContext.Session;
            var checkCus = session.GetString("CustomerId");
            if (checkCus != null)
            {
                var customerAccount = await _context.CustomerAccounts
                .FirstOrDefaultAsync(m => m.CustomerId == checkCus);
                ViewBag.Customer = customerAccount;
            }
            return View(GetCartItems());
        }

        public async Task SendMail(string mailSent, int id)
        {
            var ecomerceMedDetail = await _context.EcomerceOrders
              .Include(e => e.EcomerceMedOrderDetails)
              .ThenInclude(e => e.Med)
              .ThenInclude(e => e.Brand)
              .FirstOrDefaultAsync(m => m.OrderId == id);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_setting.Mail);
            email.To.Add(MailboxAddress.Parse(mailSent));
            email.Subject = "Order Infomation";
            var builder = new BodyBuilder();
            StringBuilder sb = new StringBuilder();
            decimal total = 0;
            decimal grandTotal = 0;
            using (StreamReader SourceReader = System.IO.File.OpenText(@"wwwroot/mailTemplate/order.html"))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
                builder.HtmlBody = builder.HtmlBody.Replace("@orderAdress", ecomerceMedDetail.Address);
                builder.HtmlBody = builder.HtmlBody.Replace("@orderDate", ecomerceMedDetail.OrderDate.ToString());
                builder.HtmlBody = builder.HtmlBody.Replace("@orderId", ecomerceMedDetail.OrderId.ToString());
                foreach (var item in ecomerceMedDetail.EcomerceMedOrderDetails)
                {
                    total = item.Med.Price * Convert.ToDecimal(item.Quantity);
                    grandTotal += total;
                    sb.AppendFormat("<tr>" +
                        
                        "<td class='service'>{0}</td>" +
                        "<td class='desc'>{1}</td>" +
                        "<td>{2}</td>" +
                        "<td>{3}</td>" +
                        "</tr>",item.Med.MedName, item.Med.Price, item.Quantity, total);
                }
                builder.HtmlBody = builder.HtmlBody.Replace("@data", sb.ToString());
                builder.HtmlBody = builder.HtmlBody.Replace("@grandTotal", grandTotal.ToString());
            }
            email.Body = builder.ToMessageBody();
            using var stmp = new SmtpClient();
            stmp.Connect(_setting.Host, _setting.Port, SecureSocketOptions.StartTls);
            stmp.Authenticate(_setting.Mail, _setting.Password);
            await stmp.SendAsync(email);
            stmp.Disconnect(true);
        }
        [HttpPost]
        public async Task<IActionResult> doCheckout(EcomerceOrder order, string Name, string Phone, string Email, string Address)
        {
            if (ModelState.IsValid)
            {

                var cart = GetCartItems();
                var session = HttpContext.Session;
                if (session.GetString("CustomerId") == null)
                {
                    //order.CustomerId = "";
                    order.CustomerName = Name;
                    order.Address = Address;
                    order.Phone = Phone;
                }
                else
                {
                    var customer = await _context.CustomerAccounts
                    .FirstOrDefaultAsync(m => m.CustomerId == session.GetString("CustomerId"));
                    order.CustomerId = customer.CustomerId;
                    order.CustomerName = customer.CustomerName;
                    order.Address = customer.Address;
                    order.Phone = customer.Phone;

                }
                order.OrderDate = DateTime.Now;
                order.Status = "Pending";
                _context.Add(order);
                await _context.SaveChangesAsync();
                foreach (var item in cart)
                {
                    EcomerceMedOrderDetail detail = new EcomerceMedOrderDetail();
                    detail.OrderDetailId = order.OrderId;
                    detail.MedId = item.medicine.MedId;
                    detail.Quantity = item.quantity;
                    detail.Total = item.medicine.Price * item.quantity;
                    _context.Add(detail);
                    await _context.SaveChangesAsync();
                    await QuantityReduce(item.medicine.MedId, item.quantity);
                }
                Notification notification = new Notification();
                notification.NotyfName = Name + " has created an order";
                notification.DateCreate = DateTime.Now;
                notification.OrderId = order.OrderId;
                notification.IsRead = false;
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
                ClearCart();
                //if (session.GetString("CustomerId") != null)
                //{
                //    var customer = await _context.CustomerAccounts
                //   .FirstOrDefaultAsync(m => m.CustomerId == session.GetString("CustomerId"));
                //    await SendMail(customer.Email, order.OrderId);
                //}
            }
            return RedirectToAction("Success");
        }
        public IActionResult Success()
        {
            return View();
        }
        public async Task<IActionResult> QuantityReduce(string medId, int quantity)
        {
            var medicine = _context.Medicines.FirstOrDefault(m => m.MedId == medId);
            medicine.Quantity -= quantity;
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool MedicineExists(string id)
        {
            return _context.Medicines.Any(e => e.MedId == id);
        }
    }
}
