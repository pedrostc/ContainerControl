using System;

namespace ContainerControl.Domain.Model
{
    public class Container : ModelBase
    {
        public Guid CodigoIsoId { get; set; }
        public CodigoIso CodigoIso { get; set; }
        public string NroContainer { get; set; }
        public double Tara { get; set; }
        public double CM { get; set; }
        public double Peso { get; set; }
        public DateTime Fabricacao { get; set; }
    }
}
