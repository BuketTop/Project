using Newtonsoft.Json;
using Project.Model.Context;
using Project.Model.Entity;
using Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Xml;

namespace Project.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AlisMatbaaDbContext dbContext = new AlisMatbaaDbContext();

        [HttpGet]
        public ActionResult OrderList()
        {
            List<Order> orderList = dbContext.Order
                .Include("OrderDetail")
                .Include("OrderStatus")
                .OrderByDescending(o => o.OrderDate)
                .ToList();


            return View(orderList);
        }

        [HttpGet]
        public ActionResult OrderDetail(int id)
        {
            Order order = dbContext.Order
                .Where(o => o.Id == id)
                .Include("OrderDetail")
                .Include("OrderDetail.Product")
                .Include("OrderDetail.ProductOption")
                .Include("OrderDetail.Currency")
                .Include("OrderStatus")
                .FirstOrDefault();

            List<SelectListItem> orderStatusList = dbContext.OrderStatus
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                })
                .ToList();
            TempData["OrderStatusList"] = orderStatusList;

            return View(order);
        }

        [HttpGet]
        public ActionResult NewOrder()
        {
            NewOrderViewModel newOrderViewModel = new NewOrderViewModel()
            {
                orderStatusList = dbContext.OrderStatus.ToList(),
                productList = dbContext.Product.Include("ProductOption").Include("ProductOption.Currency").ToList(),
            };


            return View(newOrderViewModel);
        }

        [HttpPost]
        public ActionResult NewOrder(Order order)
        {
            OrderStatus orderStatus = dbContext.OrderStatus.FirstOrDefault(f => f.Name == "Yeni");

            order.OrderStatusId = orderStatus.Id;
            order.OrderDate = DateTime.Now;
            order.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            XmlTextReader xtrOkuyucu = new XmlTextReader("http://www.tcmb.gov.tr/kurlar/today.xml");
            XmlDocument xdDokuman = new XmlDocument();
            xdDokuman.Load(xtrOkuyucu);

            foreach (var item in order.OrderDetail)
            {
                if (item.ProductId <= 0)
                {
                    continue;
                }
                string currencyCode = dbContext.Currency.First(f => f.Id == item.CurrencyId).Name;


                XmlNode xn = xdDokuman.SelectSingleNode("/Tarih_Date/Currency[@Kod='" + currencyCode + "']");
                string strSatis = xn.ChildNodes[4].InnerText;

                item.TRYRate = decimal.Parse(strSatis);
                item.TRYPrice = item.Price * item.TRYRate;
                item.TRYTotal = item.TRYPrice * item.Quantity;
                order.OrderTotal += item.TRYTotal;
            }

            var willBeDeleted = order.OrderDetail.Where(q => q.ProductId <= 0).ToList();
            foreach (var item in willBeDeleted)
            {
                order.OrderDetail.Remove(item);
            }

            dbContext.Order.Add(order);
            dbContext.SaveChanges();

            SendMail("Alis Matbaa Sipariş Bildirimi", "Siparişiniz alınmıştır. Sipariş Numaranız:" + order.OrderNo, new List<string>()
            {
                order.Mail
            });

            return Redirect("/Order/OrderList");
        }

        [HttpPost]
        public ActionResult ChangeOrderStatus(int OrderStatusId, int Id)
        {
            Order order = dbContext.Order.FirstOrDefault(f => f.Id == Id);

            order.OrderStatusId = OrderStatusId;

            dbContext.Entry(order).State = EntityState.Modified;
            dbContext.SaveChanges();

            return Redirect("/Order/OrderList");
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult OrderControl()
        {
            return View(new Order());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult OrderControl(string OrderNo)
        {
            Order order = dbContext.Order
                .Where(o => o.OrderNo == OrderNo)
                .Include("OrderDetail")
                .Include("OrderDetail.Product")
                .Include("OrderDetail.ProductOption")
                .Include("OrderDetail.Currency")
                .Include("OrderStatus")
                .FirstOrDefault();

            if (order == null)
            {
                return View(new Order());
            }

            return View(order);
        }


        [HttpPost]
        public ActionResult GetProductOptionList(int ProductId)
        {
            List<ProductOption> productOptionList = dbContext.ProductOption.Where(x => x.ProductId == ProductId).ToList();


            return Json(productOptionList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProductOption(int Id)
        {
            ProductOption productOption = dbContext.ProductOption.Include("Currency").Where(x => x.Id == Id).FirstOrDefault();

            var returnObject = JsonConvert.SerializeObject(productOption, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(returnObject, JsonRequestBehavior.AllowGet);
        }

        public void SendMail(string Subject, string Body, List<string> To)
        {
            string fromMail = "alismatbaaproje@gmail.com";
            string fromDisplayName = "Alis Matbaa";
            string host = "smtp.gmail.com";
            int port = 587;
            string userName = "alismatbaaproje@gmail.com";
            string password = "fkgqlzqayipestna";
            bool IsEnabledSsl = true;

            if (To == null)
            {
                throw new ArgumentNullException("To");
            }

            if (To.Count == 0)
            {
                throw new ArgumentNullException("To empty");
            }

            MailMessage msg = new MailMessage()
            {
                Subject = Subject,
                Body = Body,
                From = new MailAddress(fromMail, fromDisplayName),
                IsBodyHtml = true,
                Priority = MailPriority.High
            };

            foreach (string item in To)
            {
                msg.To.Add(new MailAddress(item));
            }

            SmtpClient smtp = new SmtpClient(host, port);

            NetworkCredential AccountInfo = new NetworkCredential(userName, password);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = AccountInfo;
            smtp.EnableSsl = IsEnabledSsl;

            SmtpDeliveryMethod smtpDeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.DeliveryMethod = smtpDeliveryMethod;
            smtp.Send(msg);
        }
    }
}