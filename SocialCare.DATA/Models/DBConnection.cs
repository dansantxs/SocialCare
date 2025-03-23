using System;
using System.Data;
using Microsoft.Data.SqlClient;

public class DBConnection : IDisposable
{
    private SqlConnection _connection;
    private string _connectionString = "Data Source=DANIEL;Initial Catalog=SocialCare;Persist Security Info=True;User ID=sa;Password=1928;Encrypt=True;TrustServerCertificate=True";

    public SqlConnection Connection { get; internal set; }

    public DBConnection()
    {        
        _connection = new SqlConnection(_connectionString);
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

    public int ExecuteCommand(string commandText)
    {
        using (SqlCommand command = new SqlCommand(commandText, _connection))
        {
            Open();
            return command.ExecuteNonQuery();
        }
    }

    public DataTable ExecuteQuery(string query)
    {
        using (SqlCommand command = new SqlCommand(query, _connection))
        {
            Open();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }

    public void Dispose()
    {
        Close();
        _connection.Dispose();
    }
}