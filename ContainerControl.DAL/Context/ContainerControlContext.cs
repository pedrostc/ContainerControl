using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlTypes;
using ContainerControl.DAL.Mapping;
using ContainerControl.Domain.Model;

namespace ContainerControl.DAL.Context
{
    public class ContainerControlContext : DbContext
    {
        public ContainerControlContext() : base("ContainerControlContext") { }
        public DbSet<Container> Containers { get; set; }
        public DbSet<CodigoIso> CodigosIso { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CodigoIsoMap());
            modelBuilder.Configurations.Add(new ContainerMap());
        }

        public override int SaveChanges()
        {
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;
            foreach (var entry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            {
                var entity = entry.Entity as ModelBase;

                if (entity == null) continue;

                if (entry.State == EntityState.Added)
                    entity.CriadoEm = DateTime.Now;

                entity.ModificadoEm = DateTime.Now;
            }
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return 0;
        }
    }
}
