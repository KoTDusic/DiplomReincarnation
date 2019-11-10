using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ElectronDecanat.DatabaseConnection;
using ElectronDecanat.Models;
using FirebirdSql.Data.FirebirdClient;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace ElectronDecanat
{
    public class ElectronDecanatDb : DataConnection
    {
        public void CheckDatabaseCreated()
        {
            if (!File.Exists(DatabaseLocation.DatabaseFile))
            {
                FbConnection.CreateDatabase(new FireBirdSettings().ConnectionStrings.First().ConnectionString, false);
            }

            var provider = DataProvider.GetSchemaProvider().GetSchema(this);

            var existingTablesSet = provider.Tables.Select(schema => schema.TableName.ToLowerInvariant()).ToHashSet();
            if (!existingTablesSet.Contains(GetTypeTableName(typeof(RegistrationUser))))
            {
                this.CreateTable<RegistrationUser>();
            }
        }

        public ITable<RegistrationUser> Accounts => GetTable<RegistrationUser>();

        // ... other tables ...
        private string GetTypeTableName(Type type)
        {
            return (type.GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute)?.Name.ToLowerInvariant();
        }
    }
}