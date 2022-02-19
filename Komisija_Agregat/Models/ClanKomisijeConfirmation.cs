using System;

namespace Komisija_Agregat.Models
{
    public class ClanKomisijeConfirmation
    {
        public Guid ClanId { get; set; }
        public string ImeClana { get; set;}
        public string PrezimeClana { get; set;}
        public string EmailClana { get; set; }
    }
}


