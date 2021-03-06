﻿using System;
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
    public class ContainerMap : EntityTypeConfiguration<Container>
    {
        public ContainerMap()
        {
            ToTable("Containers");
            HasKey(c => c.Id);
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(c => c.NroContainer)
                .HasMaxLength(11)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, 
                new IndexAnnotation(
                    new IndexAttribute("IX_NroContainer", 1) { IsUnique = true }
                    )
                );

            HasRequired(c => c.CodigoIso)
                .WithMany()
                .HasForeignKey(c => c.CodigoIsoId);

            Property(c => c.CriadoEm)   
                .IsRequired();

            Property(c => c.ModificadoEm)
                .IsRequired();
        }
    }
}
