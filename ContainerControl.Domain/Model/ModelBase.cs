using System;

namespace ContainerControl.Domain.Model
{
    public abstract class ModelBase
    {
        public Guid Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime ModificadoEm { get; set; }

        public abstract void Populate(ModelBase obj);
    }
}
