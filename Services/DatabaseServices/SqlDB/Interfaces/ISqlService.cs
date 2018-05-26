using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SqlDB.Interfaces
{
    public interface ISqlService
    {
        Task<DataTable> ExecuteProcedure(string proc, Dictionary<string, object> parameters, bool isQuery);
        Task<DataTable> ExecuteQuery(string query);
        Task ExecuteBulkCopy(DataTable table, string name);
    }
}
