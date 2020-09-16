using Domain.Models;
using Microsoft.Data.SqlClient;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    public abstract class SqlHelper : IDbHelper
    {
        private const int _DefaultCommandTimeout = 2400;
        private readonly string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// This will return data reader based on the query.
        /// </summary>
        /// <remarks>It is the responsibility of the caller to clean up the returned DataReader.</remarks>
        /// <param name="commandText">The text to set as the CommandText.</param>
        /// <param name="parameters">The <see cref="IDataParameter"/>s to give to the SQL.</param>
        /// <returns>An <see cref="SqlDataReader"/>.</returns>
        public async Task<IDataReader> ExecuteDataReader(string commandText, IEnumerable<IDataParameter> commandParameters = null)
        {
            return await Task.Run(() =>
            {
                return ExecuteDataReader(commandText, CommandType.Text, commandParameters);
            });
        }

        /// <summary>
        /// This will return data reader based on the query.
        /// </summary>
        /// <remarks>It is the responsibility of the caller to clean up the returned DataReader.</remarks>
        /// <param name="commandText">The text to set as the CommandText.</param>
        /// <param name="commandType"></param>
        /// <param name="parameters">The <see cref="IDataParameter"/>s to give to the SQL.</param>
        /// <returns>An <see cref="SqlDataReader"/>.</returns>
        public async Task<IDataReader> ExecuteDataReader(
                 string commandText,
                 CommandType commandType,
                 IEnumerable<IDataParameter> commandParameters = null)
        {
            // NOTE: No usings because we are returning the DataReader.  When the data reader closes, it will close the
            // command and connection.
            SqlConnection connection = await CreateAndOpenConnection(_connectionString);
            SqlCommand command = await CreateCommand(connection, commandText, commandType, commandParameters);

            // When returning datareader the connection need to be open, so by using command behaviour 
            // "CommandBehavior.CloseConnection" this ensures that when datareader is used closed by the using 
            // function the connection will automatically gets closed.
            return await Task.FromResult(command.ExecuteReader(CommandBehavior.CloseConnection));
        }

        /// <summary>This will execute a query in the database.</summary>
        /// <param name="commandText">The query to execute</param>
        /// <returns>The first column of the first row in the result set.</returns>
        public async Task<object> ExecuteScalar(string commandText)
        {
            return await Task.Run(() =>
            {
                return ExecuteScalar(commandText, CommandType.Text, null);
            });
        }

        /// <summary>This will execute a query in the database.</summary>
        /// <param name="commandText">The query to execute</param>
        /// <param name="commandType">The <see cref="CommandType"/>.</param>
        /// <param name="commandParameters">The optional <see cref="IDataParameter"/>s.</param>
        /// <returns>The first column of the first row in the result set.</returns>
        public async Task<object> ExecuteScalar(
            string commandText,
            CommandType commandType,
            IEnumerable<IDataParameter> commandParameters = null
        )
        {
            using (var connection = await CreateAndOpenConnection(_connectionString))
            {
                using (var command = await CreateCommand(connection, commandText, commandType, commandParameters))
                {
                    return await Task.Run(() =>
                    {
                        return command.ExecuteScalar();
                    });
                }
            }
        }


        /// <summary>This will execute a query in the database.</summary>
        /// <param name="commandText">The query to execute</param>
        /// <returns>The number of rows affected</returns>
        public async Task<int> ExecuteNonQuery(string commandText)
        {
            return await Task.Run(() =>
            {
                return ExecuteNonQuery(commandText, CommandType.Text, null);
            });
        }

        /// <summary>This will execute a query in the database.</summary>
        /// <param name="commandText">The query to execute</param>
        /// <param name="commandParameters">The optional <see cref="IDataParameter"/>s.</param>
        /// <returns>The number of rows affected</returns>
        public async Task<int> ExecuteNonQuery(string commandText, IEnumerable<IDataParameter> commandParameters)
        {
            return await Task.Run(() =>
            {
                return ExecuteNonQuery(commandText, CommandType.Text, commandParameters);
            });
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public async Task<ExecuteNonQueryResult<T>> ExecuteNonQuery<T>(
           string commandText,
           CommandType commandType,
           IEnumerable<IDataParameter> commandParameters)
        {
            ExecuteNonQueryResult<T> result = new ExecuteNonQueryResult<T>();
            using (var connection = await CreateAndOpenConnection(_connectionString))
            {
                using (var command = await CreateCommand(connection, commandText, commandType, commandParameters))
                {
                    result.CommandResult = command.ExecuteNonQuery();

                    return await Task.Run(() =>
                    {
                        return result;
                    });
                }
            }
        }

        /// <summary>
        /// This will execute a query in the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <param name="outParamName">Output parameter name.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public async Task<ExecuteNonQueryResult<T>> ExecuteNonQuery<T>(
            string commandText,
            CommandType commandType,
            IEnumerable<IDataParameter> commandParameters,
            string outParamName)
        {
            ExecuteNonQueryResult<T> result = new ExecuteNonQueryResult<T>();
            using (var connection = await CreateAndOpenConnection(_connectionString))
            {
                //connection.ReloadTypes();
                using (var command = await CreateCommand(connection, commandText, commandType, commandParameters))
                {

                    result.CommandResult = command.ExecuteNonQuery();
                    result.Output = (T)command.Parameters[outParamName].Value;

                    return await Task.Run(() =>
                    {
                        return result;
                    });
                }
            }
        }

        /// <summary>This will execute a query in the database.</summary>
        /// <param name="commandText">The query to execute</param>
        /// <param name="commandType">The <see cref="CommandType"/>.</param>
        /// <returns>The number of rows affected</returns>
        public async Task<int> ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return await Task.Run(() =>
            {
                return ExecuteNonQuery(commandText, commandType, null);
            });
        }

        /// <summary>This will execute a query in the database.</summary>
        /// <param name="commandText">The query to execute</param>
        /// <param name="commandType">The <see cref="CommandType"/>.</param>
        /// <param name="commandParameters">The optional <see cref="IDataParameter"/>s.</param>
        /// <returns>The number of rows affected</returns>
        public async Task<int> ExecuteNonQuery(
            string commandText,
            CommandType commandType,
            IEnumerable<IDataParameter> commandParameters
        )
        {
            using (var connection = await CreateAndOpenConnection(_connectionString))
            {
                using (var command = await CreateCommand(connection, commandText, commandType, commandParameters))
                {
                    return await Task.Run(() =>
                    {
                        return command.ExecuteNonQuery();
                    });
                }
            }
        }

        /// <summary>Creates AND opens a new <see cref="SqlConnection"/> with the given connection string.</summary>
        /// <param name="connectionString">Connection string to use when connecting to DB.</param>
        /// <returns>An open <see cref="SqlConnection"/>.</returns>
        /// <remarks><see cref="SqlConnection"/> is <see cref="IDisposable"/>.  Callers of this function must be
        /// careful to dispose the returned connection properly.</remarks>
        public async Task<SqlConnection> CreateAndOpenConnection()
        {
            return await Task.Run(() =>
            {
                return CreateAndOpenConnection(_connectionString);
            });
        }

        /// <summary>
        /// Returns connection string.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetConnectionString()
        {
            return await Task.Run(() =>
            {
                return _connectionString;
            });

        }

        /// <summary>Creates AND opens a new <see cref="SqlConnection"/> with the given connection string.</summary>
        /// <param name="connectionString">Connection string to use when connecting to DB.</param>
        /// <returns>An open <see cref="SqlConnection"/>.</returns>
        /// <remarks><see cref="SqlConnection"/> is <see cref="IDisposable"/>.  Callers of this function must be
        /// careful to dispose the returned connection properly.</remarks>
        public async Task<SqlConnection> CreateAndOpenConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return await Task.Run(() =>
            {
                return connection;
            });
        }

        /// <summary>
        /// Creates a new <see cref="SqlCommand"/> with default options.  The default options are hard-coded.
        /// For now, the only option is the timeout, which is set to infinite.
        /// </summary>
        /// <param name="connection">The <see cref="SqlConnection"/> to create the command on.</param>
        /// <returns>An <see cref="SqlCommand"/>.</returns>
        public async Task<SqlCommand> CreateDefaultCommand(SqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            var command = connection.CreateCommand();
            command.CommandTimeout = _DefaultCommandTimeout;
            return await Task.Run(() =>
            {
                return command;
            });
        }

        /// <summary>Creates a <see cref="SqlCommand"/></summary>
        /// <remarks>NOTE: SqlComand is IDisposable</remarks>
        public async Task<SqlCommand> CreateCommand(SqlConnection connection, string text)
        {
            return await Task.Run(() =>
            {
                return CreateCommand(connection, text, CommandType.Text, null);
            });
        }

        /// <summary>
        /// Creates a <see cref="SqlCommand"/>
        /// </summary>
        /// <remarks>NOTE: SqlComand is IDisposable</remarks>
        public async Task<SqlCommand> CreateCommand(
            SqlConnection connection,
            string text,
            CommandType type,
            IEnumerable<IDataParameter> commandParameters = null)
        {
            var command = await CreateDefaultCommand(connection);
            command.CommandType = type;
            command.CommandText = text;
            if (commandParameters != null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }

            return await Task.Run(() =>
            {
                return command;
            });

        }
    }
}
