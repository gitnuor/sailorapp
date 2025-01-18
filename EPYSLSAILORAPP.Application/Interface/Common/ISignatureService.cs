using EPYSLSAILORAPP.Domain;

namespace EPYSLSAILORAPP.Application.Interface.Common
{
    public interface ISignatureService
    {
        /// <summary>
        /// Get Max Id
        /// </summary>
        /// <param name="field">Field or Table name</param>
        /// <param name="repeatAfter"><see cref="RepeatAfterEnum"/> Repeat After</param>
        /// <returns></returns>
        int GetMaxId(string field, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat);


        /// <summary>
        /// Get Max Id
        /// </summary>
        /// <param name="field">Field or Table name</param>
        /// <param name="repeatAfter"><see cref="RepeatAfterEnum"/> Repeat After</param>
        /// <returns></returns>
        int GetMaxId(ref System.Data.SqlClient.SqlConnection connection, ref System.Data.SqlClient.SqlTransaction transaction, string field, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat);

        /// <summary>
        /// Get Max Id Async
        /// </summary>
        /// <param name="field">Field or Table name</param>
        /// <param name="repeatAfter"><see cref="RepeatAfterEnum"/> Repeat After</param>
        /// <returns></returns>
        Task<int> GetMaxIdAsync(string field, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat);
        /// <summary>
        /// Get Max Id Async
        /// </summary>
        /// <param name="field">Field or Table name</param>
        /// <param name="repeatAfter"><see cref="RepeatAfterEnum"/> Repeat After</param>
        /// <returns></returns>
        Task<int> GetMaxIdAsync(System.Data.SqlClient.SqlTransaction transaction, string field, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat);

        /// <summary>
        /// Get Max Id
        /// </summary>
        /// <param name="field">Field or Table name</param>
        /// <param name="increment">Increment</param>
        /// <param name="repeatAfter"><see cref="RepeatAfterEnum"/>Repeat After</param>
        /// <returns></returns>
        int GetMaxId(string field, int increment, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat);

        /// <summary>
        /// Get Max Id
        /// </summary>
        /// <param name="field">Field or Table name</param>
        /// <param name="increment">Increment</param>
        /// <param name="repeatAfter"><see cref="RepeatAfterEnum"/>Repeat After</param>
        /// <returns></returns>
        int GetMaxId(ref System.Data.SqlClient.SqlConnection connection, ref System.Data.SqlClient.SqlTransaction transaction, string field, int increment, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat);

        /// <summary>
        /// Get Max Id Async
        /// </summary>
        /// <param name="field">Field or Table name</param>
        /// <param name="increment">Increment</param>
        /// <param name="repeatAfter"><see cref="RepeatAfterEnum"/>Repeat After</param>
        /// <returns></returns>
        Task<int> GetMaxIdAsync(string field, int increment, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat);

        /// <summary>
        /// Get Max Id Async
        /// </summary>
        /// <param name="field">Field or Table name</param>
        /// <param name="increment">Increment</param>
        /// <param name="repeatAfter"><see cref="RepeatAfterEnum"/>Repeat After</param>
        /// <returns></returns>
        int GetMaxId(ref System.Data.SqlClient.SqlTransaction transaction, string field, int increment, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat);

        string GetMaxNo(string field, int companyId = 1, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat, string padWith = "00000");
        string GetDCGPMaxNo(string field, System.DateTime dtDate, string companyCode, string prefix = "15", int companyId = 1, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat, string padWith = "00000");

        Task<string> GetMaxNoAsync(string field, int companyId = 1, RepeatAfterEnum repeatAfter = RepeatAfterEnum.NoRepeat, string padWith = "00000");
        Task<int> GetMaxNoAsync(string tableName, string columnName, string replaceValue);
        Task<int> GetMaxNoAsync(string tableName, string columnName, string replaceValue, int length);
    }
}
