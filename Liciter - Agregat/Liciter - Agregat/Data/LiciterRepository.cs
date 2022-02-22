using AutoMapper;
using Liciter___Agregat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Data
{
    public class LiciterRepository : ILiciterRepository
    {

        private readonly DataBaseContext context;
        private readonly IMapper mapper;

        public LiciterRepository(DataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public LiciterConfirmation CreateLiciter(LiciterModel liciter)
        {
            var createdEntity = context.Add(liciter);
            return mapper.Map<LiciterConfirmation>(createdEntity.Entity);

        }

        public void DeleteLiciter(Guid LiciterId)
        {
            var liciter = GetLiciterById(LiciterId);
            context.Remove(liciter);
        }

        public LiciterModel GetLiciterById(Guid LiciterId)
        {
            return context.Liciteri.Include(k=>k.Kupac).Include(o=>o.OvlascenoLice).FirstOrDefault(e => e.LiciterId == LiciterId);
        }

        public List<LiciterModel> GetLiciters()
        {
            return context.Liciteri.Include(k => k.Kupac).Include(o => o.OvlascenoLice).ToList();
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
