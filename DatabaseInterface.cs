using System.Data;
using System.Data.SqlClient;

namespace Booking.Server
{
    public class DatabaseInterface : IDatabaseInterface
    {
        public readonly string _connString;


        public DatabaseInterface(string connectionString)
        {
            _connString = connectionString;
        }

        public void Insert(string tableName, Dictionary<string, object> columns)
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                connection.Open();
                string columnNames = string.Join(", ", columns.Keys);
                string parameterNames = string.Join(", ", columns.Keys.Select(k => "@" + k));

                string query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var column in columns)
                    {
                        command.Parameters.AddWithValue("@" + column.Key, column.Value);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(string tableName, Dictionary<string, object> columns, string whereClause)
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                connection.Open();
                string setClause = string.Join(", ", columns.Keys.Select(k => k + " = @" + k));

                string query = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var column in columns)
                    {
                        command.Parameters.AddWithValue("@" + column.Key, column.Value);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
        public DataTable Select(string tableName, string[] columnNames) 
        {
            return Select(tableName, columnNames, "");
        }

        public DataTable Select(string tableName, string[] columnNames, string whereClause)
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                connection.Open();
                string columns = string.Join(", ", columnNames);
                string query = $"SELECT {columns} FROM {tableName}" + (string.IsNullOrEmpty(whereClause) ? string.Empty : $"WHERE {whereClause}");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public void Delete(string tableName, string whereClause)
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                connection.Open();
                string query = $"DELETE FROM {tableName} WHERE {whereClause}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}