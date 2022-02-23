using Korisnik_agregat.Entities;
using Korisnik_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik_agregat.Data
{
    public interface IKorisnikRepository
    {
        List<Korisnik> GetKorisniks();
        Korisnik GetKorisnikById(Guid KorisnikId);
        KorisnikConfirmationDto CreateKorisnik(Korisnik korisnikModel);
        void UpdateKorisnik(Korisnik korisnikModel);
        void DeleteKorisnik(Guid KorisnikId);
        bool UserWithCredentialsExists(string korisnickoIme, string lozinka);
        bool SaveChanges();

    }
}
