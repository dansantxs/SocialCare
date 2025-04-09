using System.Data;
using Npgsql;

public class DBConnection : IDisposable
{
    private NpgsqlConnection _connection;
    private NpgsqlTransaction _transaction;
    private string _connectionString = "Host=localhost;Database=SocialCare;Username=postgres;Password=postgres123;";

    public NpgsqlConnection Connection
    {
        get { return _connection; }
        set { _connection = value; }
    }

    public NpgsqlTransaction Transaction
    {
        get { return _transaction; }
    }

    public DBConnection()
    {
        _connection = new NpgsqlConnection(_connectionString);
    }

    public void Open()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
    }

    public void Close()
    {
        if (_connection.State == ConnectionState.Open)
        {
            _connection.Close();
        }
    }

    public void BeginTransaction()
    {
        Open();
        _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
        _transaction?.Commit();
        _transaction = null;
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction = null;
    }

    public int ExecuteCommand(string commandText)
    {
        Open();
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _connection, _transaction))
        {
            return command.ExecuteNonQuery();
        }
    }

    public object ExecuteScalar(string commandText)
    {
        Open();
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _connection, _transaction))
        {
            return command.ExecuteScalar();
        }
    }

    public DataTable ExecuteQuery(string query)
    {
        Open();
        using (NpgsqlCommand command = new NpgsqlCommand(query, _connection, _transaction))
        {
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        Close();
        _connection.Dispose();
    }
}