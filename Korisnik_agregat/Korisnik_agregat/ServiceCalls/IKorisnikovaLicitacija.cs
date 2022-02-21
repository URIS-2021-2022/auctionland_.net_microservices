using Korisnik_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.ServiceCalls
{
    public interface IKorisnikovaLicitacija
    {
        public bool GetKorisnikoveLicitacije(List<KorisnikovaLicitacijaDto> licitacije);
    }
}
