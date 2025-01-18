using EPYSLSAILORAPP.Domain.Statics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


namespace System.Net.Http
{
    public static class RequestExtension
    {
        //public static PaginationInfo GetPaginationInfo(this HttpRequestMessage request)
        //{
        //    var paginationInfo = new PaginationInfo();
        //    var keyValuePairs = request.GetQueryNameValuePairs();

        //    paginationInfo.GridType = keyValuePairs.FirstOrDefault(x => x.Key.Equals("gridType", StringComparison.OrdinalIgnoreCase)).Value ?? "ej2";
        //    if (paginationInfo.GridType.Equals("bootstrap-table"))
        //    {
        //        var filterValue = keyValuePairs.FirstOrDefault(x => x.Key.Equals("filter", StringComparison.OrdinalIgnoreCase));
        //        if (filterValue.Value.NotNullOrEmpty())
        //        {
        //            var filterValueList = new List<string>();
        //            dynamic filterObj = JsonConvert.DeserializeObject(filterValue.Value);
        //            foreach (var item in filterObj)
        //            {
        //                filterValueList.Add($"{item.Name} like '%{item.Value.Value}%'");
        //            }

        //            paginationInfo.FilterBy = $"Where {string.Join(" And ", filterValueList)}";
        //        }

        //        var sort = keyValuePairs.FirstOrDefault(x => x.Key.Equals("sort", StringComparison.OrdinalIgnoreCase));
        //        var order = keyValuePairs.FirstOrDefault(x => x.Key.Equals("order", StringComparison.OrdinalIgnoreCase));
        //        if (sort.Value.NotNullOrEmpty() && order.Value.NotNullOrEmpty()) paginationInfo.OrderBy = $"Order By {sort} {order}";

        //        var skip = keyValuePairs.FirstOrDefault(x => x.Key.Equals("offset", StringComparison.OrdinalIgnoreCase));
        //        var take = keyValuePairs.FirstOrDefault(x => x.Key.Equals("limit", StringComparison.OrdinalIgnoreCase));

        //        if (skip.Value.IsNotNullOrEmpty() && take.Value.IsNotNullOrEmpty())
        //        {
        //            paginationInfo.PageBy = $"Offset {skip.Value} Rows Fetch Next {take.Value} Rows Only";
        //            paginationInfo.PageByNew = $"R_No_New BETWEEN {Convert.ToInt32(skip.Value)} AND {Convert.ToInt32(skip.Value) - 1 + Convert.ToInt32(take.Value)} ";
        //            //if (paginationInfo.FilterBy.ToLower().Contains("where"))
        //            //{
        //            //    paginationInfo.PageByNew = $"R_No_New BETWEEN {Convert.ToInt32(skip.Value)} AND {Convert.ToInt32(skip.Value) - 1 + Convert.ToInt32(take.Value)} ";
        //            //}
        //        }
        //    }
        //    else
        //    {
        //        var filterValue = keyValuePairs.FirstOrDefault(x => x.Key.Equals("$filter", StringComparison.OrdinalIgnoreCase));
        //        if (filterValue.Value.NotNullOrEmpty())
        //        {
        //            var filterValueList = new List<string>();
        //            var filterValues = filterValue.Value.Split(new string[] { "and" }, StringSplitOptions.RemoveEmptyEntries);
        //            foreach (var fValue in filterValues)
        //            {
        //                if (fValue.ToLower() == "eq") //fValue.Contains("eq")
        //                {
        //                    var fValueStr = fValue.Replace("eq", "=").Replace("tolower(", "").Replace(")", "").Replace("(", "");
        //                    filterValueList.Add(fValueStr);
        //                }
        //                else if (fValue.Contains(" eq false"))
        //                {
        //                    var fValueStr = fValue.Replace(" eq false", " = 0");
        //                    filterValueList.Add(fValueStr);
        //                }
        //                else if (fValue.Contains(" eq true"))
        //                {
        //                    var fValueStr = fValue.Replace(" eq true", " = 1");
        //                    filterValueList.Add(fValueStr);
        //                }
        //                else
        //                {
        //                    var fValueParts = fValue.Split(',');
        //                    if (fValueParts.Length == 1)
        //                    {
        //                        if (fValue.Contains(" eq "))
        //                        {
        //                            var fValueStr = fValue.Replace("eq", "=").Replace("tolower(", "").Replace(")", "").Replace("(", "");
        //                            filterValueList.Add(fValueStr);
        //                        }
        //                    }
        //                    else if (fValueParts.Length == 2)
        //                    {
        //                        var field = fValueParts[0].Substring(fValueParts[0].LastIndexOf('(') + 1).Replace(")", "");
        //                        var value = fValueParts[1].Replace(")", "").Replace("'", "");
        //                        filterValueList.Add($"{field} Like '%{value.Trim()}%'");
        //                    }
        //                }
        //            }

        //            paginationInfo.FilterBy = $"Where {string.Join(" And ", filterValueList)}";
        //        }

        //        var orderBy = keyValuePairs.FirstOrDefault(x => x.Key.Equals("$orderby", StringComparison.OrdinalIgnoreCase));
        //        if (orderBy.Value.NotNullOrEmpty()) paginationInfo.OrderBy = $"Order By {orderBy.Value}";

        //        var skip = keyValuePairs.FirstOrDefault(x => x.Key.Equals("$skip", StringComparison.OrdinalIgnoreCase));
        //        var take = keyValuePairs.FirstOrDefault(x => x.Key.Equals("$top", StringComparison.OrdinalIgnoreCase));

        //        if (skip.Value.IsNotNullOrEmpty() && take.Value.IsNotNullOrEmpty())
        //        {
        //            paginationInfo.PageBy = $"Offset {skip.Value} Rows Fetch Next {take.Value} Rows Only";
        //            paginationInfo.PageByNew = $"R_No_New BETWEEN {Convert.ToInt32(skip.Value)} AND {Convert.ToInt32(skip.Value) - 1 + Convert.ToInt32(take.Value)} ";
        //        }
        //    }

        //    return paginationInfo;
        //}
    }
}
