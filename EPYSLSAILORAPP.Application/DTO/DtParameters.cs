using BDO.Core.Base;
using System;
using System.Collections.Generic;

using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Object = System.Object;

namespace EPYSLSAILORAPP.Application.DTO
{
   
    public class AjaxResponse
    {
        public Object data { get; }
        public long recordsTotal { get; }
        public long recordsFiltered { get; }

        public string responsecode { get; }
        public string responsetext { get; }
        public string responsestatus { get; }
        public string responsetitle { get; }
        public string responseredirecturl { get; }

        public AjaxResponse(string _responsecode, string _responsetext, string _responsestatus, string _responsetitle, string _responseredirecturl)
        {
            responsecode = _responsecode;
            responsetext = _responsetext;
            responsestatus = string.IsNullOrEmpty(_responsestatus) == true ? "Success" : _responsestatus;
            responsetitle = _responsetitle;
            responseredirecturl = _responseredirecturl;
        }
        public AjaxResponse(long _recordsTotal, Object _data)
        {
            recordsTotal = _recordsTotal;
            recordsFiltered = _recordsTotal;
            data = _data;
        }
        public AjaxResponse(Object _data)
        {
            data = _data;
        }
    }
        public class DtParameters 
    {
        public long? MasterID { get; set; }

        public bool? isView { get; set; }

        public string strMasterID { get; set; }

        public string strSecondID { get; set; }

        public long? FirstFilterID { get; set; }
        public long? SecondFilterID { get; set; }
        public int Draw { get; set; }
        public DtColumn[] Columns { get; set; }
        public DtOrder[] Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DtSearch Search { get; set; }
        public string SortOrder => Columns != null && Order != null && Order.Length > 0 ? (Columns[Order[0].Column].Data + (Order[0].Dir == DtOrderDir.Desc ? " " + Order[0].Dir : string.Empty)) : null;
        public IEnumerable<string> AdditionalValues { get; set; }
        public Int64 fiscal_year_id { get; set; }

        public Int64 event_id { get; set; }
        public Int64 supplier_id { get; set; }
        public Int64 delivery_unit_id { get; set; }

        public string? search_text { get; }
    }
    public class MCD_DtParameters : DtParameters
    {
        public long group_id { get; set; }

        public long sub_group_id { get; set; }

    }

    public class TechPack_DtParameters : DtParameters
    {
        public long techpack_id { get; set; }
        public string  active_tag { get; set; }
        public long  gen_finishing_process_id { get; set; }

    }

    public class PR_DtParameters : DtParameters
    {
        public long group_id { get; set; }

        public long sub_group_id { get; set; }

    }


    public class Chat_DtParameters : DtParameters
    {
        public long? group_id { get; set; }

        public string? to_user_name { get; set; }
        public string? from_user_name { get; set; }


    }
    public class DtColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DtSearch Search { get; set; }
    }

    public class DtOrder
    {
        public int Column { get; set; }
        public DtOrderDir Dir { get; set; }
    }

    public enum DtOrderDir
    {
        Asc,
        Desc
    }

    public class DtSearch
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }

}
