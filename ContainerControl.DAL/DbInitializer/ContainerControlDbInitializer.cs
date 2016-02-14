using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerControl.DAL.Context;

namespace ContainerControl.DAL.DbInitializer
{
    public class ContainerControlDbInitializer : CreateDatabaseIfNotExists<ContainerControlContext>
    { }
}
