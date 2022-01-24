using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class LiciterRepository : ILiciterRepository
    {

        public static List<LiciterModel> Liciteri { get; set; } = new List<LiciterModel>();

        public LiciterRepository()
        {
            FillData();
        }

        private void FillData()
        {
            //napuniti podacima
        }

        public LiciterConfirmation CreateLiciter(LiciterModel liciter)
        {
            liciter.LiciterId = Guid.NewGuid();
            Liciteri.Add(liciter);
            LiciterModel liciter2 = GetLiciterById(liciter.LiciterId);

            return new LiciterConfirmation()
            {
                LiciterId = liciter2.LiciterId
            }; 
            
        }

        public void DeleteLiciter(Guid LiciterId)
        {
            Liciteri.Remove(Liciteri.FirstOrDefault(e => e.LiciterId == LiciterId));
        }

        public LiciterModel GetLiciterById(Guid LiciterId)
        {
            return Liciteri.FirstOrDefault(e => e.LiciterId == LiciterId);
        }

        public List<LiciterModel> GetLiciters()
        {
            throw new NotImplementedException(); // nzm kako
        }

        public LiciterConfirmation UpdateLiciter(LiciterModel liciter)
        {
            LiciterModel liciter2 = GetLiciterById(liciter.LiciterId);

            liciter2.Kupac = liciter.Kupac;
            liciter2.OvlascenoLice = liciter.OvlascenoLice;
            liciter2.LiciterId = liciter.LiciterId;

            return new LiciterConfirmation
            {
                LiciterId = liciter2.LiciterId
            };
        }
    }
}
