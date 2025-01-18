using Newtonsoft.Json.Serialization;

namespace EPYSLSAILORAPP.Models
{
    public class CustomContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName[0] >= 'A' && propertyName[0] <= 'Z')
            {
                return propertyName;
            }
            else
            {
                return base.ResolvePropertyName(propertyName);
            }
        }
    }
}
