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
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        //Hae kaikki asiakkaat
        public List<Product> GetAllCustomers()
        {
            NorthwindContext db = new NorthwindContext();
            List<Product> tuotteet = db.Products.ToList();
            return tuotteet;
        }
        //Hae yksi asiakas primarykeyn perusteella ex. ALFKI
        [HttpGet]
        [Route("{key}")]
        public Product GetOneCustomer(int key)
        {
            NorthwindContext db = new NorthwindContext();
            Product tuote = db.Products.Find(key);
            return tuote;
        }
        //Hae asiakas maatiedon perusteella || Maatiedon voi vaihtaa myös muuhun tietueeseen.
        [HttpGet]
        [Route("country/{key}")]
        public ActionResult GetProdBySupplier(int key)
        {
            List<Product> lista = new List<Product>();
            NorthwindContext db = new NorthwindContext();
            try
            {
            lista = (from c in db.Products
                                where c.CategoryId == key
                                select c).ToList();
            
                return Ok(lista);

            }
            catch (Exception)
            {

                return BadRequest("Virheellinen id");
            }
            finally
            {
                db.Dispose();
            }
        }
        [HttpPost] //<-- filtteri, joka sallii vain POST-metodit
        [Route("")]// <-- Routen placeholder
        public ActionResult PostCreateNew([FromBody] Product tuote)
        {
            NorthwindContext db = new NorthwindContext(); //Tietokanta yhteytden muodostus
            try
            {
                db.Products.Add(tuote);
                db.SaveChanges();

            }
            catch (Exception)
            {

                return BadRequest("Syötä tiedot ilman productid:tä.");
            }
            db.Dispose(); //Tietokannan vapautus
            return Ok(tuote.ProductId); //Palauttaa vastaluodun uuden asiakkaan avainarvon

        }
        [HttpPut]//<-- Filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<--key == customerID
        public ActionResult PutEdit(int key, [FromBody] Product tuote)
        {
            NorthwindContext db = new NorthwindContext();
            try
            {
                Product product = db.Products.Find(key);
                if (product != null)
                {
                    product.ProductName = tuote.ProductName;
                    product.CategoryId = tuote.CategoryId;
                    product.Category = tuote.Category;
                    product.Discontinued = tuote.Discontinued;
                    product.SupplierId = tuote.SupplierId;
                    product.Supplier = tuote.Supplier;
                    product.QuantityPerUnit = tuote.QuantityPerUnit;
                    product.UnitPrice = tuote.UnitPrice;
                    product.UnitsInStock = tuote.UnitsInStock;
                    product.UnitsOnOrder = tuote.UnitsOnOrder;
                    product.ReorderLevel = tuote.ReorderLevel;
                    product.ImageLink = tuote.ImageLink;
                    db.SaveChanges();

                    return Ok(product.ProductId);
                }
                else
                {
                    return NotFound("Tuotetta ei löytynyt.");
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
        public ActionResult DeleteOneCustomer(int key)
        {
            NorthwindContext db = new NorthwindContext();
            Product tuote = db.Products.Find(key);
            if (tuote != null)
            {
                db.Products.Remove(tuote);
                db.SaveChanges();
                return Ok("Tuote " + key + " poistettiin");
            }
            else
            {
                return NotFound("Tuotetta " + key + " ei löydy.");
            }
        }
    }
}
