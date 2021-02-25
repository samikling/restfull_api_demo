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
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        //Hae kaikki asiakkaat
        public List<Customer> GetAllCustomers()
        {
            NorthwindContext db = new NorthwindContext();
            List<Customer> asiakkaat = db.Customers.ToList();
            return asiakkaat;
        }
        //Hae yksi asiakas primarykeyn perusteella ex. ALFKI
        [HttpGet]
        [Route("{key}")]
        public Customer GetOneCustomer(string key)
        {
            NorthwindContext db = new NorthwindContext();
            Customer asiakas = db.Customers.Find(key);
            return asiakas;
        }
        //Hae asiakas maatiedon perusteella || Maatiedon voi vaihtaa myös muuhun tietueeseen.
        [HttpGet]
        [Route("country/{key}")]
        public List<Customer> GetCustByCountry(string key)
        {
            NorthwindContext db = new NorthwindContext();

            var someCustomers = from c in db.Customers
                                where c.Country == key
                                select c;
            return someCustomers.ToList();
        }
        [HttpPost] //<-- filtteri, joka sallii vain POST-metodit
        [Route("")]// <-- Routen placeholder
        public ActionResult PostCreateNew([FromBody] Customer asiakas)
        {
            NorthwindContext db = new NorthwindContext(); //Tietokanta yhteytden muodostus
            try
            {
                db.Customers.Add(asiakas);
                db.SaveChanges();

            }
            catch (Exception)
            {

                return BadRequest("Jokin meni pieleen asiakasta lisättäessä.\nOta yhteyttä Guruun!");
            }
            db.Dispose(); //Tietokannan vapautus
            return Ok(asiakas.CustomerId); //Palauttaa vastaluodun uuden asiakkaan avainarvon

        }
        [HttpPut]//<-- Filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<--key == customerID
        public ActionResult PutEdit(string key, [FromBody] Customer asiakas)
        {
            NorthwindContext db = new NorthwindContext();
            try
            {
                Customer customer = db.Customers.Find(key);
                if (customer != null)
                {
                    customer.CompanyName = asiakas.CompanyName;
                    customer.ContactName = asiakas.ContactName;
                    customer.Address = asiakas.Address;
                    customer.City = asiakas.City;
                    customer.ContactTitle = asiakas.ContactTitle;
                    customer.Country = asiakas.Country;
                    customer.Phone = asiakas.Phone;
                    customer.PostalCode = asiakas.PostalCode;
                    db.SaveChanges();

                    return Ok(customer.CustomerId);
                }
                else
                {
                    return NotFound("Asiakasta ei löytynyt.");
                }
            }
            catch (Exception)
            {

                return BadRequest("Hupsista");
            }
            finally
            {
                db.Dispose();
            }
        }
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteOneCustomer(string key)
        {
            NorthwindContext db = new NorthwindContext();
            Customer asiakas = db.Customers.Find(key);
            if (asiakas!=null)
            {
                db.Customers.Remove(asiakas);
                db.SaveChanges();
                return Ok("Asiakas " + key +" poistettiin");
            }
            else
            {
                return NotFound("Asiakasta " + key + " ei löydy.");
            }
        }
    }
}
