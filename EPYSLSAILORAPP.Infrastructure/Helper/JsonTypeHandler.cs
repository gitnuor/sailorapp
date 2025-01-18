using Dapper;
using Newtonsoft.Json;
using System.Data;

public class JsonTypeHandler : SqlMapper.TypeHandler<string>
{
    public override void SetValue(IDbDataParameter parameter, string value)
    {
        parameter.Value = value;
        parameter.DbType = DbType.String;
    }

    public override string Parse(object value)
    {
        return value.ToString();
    }
}


