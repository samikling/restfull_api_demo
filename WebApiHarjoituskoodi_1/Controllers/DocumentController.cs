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
    public class DocumentController : ControllerBase
    {
        
        [HttpGet]
        [Route("{key}")]
         public ActionResult GetCustByCountry(string key)
        {
            NorthwindContext db = new NorthwindContext();
            if (key == "123")
            {
                List<Documentation> someCustomers = (from c in db.Documentations //Muutetaan kysely listaksi ennen returnia, niin ei tule typemismatch matoa kiusaamaan.
                                    where c.Keycode == key
                                    select c).ToList();
                return Ok(someCustomers);
            }
            else
            {

                return BadRequest("Date&Time: "+DateTime.Now.ToString() + "\nThe Documentation you were looking for is Missing...");
                
            }


        }
        
        
    }
}
