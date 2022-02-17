using Licitacija_agregat.Entities;
using Licitacija_agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public class LicitacijaRepository : ILicitacijaRepository
    {
        public static List<Licitacija> Licitacije { get; set; } = new List<Licitacija>();

        public LicitacijaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            
        }

        public LicitacijaConfirmation CreateLicitacija(Licitacija licitacijaModel)
        {
            licitacijaModel.LicitacijaId = Guid.NewGuid();
            Licitacije.Add(licitacijaModel);
            var licitacija = GetLicitacijaById(licitacijaModel.LicitacijaId);

            return new LicitacijaConfirmation()
            {
                LicitacijaId = licitacija.LicitacijaId,
                Broj = licitacija.Broj,
                Datum = licitacija.Datum
            };
        }

        public void DeleteLicitacija(Guid LicitacijaId)
        {
            Licitacije.Remove(Licitacije.FirstOrDefault(e => e.LicitacijaId == LicitacijaId));
        }

        public Licitacija GetLicitacijaById(Guid LicitacijaId)
        {
            return Licitacije.FirstOrDefault(e => e.LicitacijaId == LicitacijaId);
        }

        public List<Licitacija> GetLicitacijas(DateTime datum = default)
        {
            return (from e in Licitacije
                    where e.Datum.Equals(datum) || datum == default
                    select e).ToList();
        }

        public LicitacijaConfirmation UpdateLicitacija(Licitacija licitacijaModel)
        {
            var licitacija = GetLicitacijaById(licitacijaModel.LicitacijaId);

            licitacija.LicitacijaId = licitacijaModel.LicitacijaId;
            licitacija.Broj = licitacijaModel.Broj;
            licitacija.Godina = licitacijaModel.Godina;
            licitacija.Datum = licitacijaModel.Datum;
            licitacija.Ogranicenje = licitacijaModel.Ogranicenje;
            licitacija.Korak_cene = licitacijaModel.Korak_cene;
            licitacija.Lista_dokumentacije_fizicka_lica = licitacijaModel.Lista_dokumentacije_fizicka_lica;
            licitacija.Lista_dokumentacije_pravna_lica = licitacijaModel.Lista_dokumentacije_pravna_lica;
            licitacija.Rok_za_dostavljanje_prijave = licitacijaModel.Rok_za_dostavljanje_prijave;

            return new LicitacijaConfirmation()
            {
                LicitacijaId = licitacija.LicitacijaId,
                Broj = licitacija.Broj,
                Datum = licitacija.Datum
            };
        }
    }
}
