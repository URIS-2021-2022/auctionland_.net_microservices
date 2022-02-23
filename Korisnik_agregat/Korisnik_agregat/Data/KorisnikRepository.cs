using AutoMapper;
using Korisnik_agregat.Entities;
using Korisnik_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Korisnik_agregat.Data
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly KorisnikContext context;
        private readonly IMapper mapper;
        public List<Korisnik> KorisnikList { get; set; } = new List<Korisnik>();

        public KorisnikRepository(KorisnikContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

            KorisnikList.AddRange(GetKorisnikList());

        }

        public List<Korisnik> GetKorisnikList()
        {
            return context.Korisnici.ToList();
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public KorisnikConfirmationDto CreateKorisnik(Korisnik korisnikModel)
        {
            korisnikModel.KorisnikId = Guid.NewGuid();

            var sifra = HashPassword(korisnikModel.Lozinka);

            korisnikModel.Lozinka = sifra.Item1;
            korisnikModel.Salt = sifra.Item2;

            context.Korisnici.Add(korisnikModel);
            context.SaveChanges();

            return mapper.Map<KorisnikConfirmationDto>(korisnikModel);
        }

        public void DeleteKorisnik(Guid KorisnikId)
        {
            Korisnik korisnik = GetKorisnikById(KorisnikId);

            if (korisnik == null)
            {
                throw new ArgumentNullException("KorisnikId");
            }

            context.Korisnici.Remove(korisnik);
            context.SaveChanges();

        }

        public Korisnik GetKorisnikById(Guid KorisnikId)
        {
            return context.Korisnici.FirstOrDefault(e => e.KorisnikId == KorisnikId);
        }

        public List<Korisnik> GetKorisniks()
        {
            return context.Korisnici.ToList();
        }

        public void UpdateKorisnik(Korisnik korisnikModel)
        {
            Korisnik k = context.Korisnici.FirstOrDefault(e => e.KorisnikId == korisnikModel.KorisnikId);

            if (k == null)
            {
                throw new EntryPointNotFoundException();
            }

            k.TipKorisnikaId = korisnikModel.TipKorisnikaId;
            k.Ime = korisnikModel.Ime;
            k.Prezime = korisnikModel.Prezime;
            k.KorisnickoIme = korisnikModel.KorisnickoIme;

            var sifra = HashPassword(korisnikModel.Lozinka);

            k.Lozinka = sifra.Item1;
            k.Salt = sifra.Item2;

            context.SaveChanges();
        }

        public bool UserWithCredentialsExists(string korisnickoIme, string lozinka)
        {
            Korisnik korisnik = KorisnikList.FirstOrDefault(k => k.KorisnickoIme == korisnickoIme);

            if (korisnik == null)
            {
                return false;
            }

            if (VerifyPassword(lozinka, korisnik.Lozinka, korisnik.Salt))
            {
                return true;
            }

            return false;
        }
        private Tuple<string, string> HashPassword(string lozinka)
        {
            var sBytes = new byte[lozinka.Length];

            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);

            var salt = Convert.ToBase64String(sBytes);
            var derivedBytes = new Rfc2898DeriveBytes(lozinka, sBytes, 100);

            return new Tuple<string, string>(Convert.ToBase64String(derivedBytes.GetBytes(256)), salt);
        }

        private bool VerifyPassword(string lozinka, string savedLozinka, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(lozinka, saltBytes, 100);

            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedLozinka;
        }
    }


    
}
