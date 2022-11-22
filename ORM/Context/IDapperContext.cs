using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Context
{
    public interface IDapperContext : IDisposable
    {
        IDbConnection db { get; }
        IDbTransaction transaction { get; }
        // bool IsOpenConnection();
        // bool ExecSQL(string sql);
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
        string GetGUID();
        int GetPagesCount(string sql, int pageSize, object param = null);
    }
}
