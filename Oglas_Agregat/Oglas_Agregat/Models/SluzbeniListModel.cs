﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oglas_Agregat.Models
{
    public class SluzbeniListModel
    {
        public Guid SluzbeniListId { get; set; }
        public DateTime DatumIzdanja { get; set; }
        public int BrojLista { get; set; }
    }
}