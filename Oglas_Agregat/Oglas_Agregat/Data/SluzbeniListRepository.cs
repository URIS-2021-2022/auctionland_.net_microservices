using Oglas_Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Data
{
    public class SluzbeniListRepository : ISluzbeniListRepository
    {
        public static List<SluzbeniListModel> SluzbeniListovi { get; set; } = new List<SluzbeniListModel>();

        public SluzbeniListRepository()
        {
            FillData();
        }

        private void FillData()
        {

        }

        public SluzbeniListConfirmation CreateSluzbeniList(SluzbeniListModel sluzbeniListModel)
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

        public SluzbeniListModel GetSluzbeniListById(Guid SluzbeniListId)
        {
            return SluzbeniListovi.FirstOrDefault(e => e.SluzbeniListId == SluzbeniListId);
        }

        public List<SluzbeniListModel> GetSluzbeniListovi(int BrojLista = default)
        {
            return (from e in SluzbeniListovi
                    where BrojLista == default || e.BrojLista.Equals(BrojLista)
                    select e).ToList();
        }

        public SluzbeniListConfirmation UpdateSluzbeniList(SluzbeniListModel sluzbeniListModel)
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
