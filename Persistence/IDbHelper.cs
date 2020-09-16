using Domain.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IDbHelper
    {
        Task<IDataReader> ExecuteDataReader(string commandText, CommandType commandType, IEnumerable<IDataParameter> commandParameters = null);
        Task<IDataReader> ExecuteDataReader(string commandText, IEnumerable<IDataParameter> commandParameters);

        Task<object> ExecuteScalar(string commandText, CommandType commandType, IEnumerable<IDataParameter> commandParameters);
        Task<object> ExecuteScalar(string commandText);

        Task<int> ExecuteNonQuery(string commandText, CommandType commandType, IEnumerable<IDataParameter> commandParameters);
        Task<ExecuteNonQueryResult<T>> ExecuteNonQuery<T>(string commandText, CommandType commandType, IEnumerable<IDataParameter> commandParameters, string outParamName);
        Task<int> ExecuteNonQuery(string commandText, CommandType commandType);
        Task<int> ExecuteNonQuery(string commandText);
        Task<int> ExecuteNonQuery(string commandText, IEnumerable<IDataParameter> commandParameters);

        Task<string> GetConnectionString();
    }
}
