﻿using EPYSLSAILORAPP.Domain.Entity.General;
using EPYSLSAILORAPP.Domain.Interface;
using EPYSLSAILORAPP.Domain.Statics;
using System.Data;
using System.Data.SqlClient;

namespace EPYSLSAILORAPP.Application.Interface.Common
{
    public interface IDapperCRUDService<T> where T : class, IDapperBaseEntity
    {
        SqlConnection Connection { get; set; }

        SqlConnection GetConnection(string connectionName = AppConstants.DB_CONNECTION);

        Task<SqlConnection> OpenConnectionAsync();

        Task<List<dynamic>> GetDynamicDataAsync(string query);

        Task<List<dynamic>> GetDynamicDataAsync(string query, SqlConnection connection);

        Task<List<dynamic>> GetDynamicDataAsync(string query, object param);

        Task<List<dynamic>> GetDynamicDataAsync(string query, SqlConnection connection, object param);

        Task<int> GetSingleIntFieldAsync(string query);

        Task<int> GetSingleIntFieldAsync(string query, SqlConnection connection);

        Task<string> GetSingleStringFieldAsync(string query);

        Task<string> GetSingleStringFieldAsync(string query, SqlConnection connection);

        Task<bool> GetSingleBooleanFieldAsync(string query);

        Task<bool> GetSingleBooleanFieldAsync(string query, SqlConnection connection);
        Task OpenAsync();
        Task<dynamic> GetFirstOrDefaultDynamicDataAsync(string query);

        Task<dynamic> GetFirstOrDefaultDynamicDataAsync(string query, SqlConnection connection);

        Task<dynamic> GetFirstOrDefaultDynamicDataAsync(string query, object param);

        Task<dynamic> GetFirstOrDefaultDynamicDataAsync(string query, SqlConnection connection, object param);

        Task<List<T>> GetDataAsync(string query);

        Task<List<T>> GetDataAsync(string query, SqlConnection connection);

        Task<List<T>> GetDataAsync(string query, object param);

        Task<List<T>> GetDataAsync(string query, SqlConnection connection, object param);

        Task<List<CT>> GetDataAsync<CT>(string query) where CT : class;

        Task<List<CT>> GetDataAsync<CT>(string query, SqlConnection connection) where CT : class;

        Task<List<CT>> GetDataAsync<CT>(string query, object param) where CT : class;

        Task<List<CT>> GetDataAsync<CT>(string query, SqlConnection connection, object param) where CT : class;
        Task QueryMultipleAsync(string sql);
        Task<T> GetFirstOrDefaultAsync(string query);
        T GetFirstOrDefault(string query);
        Task<T> GetFirstOrDefaultAsync(string query, SqlConnection connection);

        Task<CT> GetFirstOrDefaultAsync<CT>(string query) where CT : class;

        Task<CT> GetFirstOrDefaultAsync<CT>(string query, SqlConnection connection) where CT : class;

        Task<T> GetFirstOrDefaultAsync(string query, object param);
        T GetFirstOrDefault(string query, object param);
        Signature GetSignature(string query, object param);
        Signature GetSignature(ref SqlConnection connection, ref SqlTransaction transaction, string query, object param);
        Task<T> GetFirstOrDefaultAsync(string query, SqlConnection connection, object param);

        Task<CT> GetFirstOrDefaultAsync<CT>(string query, object param) where CT : class;

        Task<CT> GetFirstOrDefaultAsync<CT>(string query, SqlConnection connection, object param) where CT : class;
        Task SaveCommonSingleAsync(IEnumerable<T> entities, SqlTransaction transaction);
        Task SaveCommonSingleAsync<CT>(IEnumerable<CT> entities, SqlTransaction transaction, Type tp) where CT : class, IDapperBaseEntity;
        Task SaveSingleAsync(T entity, SqlTransaction transaction);
        void SaveSingle(T entity, SqlTransaction transaction);
        void SaveSingle(T entity, ref SqlConnection connection, ref SqlTransaction transaction);
        Task SaveSingleAsync(T entity, SqlConnection connection, SqlTransaction transaction);
        Task SaveSingleAsync<CT>(CT entity, SqlTransaction transaction) where CT : class, IDapperBaseEntity;

        Task SaveSingleAsync<CT>(CT entity, SqlConnection connection, SqlTransaction transaction) where CT : class, IDapperBaseEntity;

        Task SaveAsync(IEnumerable<T> entities, SqlTransaction transaction);

        void Save(IEnumerable<T> entities, SqlTransaction transaction);
        Task SaveAsync(IEnumerable<T> entities, SqlConnection connection, SqlTransaction transaction);

        Task SaveAsync<CT>(IEnumerable<CT> entities, SqlTransaction transaction) where CT : class, IDapperBaseEntity;

        void Save<CT>(IEnumerable<CT> entities, SqlTransaction transaction) where CT : class, IDapperBaseEntity;

        Task SaveAsync<CT>(IEnumerable<CT> entities, SqlConnection connection, SqlTransaction transaction) where CT : class, IDapperBaseEntity;

        Task<int> ExecuteAsync(string query);

        int ExecuteSQL(string query);

        Task<int> ExecuteAsync(string query, object param, int commandTimeOut = 30, CommandType commandType = CommandType.Text);

        int ExecuteSQL(string query, object param, int commandTimeOut = 30, CommandType commandType = CommandType.Text);

        int ExecuteWithTransactionAsync(string query, ref SqlTransaction transaction, object param = null, int commandTimeOut = 30, CommandType commandType = CommandType.Text);

        Task<List<CT>> QueryMultipleAsync<CT>(string query, SqlConnection connection, object param) where CT : class;
        int QueryMultipleAsync(string query, ref SqlTransaction transaction, object param = null, int commandTimeOut = 30, CommandType commandType = CommandType.Text);
    }
}
