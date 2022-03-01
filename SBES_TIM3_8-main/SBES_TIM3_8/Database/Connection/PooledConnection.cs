using System.Data.SQLite;
using System.Data;


namespace Database.Connection
{
    public static class PooledConnection
    {
        public static IDbConnection GetConnection()
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = ConnectionParams.LOCAL_DATA_SOURCE;
            return new SQLiteConnection(builder.ConnectionString);
        }
    }
}
