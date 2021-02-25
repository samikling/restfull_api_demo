using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHarjoituskoodi_1.Models;

namespace WebApiHarjoituskoodi_1.Controllers
{
    [Route("omareitti/[controller]")]
    [ApiController]
    public class HenkilotController : ControllerBase
    {
        [HttpGet]
        [Route("merkkijono/{nimi}")]
        public string Merkkijono(string nimi)
        {
            return "Päivää " + nimi +"!";
        }
        [HttpGet]
        [Route("paivamaara")]
        public string Paivamaara()
        {
            return DateTime.Now.ToString();
        }
        [HttpGet]
        [Route("olio")]
        public Henkilo Olio(string nimi)
        {
            return new Henkilo()
            {
                Nimi = "Paavo Pesusieni",
                Osoite = "Simpukkalaakso 3",
                Ika = 11
            };
        }
        [HttpGet]
        [Route("oliolista")]
        public List<Henkilo> OlioLista()
        {
                List<Henkilo> henkilot = new List<Henkilo>()
                {
                    new Henkilo()
                    {
                    Nimi = "Paavo Pesusieni",
                    Osoite = "Simpukkalaakso 3",
                    Ika = 11
                    },
                    new Henkilo()
                    {
                    Nimi = "Liisa Pesusieni",
                    Osoite = "Simpukkalaakso 3",
                    Ika = 12
                    },
                    new Henkilo()
                    {
                    Nimi = "Patrik Meritähti",
                    Osoite = "Simpukkalaakso 5",
                    Ika = 8
                    }
                };
            return henkilot;
        }
    }
}
