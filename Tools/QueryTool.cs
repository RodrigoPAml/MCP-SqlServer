using System.Data;
using Server.Utils;
using System.Text.Json;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using ModelContextProtocol.Server;

namespace Server.Tools
{
    [McpServerToolType]
    public static class EchoTool
    {
        /// <summary>
        /// Here is the tool for the ai to undestand the database structure.
        /// </summary>
        /// <returns></returns>
        [McpServerTool(), Description("Get a list of all tables with their respective columns (SQL SERVER).")]
        public static string GetTablesWithColumnsAndTypes()
        {
            FileLogger.Log("Called GetTablesWithColumnsAndTypes()");

            try
            {
                using var connection = new SqlConnection(Configuration.GetConnectionString());
                connection.Open();

                using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                using var command = new SqlCommand(@"
                    SELECT 
                        t.table_schema,
                        t.table_name, 
                        c.column_name, 
                        c.data_type 
                    FROM 
                        information_schema.tables t
                    JOIN 
                        information_schema.columns c 
                        ON t.table_name = c.table_name
                        AND t.table_schema = c.table_schema
                    WHERE 
                        t.table_type = 'BASE TABLE';",
                    connection, 
                    transaction
                );

                using var reader = command.ExecuteReader();

                var tables = new Dictionary<string, List<Dictionary<string, string>>>();

                while (reader.Read())
                {
                    var schemaName = reader.GetString(0);
                    var tableName = reader.GetString(1);
                    var columnName = reader.GetString(2);
                    var columnType = reader.GetString(3);

                    if (!tables.ContainsKey(tableName))
                        tables[tableName] = new List<Dictionary<string, string>>();

                    tables[tableName].Add(new Dictionary<string, string>
                    {
                        {"schema", schemaName },
                        { "name", columnName },
                        { "type", columnType }
                    });
                }

                return JsonSerializer.Serialize(tables, new JsonSerializerOptions { WriteIndented = false });
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Here is the tool for the AI to execute a query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [McpServerTool, Description("Execute a query into the Microsoft SQL Server database and return the result as a JSON")]
        public static string Query(string query)
        {
            FileLogger.Log($"Called Query({query})");

            try
            {
                using var connection = new SqlConnection(Configuration.GetConnectionString());
                connection.Open();

                using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                using var command = new SqlCommand(query, connection, transaction);
                using var reader = command.ExecuteReader();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                var rows = new List<Dictionary<string, object>>();
                foreach (DataRow row in dataTable.Rows)
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        dict[column.ColumnName] = row[column] == DBNull.Value ? null : row[column];
                    }
                    rows.Add(dict);
                }

                return JsonSerializer.Serialize(rows, new JsonSerializerOptions { WriteIndented = false });

            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Here is the tool for the AI to test database connection
        /// </summary>
        /// <returns></returns>
        [McpServerTool, Description("Tests if the Microsoft SQL Server Database connection is good and alive.")]
        public static string HealthCheck()
        {
            FileLogger.Log("Called HealthCheck()");

            try
            {
                using var connection = new SqlConnection(Configuration.GetConnectionString());
                connection.Open();

                return "Connection is OK";
            }
            catch(Exception e)
            {
                return $"Connection failed: {e.Message}";
            }
        }
    }
}