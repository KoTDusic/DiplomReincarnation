using System.IO;
using FirebirdSql.Data.FirebirdClient;

namespace ElectronDecanat.DatabaseConnection
{
    public static class DatabaseLocation
    {
        public static string BaseDirectory => Directory.GetCurrentDirectory();
        public static string DatabaseFile => $"{BaseDirectory}\\firebird_server\\database.fdb";
        public static string DatabaseLibsDirectory => $"{BaseDirectory}\\firebird_server\\fbclient.dll";

        public static string DatabaseConnectionString => new FbConnectionStringBuilder
        {
            Database = DatabaseFile,

            ServerType = FbServerType.Embedded,
            UserID = "SYSDBA",
            Password = "masterkey",
            ClientLibrary = DatabaseLibsDirectory
        }.ToString();

    }
}
