using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO
{
    public class ResultEntity
    {
        public string Status_Code { get; set; }
        public string Status_Result { get; set; }
        public string link { get; set; }
        public string data {  get; set; }
        
    }

    public class FilterEntity
    {

        public string strIDs { get; set; }

        public Int64 MasterID { get; set; }

        public Boolean? isView { get; set; }    

    }
    [Serializable]
    public class ddlEntity : BaseEntity
    {
        public string id { get; set; }
        public string secondary_id { get; set; }
        public string text { get; set; }


    }

    [Serializable]
    public class dropdown_entity 
    {
        public string id { get; set; }
        public string text { get; set; }
        public string GroupName { get; set; }

    }

    [Serializable]
    public class dropdown_TechpackEntity
    {
        public string tran_techpack_id { get; set; }
        public string techpack_number { get; set; }

    }
    //[Serializable]
    //public class file_upload :BaseEntity
    //{
    //    public string server_filename { get; set; }
    //    public string filename { get; set; }
    //    public string filetype { get; set; }
    //    public string imagetype { get; set; }
    //    public string extension { get; set; }
    //    public string base64string { get; set; }
    //    public int fileindex { get; set; }


    //}

    [Serializable]
    public class file_upload
    {
        public string server_filename { get; set; }
        public string filename { get; set; }
        public string filetype { get; set; }
        public string filePath { get; set; }
        public string extension { get; set; }
        public string base64string { get; set; }

        public string imagetype { get; set; }
        public int fileindex { get; set; }

        public int current_state { get; set; }


    }

    public class SelectListItemExtended : SelectListItem
    {
        public string str_other_value { get; set; }

        public string str_other_value2 { get; set; }

        public string str_other_value3 { get; set; }
    }
}
