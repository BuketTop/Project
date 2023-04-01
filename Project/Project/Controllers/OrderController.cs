using Project.Model.Context;
using Project.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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
            List<OrderStatus> orderStatusList = dbContext.OrderStatus.ToList();


            return View(orderStatusList);
        }

        [HttpPost]
        public ActionResult NewOrder(Order order)
        {
            OrderStatus orderStatus = dbContext.OrderStatus.FirstOrDefault(f => f.Name == "Yeni");

            order.OrderStatusId = orderStatus.Id;
            order.OrderDate = DateTime.Now;
            order.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            foreach (var item in order.OrderDetail)
            {
                if (item.ProductName == null || item.ProductCode == null)
                {
                    continue;
                }

                order.OrderTotal += item.ProductPrice * item.Quantity;
            }

            dbContext.Order.Add(order);
            dbContext.SaveChanges();

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
                .Include("OrderStatus")
                .FirstOrDefault();

            if (order == null)
            {
                return View(new Order());
            }

            return View(order);
        }
    }
}