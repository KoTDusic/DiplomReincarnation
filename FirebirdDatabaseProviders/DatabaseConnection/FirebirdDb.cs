using System;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Models;

namespace FirebirdDatabaseProviders
{
    public class FirebirdDb : DataConnection
    {
        public ITable<User> Accounts => GetTable<User>();
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