using System.Collections.Generic;
using System.Linq;
using LinqToDB.Configuration;

namespace FirebirdDatabaseProviders
{
    public class FireBirdSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "Firebird";
        public string DefaultDataProvider => "Firebird";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "Firebird",
                        ProviderName = "Firebird",
                        ConnectionString = DatabaseLocation.DatabaseConnectionString
                    };
            }
        }
    }
}