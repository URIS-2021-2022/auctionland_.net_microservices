using Oglas_Agregat.Entities;
using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public class SluzbeniListRepository : ISluzbeniListRepository
    {
        public static List<SluzbeniList> SluzbeniListovi { get; set; } = new List<SluzbeniList>();

        public SluzbeniListRepository()
        {
            FillData();
        }

        private void FillData()
        {
            SluzbeniListovi.AddRange(new List<SluzbeniList>
            {
                new SluzbeniList
                {
                    SluzbeniListId = Guid.Parse("00f78e6b-a2bb-43b5-b3bb-f5708d1a5129"),
                    DatumIzdanja = DateTime.Parse("01-01-2001"),
                    BrojLista = 33
                }
            });

        }

        public SluzbeniListConfirmation CreateSluzbeniList(SluzbeniList sluzbeniListModel)
        {
            sluzbeniListModel.SluzbeniListId = Guid.NewGuid();
            SluzbeniListovi.Add(sluzbeniListModel);
            var sluzbeniList = GetSluzbeniListById(sluzbeniListModel.SluzbeniListId);

            return new SluzbeniListConfirmation()
            {
                SluzbeniListId = sluzbeniList.SluzbeniListId,
                BrojLista = sluzbeniList.BrojLista
            };
        }


        public void DeleteSluzbeniList(Guid SluzbeniListId)
        {
            SluzbeniListovi.Remove(SluzbeniListovi.FirstOrDefault(e => e.SluzbeniListId == SluzbeniListId));
        }

        public SluzbeniList GetSluzbeniListById(Guid SluzbeniListId)
        {
            return SluzbeniListovi.FirstOrDefault(e => e.SluzbeniListId == SluzbeniListId);
        }

        public List<SluzbeniList> GetSluzbeniListovi(int BrojLista = default)
        {
            return (from e in SluzbeniListovi
                    where BrojLista == default || e.BrojLista.Equals(BrojLista)
                    select e).ToList();
        }

        public SluzbeniListConfirmation UpdateSluzbeniList(SluzbeniList sluzbeniListModel)
        {
            var sluzbeniList = GetSluzbeniListById(sluzbeniListModel.SluzbeniListId);

            sluzbeniList.SluzbeniListId = sluzbeniListModel.SluzbeniListId;
            sluzbeniList.DatumIzdanja = sluzbeniListModel.DatumIzdanja;
            sluzbeniList.BrojLista = sluzbeniListModel.BrojLista;

            return new SluzbeniListConfirmation()
            {
                SluzbeniListId = sluzbeniList.SluzbeniListId,
                BrojLista = sluzbeniList.BrojLista
            };
        }
    }
}
