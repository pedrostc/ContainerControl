using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ContainerControl.DAL.Configuration
{
    public class SqlServerConfiguration : DbConfiguration
    {
        public SqlServerConfiguration()
        {
            SetProviderServices(
                SqlProviderServices.ProviderInvariantName,
                SqlProviderServices.Instance);
        }
    }
}
