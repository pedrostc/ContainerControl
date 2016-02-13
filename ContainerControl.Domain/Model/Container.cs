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
        public override void Populate(ModelBase obj)
        {
            Container newProps = obj as Container;

            NroContainer = newProps.NroContainer;
            Tara = newProps.Tara;
            CM = newProps.CM;
            Peso = newProps.Peso;
            Fabricacao = newProps.Fabricacao;
            CodigoIsoId = newProps.CodigoIsoId;
        }
    }
}
