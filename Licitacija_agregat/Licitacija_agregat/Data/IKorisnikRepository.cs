﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Data
{
    public interface IKorisnikRepository
    {
        bool UserWithCredentialsExists(string korisnickoIme, string lozinka);

    }
}
