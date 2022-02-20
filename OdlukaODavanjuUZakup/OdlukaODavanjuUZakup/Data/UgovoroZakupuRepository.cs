using OdlukaODavanjuUZakup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdlukaODavanjuUZakup.Data
{
    public class UgovoroZakupuRepository : IUgovoroZakupuRepository
    {
        public static List<UgovoroZakupuModel> UgovorioZakupu { get; set; } = new List<UgovoroZakupuModel>();

        public UgovoroZakupuRepository ()
        {
            FillData();
        }
        private void FillData()
        {

        }
        public UgovoroZakupuConfirmation CreateUgovorOZakupu(UgovoroZakupuModel ugovoroZakupu)
        {
            ugovoroZakupu.UgovoroZakupuID = Guid.NewGuid();
            UgovorioZakupu.Add(ugovoroZakupu);
            UgovoroZakupuModel ugovor = GetUgovoriOZakupuById(ugovoroZakupu.UgovoroZakupuID);

            return new UgovoroZakupuConfirmation
            {
                UgovoroZakupuID = ugovor.UgovoroZakupuID,
                datum_potpisa = ugovor.datum_potpisa,
                zavodni_Broj = ugovor.zavodni_Broj
            };
        }

        public void DeleteUgovorOZakupu(Guid UgovoroZakupuId)
        {
            UgovorioZakupu.Remove(UgovorioZakupu.FirstOrDefault(e => e.UgovoroZakupuID == UgovoroZakupuId));
        }

        public List<UgovoroZakupuModel> GetUgovoriOZakupu(string zavodni_broj = null)
        {
            return (from e in UgovorioZakupu
                    where string.IsNullOrEmpty(zavodni_broj) || e.zavodni_Broj == zavodni_broj
                    select e).ToList();
        }

        public UgovoroZakupuModel GetUgovoriOZakupuById(Guid UgovoroZakupuId)
        {
            return UgovorioZakupu.FirstOrDefault(e => e.UgovoroZakupuID == UgovoroZakupuId);
        }

        public UgovoroZakupuConfirmation UpdateUgovorOZakupu(UgovoroZakupuModel ugovoroZakupu)
        {
            UgovoroZakupuModel ugovor = GetUgovoriOZakupuById(ugovoroZakupu.UgovoroZakupuID);

            ugovor.UgovoroZakupuID = ugovoroZakupu.UgovoroZakupuID;
            ugovor.mesto_potpisivanja = ugovoroZakupu.mesto_potpisivanja;
            ugovor.datum_potpisa = ugovoroZakupu.datum_potpisa;
            ugovor.datum_zavodjenja = ugovoroZakupu.datum_zavodjenja;
            ugovor.rokovi_dospeca = ugovoroZakupu.rokovi_dospeca;
            ugovor.rok_za_vracanje_zemljista = ugovoroZakupu.rok_za_vracanje_zemljista;
            ugovor.tip_garancije = ugovoroZakupu.tip_garancije;
            ugovor.zavodni_Broj = ugovoroZakupu.zavodni_Broj;
            ugovor.Javno_Nadmetanje = ugovoroZakupu.Javno_Nadmetanje;
            ugovor.lice = ugovoroZakupu.lice;
            ugovor.odluka = ugovoroZakupu.odluka;

            return new UgovoroZakupuConfirmation
            {
                UgovoroZakupuID = ugovor.UgovoroZakupuID,
                datum_potpisa = ugovor.datum_potpisa,
                zavodni_Broj = ugovor.zavodni_Broj
            };
        }
    }
}
