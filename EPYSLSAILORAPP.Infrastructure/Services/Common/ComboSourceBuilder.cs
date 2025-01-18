using EPYSLSAILORAPP.Domain.Statics;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Infrastructure.Services.Common
{
    public class ComboSourceBuilder
    {
        public static IEnumerable<Select2OptionModel> GetSelectComboBoxSource(IList list, String dataValueField, String dataTextField)
        {
            return GetSelectComboBoxSource(list, dataValueField, dataTextField, String.Empty);
        }
        public static IEnumerable<Select2OptionModel> GetSelectComboBoxSource(IList list, String dataValueField, String dataTextField, String dataDescField)
        {
            try
            {
                List<Select2OptionModel> valueList = new List<Select2OptionModel>();
                String source = String.Empty;
                if (list.GetType() == typeof(List<Select2OptionModel>))
                {
                    return (List<Select2OptionModel>)list;
                }
                else
                {
                    foreach (Object obj in list)
                    {
                        Select2OptionModel comVal = new Select2OptionModel();
                        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
                        PropertyDescriptor? pi = null;
                        Object val = null;
                        pi = properties.Find(dataValueField, true);
                        if (pi.IsNotNull())
                        {
                            val = pi.GetValue(obj);
                            comVal.id = val.IsNull() ? String.Empty : val.ToString();
                        }
                        pi = properties.Find(dataTextField, true);
                        if (pi.IsNotNull())
                        {
                            val = pi.GetValue(obj);
                            comVal.text = val.IsNull() ? String.Empty : val.ToString();
                        }
                        pi = properties.Find(dataDescField, true);
                        if (pi.IsNotNull())
                        {
                            val = pi.GetValue(obj);
                            comVal.desc = val.IsNull() ? String.Empty : val.ToString();
                        }
                        //
                        valueList.Add(comVal);
                    }
                    //
                    return valueList;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
