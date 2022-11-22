using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
    //public class DapperContext : IDapperContext
    //{
    //    private IDbConnection _db;
    //    private IDbTransaction _transaction;
    //    private readonly string _providerName;
    //    private readonly string _connectionString;

    //    public DapperContext()
    //    {
    //    }
    //    public DapperContext(string server)
    //    {
    //        // _connectionString = "Data Source=192.168.13.67; Initial Catalog=EKlinikDB; User ID=sa; Password=12345; MultipleActiveResultSets=True;";
    //        _providerName = "Microsoft.Data.SqlClient";
    //        _connectionString = "Data Source=192.168.12.67; Initial Catalog=Dapper; User ID=sa; Password=Muhammad1828029699; MultipleActiveResultSets=True; Connection Timeout=1200";
    //        //if (server == Constant.DBKlinik)
    //        //{

    //        //    var host = "192.168.12.4";
    //        //    var port = "5432";
    //        //    var dbName = "eklinikdb";
    //        //    var appName = "EKlinik";
    //        //    var userId = "uKoneksi";
    //        //    var userPassword = "sm@rt2018";

    //        //    _providerName = "Npgsql";
    //        //    _connectionString = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};ApplicationName={5};Timeout=600", host, port, userId, userPassword, dbName, appName);

    //        //}
    //        //else if (server == Constant.DBPayroll)
    //        //{
    //        //    _providerName = "Microsoft.Data.SqlClient";
    //        //    _connectionString = "Data Source=192.168.12.12; Initial Catalog=RSUPPayroll; User ID=uKoneksi; Password=sm@rt2018; MultipleActiveResultSets=True; Connection Timeout=1200";
    //        //}
    //        //else if (server == Constant.DBSMSGat)
    //        //{
    //        //    _providerName = "Microsoft.Data.SqlClient";
    //        //    _connectionString = "Data Source=192.168.12.12; Initial Catalog=SMSGateway; User ID=uKoneksi; Password=sm@rt2018; MultipleActiveResultSets=True; Connection Timeout=1200";
    //        //}
    //        //else if (server == Constant.DBSQLSERVER)
    //        //{
    //        //    _providerName = "Microsoft.Data.SqlClient";
    //        //    _connectionString = "Data Source=192.168.13.67; Initial Catalog=EKlinikDB; User ID=sa; Password=12345; MultipleActiveResultSets=True; Connection Timeout=1200";
    //        //}
    //        //else if (server == Constant.DBPersonalia)
    //        //{
    //        //    _providerName = "Microsoft.Data.SqlClient";
    //        //    _connectionString = "Data Source=192.168.12.12; Initial Catalog=Personalia; User ID=uKoneksi; Password=sm@rt2018; MultipleActiveResultSets=True; Connection Timeout=1200";
    //        //}


    //        // else if (server == "Klinik")
    //        // _providerName = "Microsoft.Data.SqlClient";

    //        //         _connectionString = "Data Source=192.168.13.67; Initial Catalog=EKlinikDB; User ID=sa; Password=12345; MultipleActiveResultSets=True; Connection Timeout=1200";
    //        // else
    //        //     _connectionString = "Data Source=192.168.9.9; Initial Catalog=PKBDB; User ID=sa; Password=p@ssw0rd; MultipleActiveResultSets=True;";

    //        // _connectionString = "Data Source= 192.168.13.67; Initial Catalog=EKlinikDB; User ID=sa; Password=12345; MultipleActiveResultSets=True; Connection Timeout=1200";

    //        // var server = "192.168.12.12";
    //        // var dbName = "RSUPPayroll";
    //        // var userId = "iAzis";
    //        // var userPassword = "Mikrotik12@";

    //        // _providerName = "Microsoft.Data.SqlClient";
    //        // _connectionString = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3}; MultipleActiveResultSets=True;", server, dbName, userId, userPassword);

    //        if (_db == null)
    //        {
    //            _db = GetOpenConnection(_providerName, _connectionString);
    //        }
    //    }


    //    private IDbConnection GetOpenConnection(string providerName, string connectionString)
    //    {
    //        DbConnection conn = null;

    //        try
    //        {
    //            //if (providerName == "Npgsql")
    //            //{
    //            //    NpgsqlFactory provider = NpgsqlFactory.Instance;
    //            //    conn = provider.CreateConnection();
    //            //}
    //            //else
    //            //{
    //            //    SqlClientFactory provider = SqlClientFactory.Instance;
    //            //    conn = provider.CreateConnection();
    //            SqlClientFactory provider = SqlClientFactory.Instance;
    //            conn = provider.CreateConnection();
    //            //}
    //            // DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);
    //            conn.ConnectionString = connectionString;
    //            conn.Open();
    //        }
    //        catch
    //        {
    //        }

    //        return conn;
    //    }

    //    public IDbConnection db
    //    {
    //        get { return _db ?? (_db = GetOpenConnection(_providerName, _connectionString)); }
    //    }

    //    public IDbTransaction transaction
    //    {
    //        get { return _transaction; }
    //    }

    //    public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    //    {
    //        if (_transaction == null)
    //            _transaction = _db.BeginTransaction(isolationLevel);
    //    }

    //    public void Commit()
    //    {
    //        if (_transaction != null)
    //        {
    //            _transaction.Commit();
    //            _transaction = null;
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        if (_db != null)
    //        {
    //            try
    //            {
    //                if (_db.State != ConnectionState.Closed)
    //                {
    //                    if (_transaction != null)
    //                    {
    //                        _transaction.Rollback();
    //                    }

    //                    _db.Close();
    //                }
    //            }
    //            finally
    //            {
    //                _db.Dispose();
    //            }
    //        }

    //        GC.SuppressFinalize(this);
    //    }

    //    public string GetGUID()
    //    {
    //        var result = string.Empty;

    //        try
    //        {
    //            result = Guid.NewGuid().ToString();
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }

    //        return result;
    //    }

    //    public void Rollback()
    //    {
    //        if (_transaction != null)
    //        {
    //            _transaction.Rollback();
    //            _transaction = null;
    //        }
    //    }

    //    public int GetPagesCount(string sql, int pageSize, object param = null)
    //    {
    //        var pagesCount = 0;

    //        try
    //        {
    //            var recordCount = _db.QuerySingleOrDefault<int>(sql, param);
    //            pagesCount = (int)Math.Ceiling(recordCount / (decimal)pageSize);
    //        }
    //        catch (Exception)
    //        {
    //            //_log.Error("Error:", ex);
    //            throw;
    //        }

    //        return pagesCount;
    //    }
    //}
}
