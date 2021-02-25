using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiHarjoituskoodi_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        [Route("{key}")]
        public string GetWeather(string key)
        { WebClient client = new WebClient();
            try
            { 
                string data = client.DownloadString("https://www.supersaa.fi/suomi/" + key+"/"); //Ei suostu toimimaan videoilla näytetyn esimerkin mukaan.
                int index = data.IndexOf("<div class=\"supers-lane-top-row-right-column\"");     //Kaikki Sää palvelut ovat päivittäneet sivujaan erillaisiksi.
                if (index > 0)                                                                   //Palauttaa kuitenkin postmaniin lämpötilan, preview välilehden alle.
                {
                    string weather = data.Substring(index + 0,150);
                    return weather; 
                }
                else
                {
                    return "Joku meni pieleen...";
                }
            }
            catch
            {
                return "(unknown)";
            }
            finally
            { 
                client.Dispose();
                
            }
        }
    }
}
