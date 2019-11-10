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
using LinqToDB.SchemaProvider;

namespace ElectronDecanat
{
    public class ElectronDecanatDb : DataConnection
    {
        public ITable<RegistrationUser> Accounts => GetTable<RegistrationUser>();
        public ITable<Faculty> Faculties => GetTable<Faculty>();
        public ITable<Speciality> Specialties => GetTable<Speciality>();
        public ITable<Teacher> Teachers => GetTable<Teacher>();

        // ... other tables ...
        private string GetTypeTableName(Type type)
        {
            return (type.GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute)?.Name.ToLowerInvariant();
        }
    }
}