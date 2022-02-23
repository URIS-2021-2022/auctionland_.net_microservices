using Liciter___Agregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
  public  interface ILiciterRepository
    {
        List<LiciterModel> GetLiciters();

        LiciterModel GetLiciterById(Guid LiciterId);

        LiciterConfirmation CreateLiciter(LiciterModel liciter);

        LiciterConfirmation UpdateLiciter(LiciterModel liciter);

        void DeleteLiciter(Guid LiciterId);

        bool SaveChanges();
    }
}
