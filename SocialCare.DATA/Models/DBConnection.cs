﻿using System.Data;
using Microsoft.Data.SqlClient;

public class DBConnection : IDisposable
{
    private SqlConnection _connection;
    private SqlTransaction _transaction;
    private string _connectionString = "Data Source=DANIEL;Initial Catalog=SocialCare;Persist Security Info=True;User ID=sa;Password=1928;Encrypt=True;TrustServerCertificate=True";

    public SqlConnection Connection
    {
        get { return _connection; }
        set { _connection = value; }
    }

    public SqlTransaction Transaction
    {
        get { return _transaction; }
    }

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
        using (SqlCommand command = new SqlCommand(commandText, _connection, _transaction))
        {
            return command.ExecuteNonQuery();
        }
    }

    public object ExecuteScalar(string commandText)
    {
        Open();
        using (SqlCommand command = new SqlCommand(commandText, _connection, _transaction))
        {
            return command.ExecuteScalar();
        }
    }

    public DataTable ExecuteQuery(string query)
    {
        Open();
        using (SqlCommand command = new SqlCommand(query, _connection, _transaction))
        {
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
        _transaction?.Dispose();
        Close();
        _connection.Dispose();
    }
}