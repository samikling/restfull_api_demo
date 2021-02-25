using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHarjoituskoodi_1.Models;

namespace WebApiHarjoituskoodi_1.Controllers
{
    [Route("northwind/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        //Get all orders
        [HttpGet]
        [Route("")]
        public List<Order> GetAllOrders()
        {
            NorthwindContext db = new NorthwindContext();
            List<Order> orders = db.Orders.ToList();
            db.Dispose();
            return orders;
        }

        //Get single order by key
        [HttpGet]
        [Route("{key}")]
        public Order GetOneOrder(int key)
        {
            NorthwindContext db = new NorthwindContext();
            Order order = db.Orders.Find(key);
            db.Dispose();
            return order;
        }

        //Get orders by shipping country
        [HttpGet]
        [Route("country/{key}")]
        public ActionResult GetOrderByShippingCountry(string key)
        {
            List<Order> lista = new List<Order>();
            NorthwindContext db = new NorthwindContext();
            try
            {
                lista = (from c in db.Orders
                         where c.ShipCountry == key
                         select c).ToList();

                return Ok(lista);

            }
            catch (Exception)
            {

                return BadRequest("Virheellinen maatieto.");
            }
            finally
            {
                db.Dispose();
            }
        }
        
        //Create a new Order
        [HttpPost] //<-- filtteri, joka sallii vain POST-metodit
        [Route("")]// <-- Routen placeholder
        public ActionResult PostCreateNew([FromBody] Order order)
        {
            NorthwindContext db = new NorthwindContext(); //Tietokanta yhteytden muodostus
            try
            {
                db.Orders.Add(order);
                db.SaveChanges();

            }
            catch (Exception)
            {

                return BadRequest("Syötä tiedot ilman OrderID:tä.");
            }
            db.Dispose(); //Tietokannan vapautus
            return Ok("Tiedot tallennettu: "+order.OrderId + " | " + order.OrderDate + " | " + order.OrderDetails); //Palauttaa vastaluodun uuden tilauksen avainarvon
        }

        //Edit Order
        [HttpPut]//<-- Filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<--key == orderID
        public ActionResult PutEdit(int key, [FromBody] Order order)
        {
            NorthwindContext db = new NorthwindContext();
            try
            {
                Order o = db.Orders.Find(key);
                if (o != null)
                {
                    o.OrderDate = order.OrderDate;
                    o.ShipAddress = order.ShipAddress;
                    o.ShipCity = order.ShipCity;
                    o.ShipCountry = order.ShipCountry;
                    o.ShipPostalCode = order.ShipPostalCode;
                    o.ShipRegion = order.ShipRegion;
                    o.ShipVia = order.ShipVia;
                    o.ShipViaNavigation = order.ShipViaNavigation;
                    o.ShippedDate = order.ShippedDate;
                    o.Customer = order.Customer;
                    o.CustomerId = order.CustomerId;
                    o.Employee = order.Employee;
                    o.EmployeeId = order.EmployeeId;
                    o.Freight = order.Freight;
                    o.RequiredDate = order.RequiredDate;
                    o.OrderDetails = order.OrderDetails;

                    db.SaveChanges();

                    return Ok("Muokattu onnistuneesti: " + o.OrderId);

                }
                else
                {
                    return NotFound("Tilausta ei löytynyt.");
                }
            }
            catch (Exception)
            {

                return BadRequest("Hupsista, jotain meni pieleen.");
            }
            finally
            {
                db.Dispose();
            }
        }

        //Delete Order
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteOneOrder(int key)
        {
            NorthwindContext db = new NorthwindContext();
            Order o = db.Orders.Find(key);
            if (o != null)
            {
                db.Orders.Remove(o);
                db.SaveChanges();
                return Ok("Tilaus " + key + " poistettiin");
            }
            else
            {
                return NotFound("Tilausta " + key + " ei löydy.");
            }
        }
    }
}
