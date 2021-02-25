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
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        //Hae kaikki asiakkaat
        public List<Employee> GetAllEmployees()
        {
            NorthwindContext db = new NorthwindContext();
            List<Employee> tekijä = db.Employees.ToList();
            return tekijä;
        }
        //Hae yksi asiakas primarykeyn perusteella ex. ALFKI
        [HttpGet]
        [Route("{key}")]
        public Employee GetOneEmployee(int key)
        {
            NorthwindContext db = new NorthwindContext();
            Employee tekijä = db.Employees.Find(key);
            return tekijä;
        }
        //Hae asiakas maatiedon perusteella || Maatiedon voi vaihtaa myös muuhun tietueeseen.
        [HttpGet]
        [Route("lastname/{key}")]
        public ActionResult GetProdByLastname(string key)
        {
            List<Employee> lista = new List<Employee>();
            NorthwindContext db = new NorthwindContext();
            try
            {
                lista = (from c in db.Employees
                         where c.LastName == key
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
        public ActionResult PostCreateNew([FromBody] Employee tekijä)
        {
            NorthwindContext db = new NorthwindContext(); //Tietokanta yhteytden muodostus
            try
            {
                db.Employees.Add(tekijä);
                db.SaveChanges();

            }
            catch (Exception)
            {

                return BadRequest("Syötä tiedot ilman employeeid:tä.");
            }
            db.Dispose(); //Tietokannan vapautus
            return Ok(tekijä.EmployeeId+" | "+tekijä.FirstName + " | " + tekijä.LastName); //Palauttaa vastaluodun uuden asiakkaan avainarvon

        }
        [HttpPut]//<-- Filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<--key == customerID
        public ActionResult PutEdit(int key, [FromBody] Employee tekijä)
        {
            NorthwindContext db = new NorthwindContext();
            try
            {
                Employee emp = db.Employees.Find(key);
                if (emp != null)
                {
                    emp.FirstName = tekijä.FirstName;
                    emp.LastName = tekijä.LastName;
                    emp.Photo = tekijä.Photo;
                    emp.HireDate = tekijä.HireDate;
                    emp.BirthDate = tekijä.BirthDate;
                    emp.City = tekijä.City;
                    emp.Country = tekijä.Country;
                    emp.EmployeeTerritories = tekijä.EmployeeTerritories;
                    emp.Title = tekijä.Title;
                    emp.ReportsTo = tekijä.ReportsTo;
                    emp.Notes = tekijä.Notes;
                    emp.Orders = tekijä.Orders;

                    db.SaveChanges();

                    return Ok(emp.EmployeeId);

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
        public ActionResult DeleteOneEmployee(int key)
        {
            NorthwindContext db = new NorthwindContext();
            Employee tekijä = db.Employees.Find(key);
            if (tekijä != null)
            {
                db.Employees.Remove(tekijä);
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