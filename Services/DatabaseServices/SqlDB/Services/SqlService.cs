using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlDB.Config;
using SqlDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SqlDB.Services
{
    public class SqlService : ISqlService
    {
        private readonly SqlDBSettings _config;

        public SqlService(ILogger<SqlService> log, IOptions<SqlDBSettings> config)
        {
            _config = config.Value;
        }

        public async Task<DataTable> ExecuteProcedure(string proc, Dictionary<string, object> parameters, bool isQuery)
        {
            DataTable response = new DataTable();
            using (SqlConnection conn = new SqlConnection(_config.ConnectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure})
                {
                    foreach (var param in parameters)
                    {
                        var _param = param.Value ?? DBNull.Value; 
                        cmd.Parameters.AddWithValue(param.Key, _param);
                    }
                    if (isQuery)
                        response.Load(await cmd.ExecuteReaderAsync());
                    else
                        await cmd.ExecuteNonQueryAsync(); 
                }
            }
            return response;
        }

        public async Task<DataTable> ExecuteQuery(string query)
        {
            DataTable response = new DataTable();
            using (SqlConnection conn = new SqlConnection(_config.ConnectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    response.Load(await cmd.ExecuteReaderAsync());
                }
            }
            return response;
        }

        public async Task ExecuteBulkCopy(DataTable table, string name)
        {
            using (SqlConnection conn = new SqlConnection(_config.ConnectionString))
            {
                await conn.OpenAsync();
                using (SqlBulkCopy blk = new SqlBulkCopy(conn))
                {
                    blk.DestinationTableName = name;
                    foreach (DataColumn col in table.Columns)
                        blk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    await blk.WriteToServerAsync(table);
                }
            }
        }
    }
}
