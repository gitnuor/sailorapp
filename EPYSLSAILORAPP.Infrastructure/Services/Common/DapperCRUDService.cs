using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.Entity.General;
using EPYSLSAILORAPP.Domain.Interface;
using EPYSLSAILORAPP.Domain.Statics;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPYSLSAILORAPP.Infrastructure.Services.Common
{
    public class DapperCRUDService<T> : IDapperCRUDService<T> where T : class, IDapperBaseEntity
    {
        private readonly IConfiguration _configuration;
        public SqlConnection Connection { get; set; }
        public DapperCRUDService(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public SqlConnection GetConnection(string connectionName = AppConstants.DB_CONNECTION)
        {
            return Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<SqlConnection> OpenConnectionAsync()
        {
            //var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await Connection.OpenAsync();
            return Connection;
        }

        public async Task<List<dynamic>> GetDynamicDataAsync(string query)
        {
            try
            {
                await Connection.OpenAsync();
                var records = await Connection.QueryAsync(query);
                return records.AsList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<List<dynamic>> GetDynamicDataAsync(string query, SqlConnection connection)
        {
            try
            {
                await connection.OpenAsync();
                var records = await connection.QueryAsync(query);
                return records.AsList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<List<dynamic>> GetDynamicDataAsync(string query, object param)
        {
            try
            {
                await Connection.OpenAsync();
                var records = await Connection.QueryAsync<dynamic>(query, param);
                return records.AsList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<List<dynamic>> GetDynamicDataAsync(string query, SqlConnection connection, object param)
        {
            try
            {
                await connection.OpenAsync();
                var records = await connection.QueryAsync<dynamic>(query, param);
                return records.AsList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<dynamic> GetFirstOrDefaultDynamicDataAsync(string query)
        {
            try
            {
                await Connection.OpenAsync();
                return await Connection.QueryFirstOrDefaultAsync(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<dynamic> GetFirstOrDefaultDynamicDataAsync(string query, SqlConnection connection)
        {
            try
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<dynamic> GetFirstOrDefaultDynamicDataAsync(string query, object param)
        {
            try
            {
                await Connection.OpenAsync();
                var records = await Connection.QueryFirstOrDefaultAsync(query, param);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<dynamic> GetFirstOrDefaultDynamicDataAsync(string query, SqlConnection connection, object param)
        {
            try
            {
                await connection.OpenAsync();
                var records = await connection.QueryFirstOrDefaultAsync(query, param);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<List<T>> GetDataAsync(string query)
        {
            try
            {
                await Connection.OpenAsync();
                var records = await Connection.QueryAsync<T>(query);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<List<T>> GetDataAsync(string query, SqlConnection connection)
        {
            try
            {
                await connection.OpenAsync();
                var records = await connection.QueryAsync<T>(query);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<List<T>> GetDataAsync(string query, object param)
        {
            try
            {
                await Connection.OpenAsync();
                var records = await Connection.QueryAsync<T>(query, param);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<List<T>> GetDataAsync(string query, SqlConnection connection, object param)
        {
            try
            {
                await connection.OpenAsync();
                var records = await connection.QueryAsync<T>(query, param);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<List<CT>> GetDataAsync<CT>(string query) where CT : class
        {
            try
            {
                await Connection.OpenAsync();
                var records = await Connection.QueryAsync<CT>(query);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<List<CT>> GetDataAsync<CT>(string query, SqlConnection connection) where CT : class
        {
            try
            {
                await connection.OpenAsync();
                var records = await connection.QueryAsync<CT>(query);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<List<CT>> GetDataAsync<CT>(string query, object param) where CT : class
        {
            try
            {
                await Connection.OpenAsync();
                var records = await Connection.QueryAsync<CT>(query, param);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<List<CT>> GetDataAsync<CT>(string query, SqlConnection connection, object param) where CT : class
        {
            try
            {
                await connection.OpenAsync();
                var records = await connection.QueryAsync<CT>(query, param);
                return records.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<int> GetSingleIntFieldAsync(string query)
        {
            try
            {
                await Connection.OpenAsync();
                return await Connection.QueryFirstOrDefaultAsync<int>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<int> GetSingleIntFieldAsync(string query, SqlConnection connection)
        {
            try
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<int>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<string> GetSingleStringFieldAsync(string query)
        {
            try
            {
                await Connection.OpenAsync();
                return await Connection.QueryFirstOrDefaultAsync<string>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<string> GetSingleStringFieldAsync(string query, SqlConnection connection)
        {
            try
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<string>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<bool> GetSingleBooleanFieldAsync(string query)
        {
            try
            {
                await Connection.OpenAsync();
                return await Connection.QueryFirstOrDefaultAsync<bool>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<bool> GetSingleBooleanFieldAsync(string query, SqlConnection connection)
        {
            try
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<bool>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<T> GetFirstOrDefaultAsync(string query)
        {
            try
            {
                await Connection.OpenAsync();
                return await Connection.QueryFirstOrDefaultAsync<T>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public T GetFirstOrDefault(string query)
        {
            try
            {
                Connection.Open();
                return Connection.QueryFirstOrDefault(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<T> GetFirstOrDefaultAsync(string query, SqlConnection connection)
        {
            try
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<CT> GetFirstOrDefaultAsync<CT>(string query) where CT : class
        {
            try
            {
                await Connection.OpenAsync();
                return await Connection.QueryFirstOrDefaultAsync<CT>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<CT> GetFirstOrDefaultAsync<CT>(string query, SqlConnection connection) where CT : class
        {
            try
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<CT>(query);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<T> GetFirstOrDefaultAsync(string query, object param)
        {
            try
            {
                await Connection.OpenAsync();
                return await Connection.QueryFirstOrDefaultAsync<T>(query, param);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<T> GetFirstOrDefaultAsync(string query, SqlConnection connection, object param)
        {
            try
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(query, param);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<CT> GetFirstOrDefaultAsync<CT>(string query, object param) where CT : class
        {
            try
            {
                await Connection.OpenAsync();
                return await Connection.QueryFirstOrDefaultAsync<CT>(query, param);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public T GetFirstOrDefault(string query, object param)
        {
            try
            {
                Connection.Open();
                return Connection.QueryFirstOrDefault(query, param);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        public Signature GetSignature(string query, object param)
        {
            try
            {
                Connection.Open();
                return Connection.QueryFirstOrDefault<Signature>(query, param);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public Signature GetSignature(ref SqlConnection connection, ref SqlTransaction transaction, string query, object param)
        {
            try
            {
                return connection.QueryFirstOrDefault<Signature>(query, param, transaction);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CT> GetFirstOrDefaultAsync<CT>(string query, SqlConnection connection, object param) where CT : class
        {
            try
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<CT>(query, param);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public async Task SaveCommonSingleAsync(IEnumerable<T> entities, SqlTransaction transaction)
        {
            var addList = entities.Where(x => x.EntityState == EntityState.Added);
            if (addList.Any()) await Connection.InsertMAsync(addList, transaction);

            var updateList = entities.Where(x => x.EntityState == EntityState.Modified);
            if (updateList.Any()) await Connection.UpdateAsync(updateList, transaction);

            var deleteList = entities.Where(x => x.EntityState == EntityState.Deleted);
            if (deleteList.Any()) await Connection.DeleteAsync(deleteList, transaction);
        }
        public async Task SaveCommonSingleAsync<CT>(IEnumerable<CT> entities, SqlTransaction transaction, Type tp) where CT : class, IDapperBaseEntity
        {
            var addList = entities.Where(x => x.EntityState == EntityState.Added);
            if (addList.Any()) await Connection.InsertMAsync(addList, transaction, tp);

            var updateList = entities.Where(x => x.EntityState == EntityState.Modified);
            if (updateList.Any()) await Connection.UpdateMAsync(updateList, transaction, tp);

            var deleteList = entities.Where(x => x.EntityState == EntityState.Deleted);
            if (deleteList.Any()) await Connection.DeleteMAsync(deleteList, transaction, tp);
        }
        public async Task SaveSingleAsync(T entity, SqlTransaction transaction)
        {
            switch (entity.EntityState)
            {
                case EntityState.Added:
                    await Connection.InsertAsync(entity, transaction);
                    break;
                case EntityState.Deleted:
                    await Connection.DeleteAsync(entity, transaction);
                    break;
                case EntityState.Modified:
                    await Connection.UpdateAsync(entity, transaction);
                    break;
                default:
                    break;
            }
        }
        public void SaveSingle(T entity, ref SqlConnection connection, ref SqlTransaction transaction)
        {
            switch (entity.EntityState)
            {
                case EntityState.Added:
                    connection.Insert(entity, transaction);
                    break;
                case EntityState.Deleted:
                    connection.Delete(entity, transaction);
                    break;
                case EntityState.Modified:
                    connection.Update(entity, transaction);
                    break;
                default:
                    break;
            }
        }
        public void SaveSingle(T entity, SqlTransaction transaction)
        {
            switch (entity.EntityState)
            {
                case EntityState.Added:
                    Connection.Insert(entity, transaction);
                    break;
                case EntityState.Deleted:
                    Connection.Delete(entity, transaction);
                    break;
                case EntityState.Modified:
                    Connection.Update(entity, transaction);
                    break;
                default:
                    break;
            }
        }

        public async Task SaveSingleAsync(T entity, SqlConnection connection, SqlTransaction transaction)
        {
            switch (entity.EntityState)
            {
                case EntityState.Added:
                    await connection.InsertAsync(entity, transaction);
                    break;
                case EntityState.Deleted:
                    await connection.DeleteAsync(entity, transaction);
                    break;
                case EntityState.Modified:
                    await connection.UpdateAsync(entity, transaction);
                    break;
                default:
                    break;
            }
        }

        public async Task SaveSingleAsync<CT>(CT entity, SqlTransaction transaction) where CT : class, IDapperBaseEntity
        {
            switch (entity.EntityState)
            {
                case EntityState.Added:
                    await Connection.InsertAsync(entity, transaction);
                    break;
                case EntityState.Deleted:
                    await Connection.DeleteAsync(entity, transaction);
                    break;
                case EntityState.Modified:
                    await Connection.UpdateAsync(entity, transaction);
                    break;
                default:
                    break;
            }
        }

        public async Task SaveSingleAsync<CT>(CT entity, SqlConnection connection, SqlTransaction transaction) where CT : class, IDapperBaseEntity
        {
            switch (entity.EntityState)
            {
                case EntityState.Added:
                    await connection.InsertAsync(entity, transaction);
                    break;
                case EntityState.Deleted:
                    await connection.DeleteAsync(entity, transaction);
                    break;
                case EntityState.Modified:
                    await connection.UpdateAsync(entity, transaction);
                    break;
                default:
                    break;
            }
        }

        public async Task SaveAsync(IEnumerable<T> entities, SqlTransaction transaction)
        {
            var addList = entities.Where(x => x.EntityState == EntityState.Added);
            if (addList.Any()) await Connection.InsertAsync(addList, transaction);

            var updateList = entities.Where(x => x.EntityState == EntityState.Modified);
            if (updateList.Any()) await Connection.UpdateAsync(updateList, transaction);

            var deleteList = entities.Where(x => x.EntityState == EntityState.Deleted);
            if (deleteList.Any()) await Connection.DeleteAsync(deleteList, transaction);
        }

        public void Save(IEnumerable<T> entities, SqlTransaction transaction)
        {
            var addList = entities.Where(x => x.EntityState == EntityState.Added);
            if (addList.Any()) Connection.Insert(addList, transaction);

            var updateList = entities.Where(x => x.EntityState == EntityState.Modified);
            if (updateList.Any()) Connection.Update(updateList, transaction);

            var deleteList = entities.Where(x => x.EntityState == EntityState.Deleted);
            if (deleteList.Any()) Connection.Delete(deleteList, transaction);
        }

        public async Task SaveAsync(IEnumerable<T> entities, SqlConnection connection, SqlTransaction transaction)
        {
            var addList = entities.Where(x => x.EntityState == EntityState.Added);
            if (addList.Any()) await connection.InsertAsync(addList, transaction);

            var updateList = entities.Where(x => x.EntityState == EntityState.Modified);
            if (updateList.Any()) await connection.UpdateAsync(updateList, transaction);

            var deleteList = entities.Where(x => x.EntityState == EntityState.Deleted);
            if (deleteList.Any()) await connection.DeleteAsync(deleteList, transaction);
        }

        public async Task SaveAsync<CT>(IEnumerable<CT> entities, SqlTransaction transaction) where CT : class, IDapperBaseEntity
        {
            var addList = entities.Where(x => x.EntityState == EntityState.Added);
            if (addList.Any()) await Connection.InsertAsync(addList, transaction);

            var updateList = entities.Where(x => x.EntityState == EntityState.Modified);
            if (updateList.Any()) await Connection.UpdateAsync(updateList, transaction);

            var deleteList = entities.Where(x => x.EntityState == EntityState.Deleted);
            if (deleteList.Any()) await Connection.DeleteAsync(deleteList, transaction);
        }

        public void Save<CT>(IEnumerable<CT> entities, SqlTransaction transaction) where CT : class, IDapperBaseEntity
        {
            var addList = entities.Where(x => x.EntityState == EntityState.Added);
            if (addList.Any()) Connection.Insert(addList, transaction);

            var updateList = entities.Where(x => x.EntityState == EntityState.Modified);
            if (updateList.Any()) Connection.Update(updateList, transaction);

            var deleteList = entities.Where(x => x.EntityState == EntityState.Deleted);
            if (deleteList.Any()) Connection.Delete(deleteList, transaction);
        }

        public async Task SaveAsync<CT>(IEnumerable<CT> entities, SqlConnection connection, SqlTransaction transaction) where CT : class, IDapperBaseEntity
        {
            var addList = entities.Where(x => x.EntityState == EntityState.Added);
            if (addList.Any()) await connection.InsertAsync(addList, transaction);

            var updateList = entities.Where(x => x.EntityState == EntityState.Modified);
            if (updateList.Any()) await connection.UpdateAsync(updateList, transaction);

            var deleteList = entities.Where(x => x.EntityState == EntityState.Deleted);
            if (deleteList.Any()) await connection.DeleteAsync(deleteList, transaction);
        }
        public async Task<int> ExecuteAsync(string query)
        {
            SqlTransaction transaction = null;

            try
            {
                await Connection.OpenAsync();
                transaction = Connection.BeginTransaction();
                int rows = await Connection.ExecuteAsync(query);
                transaction.Commit();
                return rows;
            }
            catch (System.Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                Connection.Close();
            }
        }

        public int ExecuteSQL(string query)
        {
            SqlTransaction transaction = null;

            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                int rows = Connection.Execute(query);
                transaction.Commit();
                return rows;
            }
            catch (System.Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                Connection.Close();
            }
        }

        public async Task<int> ExecuteAsync(string query, object param, int commandTimeOut = 30, CommandType commandType = CommandType.Text)
        {
            SqlTransaction transaction = null;

            try
            {
                await Connection.OpenAsync();
                transaction = Connection.BeginTransaction();
                int rows = await Connection.ExecuteAsync(query, param, transaction, commandTimeOut, commandType);
                transaction.Commit();
                return rows;
            }
            catch (System.Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                Connection.Close();
            }
        }

        public int ExecuteSQL(string query, object param, int commandTimeOut = 30, CommandType commandType = CommandType.Text)
        {
            SqlTransaction transaction = null;

            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                int rows = Connection.Execute(query, param, transaction, commandTimeOut, commandType);
                transaction.Commit();
                return rows;
            }
            catch (System.Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                Connection.Close();
            }
        }



        public int ExecuteWithTransactionAsync(string query, ref SqlTransaction transaction, object param = null, int commandTimeOut = 30, CommandType commandType = CommandType.Text)
        {
            //SqlTransaction transaction = null;

            try
            {
                //await Connection.OpenAsync();
                //transaction = Connection.BeginTransaction();
                int rows = Connection.Execute(query, param, transaction, commandTimeOut, commandType);
                //transaction.Commit();
                return rows;
            }
            catch (System.Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }

        }

        public Task OpenAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task QueryMultipleAsync(string sql)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CT>> QueryMultipleAsync<CT>(string query, SqlConnection connection, object param) where CT : class
        {
            throw new System.NotImplementedException();
        }

        public int QueryMultipleAsync(string query, ref SqlTransaction transaction, object param = null, int commandTimeOut = 30, CommandType commandType = CommandType.Text)
        {
            throw new System.NotImplementedException();
        }
    }
}