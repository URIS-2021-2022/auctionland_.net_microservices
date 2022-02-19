using System;

namespace Komisija_Agregat.Models
{
    public class KomisijaConfirmation
    {
        public Guid KomisijaId { get; set; }
        public PredsednikModel Predsednik { get; set; }
    }
}
