using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.Entity.General;
using EPYSLSAILORAPP.Domain.Statics;
using System.Data.SqlClient;

namespace EPYSLSAILORAPP.Infrastructure.Services.Common
{
    public class SignatureService : ISignatureService
    {
        private SqlConnection _connection = null;
        private IDapperCRUDService<Signature> _service = null;
        private SqlTransaction transaction;
        public SignatureService(IDapperCRUDService<Signature> service)
        {
            _connection = service.Connection;
            _service = service;
        }

        #region All Method
        public int GetMaxId(string field, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat)
        {
            try
            {
                Signature signatureEntity = new Signature();// GetSignatureAsync(field, 1, 1, repeatAfter);
                if (signatureEntity == null)
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        Dates = DateTime.Today
                    };
                }
                else
                {
                    signatureEntity.LastNumber = ++signatureEntity.LastNumber;
                    signatureEntity.EntityState = EntityState.Modified;
                }
                _connection.Open();
                transaction = _connection.BeginTransaction();
                _service.SaveSingle(signatureEntity, transaction);
                transaction.Commit();
                return Convert.ToInt32(signatureEntity.LastNumber);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw (ex);
            }
            finally
            {
                if (transaction != null) transaction.Dispose();
            }
        }
        public int GetMaxId(ref SqlConnection connection, ref SqlTransaction transaction, string field, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat)
        {
            try
            {
                Signature signatureEntity = GetSignature(ref connection, ref transaction, field, 1, 1, repeatAfter);
                if (signatureEntity == null)
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        Dates = DateTime.Today
                    };
                }
                else
                {
                    signatureEntity.LastNumber = ++signatureEntity.LastNumber;
                    signatureEntity.EntityState = EntityState.Modified;
                }

                _service.SaveSingle(signatureEntity, ref connection, ref transaction);
                return Convert.ToInt32(signatureEntity.LastNumber);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw (ex);
            }

        }

        public async Task<int> GetMaxIdAsync(string field, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat)
        {
            try
            {
                Signature signatureEntity = await GetSignatureAsync(field, 1, 1, repeatAfter);

                if (signatureEntity == null)
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        Dates = DateTime.Today
                    };
                }
                else
                {
                    signatureEntity.LastNumber = ++signatureEntity.LastNumber;
                    signatureEntity.EntityState = EntityState.Modified;
                }

                await _connection.OpenAsync();
                transaction = _connection.BeginTransaction();
                await _service.SaveSingleAsync(signatureEntity, transaction);
                transaction.Commit();

                return Convert.ToInt32(signatureEntity.LastNumber);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw (ex);
            }
            finally
            {
                if (transaction != null) transaction.Dispose();
                _connection.Close();
            }
        }

        public async Task<int> GetMaxIdAsync(SqlTransaction transaction, string field, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat)
        {
            try
            {
                Signature signatureEntity = await GetSignatureAsync(field, 1, 1, repeatAfter);

                if (signatureEntity == null)
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        Dates = DateTime.Today
                    };
                }
                else
                {
                    signatureEntity.LastNumber = ++signatureEntity.LastNumber;
                    signatureEntity.EntityState = EntityState.Modified;
                }

                await _connection.OpenAsync();
                //transaction = _connection.BeginTransaction();
                await _service.SaveSingleAsync(signatureEntity, transaction);
                //transaction.Commit();

                return Convert.ToInt32(signatureEntity.LastNumber);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw (ex);
            }
            finally
            {
                if (transaction != null) transaction.Dispose();
                _connection.Close();
            }
        }

        public int GetMaxId(string field, int increment, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat)
        {
            try
            {
                Signature signatureEntity = new Signature();// GetSignatureAsync(field, 1, 1, repeatAfter);

                decimal maxId;
                if (signatureEntity == null)
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        Dates = DateTime.Today
                    };

                    maxId = signatureEntity.LastNumber;
                    signatureEntity.LastNumber = increment;
                }
                else
                {
                    maxId = signatureEntity.LastNumber;
                    signatureEntity.LastNumber += increment;
                    signatureEntity.EntityState = EntityState.Modified;
                }

                _connection.Open();
                transaction = _connection.BeginTransaction();
                _service.SaveSingle(signatureEntity, transaction);
                transaction.Commit();

                return Convert.ToInt32(++maxId);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw (ex);
            }
            finally
            {
                if (transaction != null) transaction.Dispose();
                _connection.Close();
            }

        }
        public int GetMaxId(ref SqlConnection connection, ref SqlTransaction transaction, string field, int increment, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat)
        {
            try
            {
                Signature signatureEntity = GetSignature(ref connection, ref transaction, field, 1, 1, repeatAfter); //new Signature();// GetSignatureAsync(field, 1, 1, repeatAfter);

                decimal maxId;
                if (signatureEntity.IsNull())
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        Dates = DateTime.Today
                    };

                    maxId = signatureEntity.LastNumber;
                    signatureEntity.LastNumber = increment;
                }
                else
                {
                    maxId = signatureEntity.LastNumber;
                    signatureEntity.LastNumber += increment;
                    signatureEntity.EntityState = EntityState.Modified;
                }

                _service.SaveSingle(signatureEntity, ref connection, ref transaction);

                return Convert.ToInt32(++maxId);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw (ex);
            }
        }

        public async Task<int> GetMaxIdAsync(string field, int increment, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat)
        {
            try
            {
                if (increment == 0) return 0;

                Signature signatureEntity = await GetSignatureAsync(field, 1, 1, repeatAfter);

                decimal maxId;
                if (signatureEntity == null)
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        Dates = DateTime.Today
                    };

                    maxId = signatureEntity.LastNumber;
                    signatureEntity.LastNumber = increment;
                }
                else
                {
                    maxId = signatureEntity.LastNumber + 1;
                    signatureEntity.LastNumber += increment;
                    signatureEntity.EntityState = EntityState.Modified;
                }

                await _connection.OpenAsync();
                transaction = _connection.BeginTransaction();
                await _service.SaveSingleAsync(signatureEntity, transaction);
                transaction.Commit();

                return Convert.ToInt32(maxId);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw (ex);
            }
            finally
            {
                if (transaction != null) transaction.Dispose();
                _connection.Close();
            }
        }
        public int GetMaxId(ref SqlTransaction transaction, string field, int increment, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat)
        {
            try
            {
                if (increment == 0) return 0;

                Signature signatureEntity = GetSignature(field, 1, 1, repeatAfter);

                decimal maxId;
                if (signatureEntity == null)
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        Dates = DateTime.Today
                    };

                    maxId = signatureEntity.LastNumber;
                    signatureEntity.LastNumber = increment;
                }
                else
                {
                    maxId = signatureEntity.LastNumber + 1;
                    signatureEntity.LastNumber += increment;
                    signatureEntity.EntityState = EntityState.Modified;
                }

                _connection.Open();
                //transaction = _connection.BeginTransaction();
                _service.SaveSingle(signatureEntity, transaction);
                //transaction.Commit();

                return Convert.ToInt32(maxId);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw (ex);
            }
            finally
            {
                if (transaction != null) transaction.Dispose();
                _connection.Close();
            }
        }

        public string GetMaxNo(string field, int companyId = 1, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat, string padWith = "00000")
        {
            try
            {
                Signature signatureEntity = new Signature();// GetSignatureAsync(field, companyId, 1, repeatAfter);

                if (signatureEntity == null)
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        CompanyId = companyId.ToString(),
                        Dates = DateTime.Today
                    };
                }
                else
                {
                    signatureEntity.LastNumber = ++signatureEntity.LastNumber;
                    signatureEntity.EntityState = EntityState.Modified;
                }

                _connection.Open();
                transaction = _connection.BeginTransaction();
                _service.SaveSingle(signatureEntity, transaction);
                transaction.Commit();

                var datePart = DateTime.Now.ToString("yyMMdd");
                var numberPart = signatureEntity.LastNumber.ToString(padWith);
                var maxNo = $@"{companyId}{datePart}{numberPart}";
                return maxNo;
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw (ex);
            }
            finally
            {
                if (transaction != null) transaction.Dispose();
                _connection.Close();
            }
        }

        public string GetDCGPMaxNo(string field, DateTime dtDate, string companyCode, string prefix = "15", int companyId = 1, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat, string padWith = "00000")
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetMaxNoAsync(string field, int companyId = 1, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat, string padWith = "00000")
        {
            try
            {
                Signature signatureEntity = await GetSignatureAsync(field, companyId, 1, repeatAfter);

                if (signatureEntity == null)
                {
                    signatureEntity = new Signature
                    {
                        Field = field,
                        Dates = DateTime.Today,
                        CompanyId = companyId.ToString()
                    };


                }
                else
                {
                    signatureEntity.LastNumber = ++signatureEntity.LastNumber;
                    signatureEntity.EntityState = EntityState.Modified;
                }

                await _connection.OpenAsync();
                transaction = _connection.BeginTransaction();
                await _service.SaveSingleAsync(signatureEntity, transaction);
                transaction.Commit();

                var datePart = DateTime.Now.ToString("yyMMdd");
                var numberPart = signatureEntity.LastNumber.ToString(padWith);
                var comId = companyId.ToString("00");
                var maxNo = $@"{comId}{datePart}{numberPart}";

                return maxNo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<int> GetMaxNoAsync(string tableName, string columnName, string replaceValue)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetMaxNoAsync(string tableName, string columnName, string replaceValue, int length)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TexConnection"].ConnectionString;
                var queryString = $"Select MaxValue=(ISNULL(Max(Convert(int,Replace({columnName},'{replaceValue}',''))),0) + 1) From {tableName} where {columnName} like '{replaceValue}%'";

                if (length > 0)
                {
                    queryString += $" AND LEN({columnName}) = {length} ";
                }

                int maxNo = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            maxNo = Convert.ToInt32(reader["MaxValue"]);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                return maxNo;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private Signature GetSignature(string field, int companyId, int siteId, RepeatAfterEnum repeatAfter)
        {
            Signature signatureEntity = new Signature();

            switch (repeatAfter)
            {
                case RepeatAfterEnum.NoRepeat:
                    signatureEntity = _service.GetSignature($@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId", new { Field = field, CompanyId = companyId, SiteId = siteId });
                    break;
                case RepeatAfterEnum.EveryYear:
                    signatureEntity = _service.GetSignature($@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId And Year(Dates)=@Year", new { Field = field, CompanyId = companyId, SiteId = siteId, Year = DateTime.Now.Year });
                    break;
                case RepeatAfterEnum.EveryMonth:
                    signatureEntity = _service.GetSignature($@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId And Month(Dates)=@Month And Year(Dates)=@Year", new { Field = field, CompanyId = companyId, SiteId = siteId, Month = DateTime.Now.Month, Year = DateTime.Now.Year });
                    break;
                case RepeatAfterEnum.EveryDay:
                    signatureEntity = _service.GetSignature($@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId And Day(Dates)=@Day And Month(Dates)=@Month And Year(Dates)=@Year", new { Field = field, CompanyId = companyId, SiteId = siteId, Day = DateTime.Today.Day, Month = DateTime.Today.Month, Year = DateTime.Today.Year });
                    break;
                default:
                    break;
            }
            if (signatureEntity.IsNotNull())
                signatureEntity.EntityState = EntityState.Modified;
            return signatureEntity;
        }
        private Signature GetSignature(ref SqlConnection connection, ref SqlTransaction transaction, string field, int companyId, int siteId, RepeatAfterEnum repeatAfter)
        {
            Signature signatureEntity = new Signature();

            switch (repeatAfter)
            {
                case RepeatAfterEnum.NoRepeat:
                    signatureEntity = _service.GetSignature(ref connection, ref transaction, $@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId", new { Field = field, CompanyId = companyId, SiteId = siteId });
                    break;
                case RepeatAfterEnum.EveryYear:
                    signatureEntity = _service.GetSignature(ref connection, ref transaction, $@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId And Year(Dates)=@Year", new { Field = field, CompanyId = companyId, SiteId = siteId, Year = DateTime.Now.Year });
                    break;
                case RepeatAfterEnum.EveryMonth:
                    signatureEntity = _service.GetSignature(ref connection, ref transaction, $@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId And Month(Dates)=@Month And Year(Dates)=@Year", new { Field = field, CompanyId = companyId, SiteId = siteId, Month = DateTime.Now.Month, Year = DateTime.Now.Year });
                    break;
                case RepeatAfterEnum.EveryDay:
                    signatureEntity = _service.GetSignature(ref connection, ref transaction, $@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId And Day(Dates)=@Day And Month(Dates)=@Month And Year(Dates)=@Year", new { Field = field, CompanyId = companyId, SiteId = siteId, Day = DateTime.Today.Day, Month = DateTime.Today.Month, Year = DateTime.Today.Year });
                    break;
                default:
                    break;
            }
            if (signatureEntity.IsNotNull())
                signatureEntity.EntityState = EntityState.Modified;
            return signatureEntity;
        }
        private async Task<Signature> GetSignatureAsync(string field, int companyId, int siteId, RepeatAfterEnum repeatAfter)
        {
            var signatureEntity = new Signature();

            switch (repeatAfter)
            {
                case RepeatAfterEnum.NoRepeat:
                    signatureEntity = await _service.GetFirstOrDefaultAsync<Signature>($@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId", new { Field = field, CompanyId = companyId, SiteId = siteId });
                    break;
                case RepeatAfterEnum.EveryYear:
                    signatureEntity = await _service.GetFirstOrDefaultAsync($@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId And Year(Dates)=@Year", new { Field = field, CompanyId = companyId, SiteId = siteId, Year = DateTime.Now.Year });
                    break;
                case RepeatAfterEnum.EveryMonth:
                    signatureEntity = await _service.GetFirstOrDefaultAsync($@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId And Month(Dates)=@Month And Year(Dates)=@Year", new { Field = field, CompanyId = companyId, SiteId = siteId, Month = DateTime.Now.Month, Year = DateTime.Now.Year });
                    break;
                case RepeatAfterEnum.EveryDay:
                    signatureEntity = await _service.GetFirstOrDefaultAsync($@"Select *From Signature Where Field = @Field And CompanyId = @CompanyId And SiteId = @SiteId And Day(Dates)=@Day And Month(Dates)=@Month And Year(Dates)=@Year", new { Field = field, CompanyId = companyId, SiteId = siteId, Day = DateTime.Today.Day, Month = DateTime.Today.Month, Year = DateTime.Today.Year });
                    break;
                default:
                    break;
            }
            return signatureEntity;
        }

        #endregion
    }
}
