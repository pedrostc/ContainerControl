using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure.Annotations;

using ContainerControl.Domain.Model;

namespace ContainerControl.DAL.Mapping
{
    public class CodigoIsoMap : EntityTypeConfiguration<CodigoIso>
    {
        public CodigoIsoMap()
        {
            HasKey(c => c.Id);
            Property(c => c.Codigo).HasMaxLength(11).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_CodigoIsoCodigo", 1) { IsUnique = true }
                    )
                );
            Property(c => c.Nome).HasMaxLength(50).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_CodigoIsoNome", 1) { IsUnique = true }
                    )
                );

            Property(c => c.ModificadoEm)
                .IsRequired()
                .IsConcurrencyToken(true);
        }
    }
}
