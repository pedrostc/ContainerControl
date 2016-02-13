namespace ContainerControl.Domain.Model
{
    public class CodigoIso : ModelBase
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }

        public override void Populate(ModelBase obj)
        {
            CodigoIso newProps = obj as CodigoIso;

            Codigo = newProps.Codigo;
            Nome = newProps.Nome;
        }
    }
}
