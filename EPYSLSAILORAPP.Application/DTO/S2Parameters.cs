using BDO.Core.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EPYSLSAILORAPP.Application.DTO
{
    [Serializable]
    [DataContract(Name = "S2Parameters", Namespace = "http://www.KAF.com/types")]
    public class S2Parameters : BaseEntity
    {
        [DataMember]
        public string s2SearchTerm { get; set; }
        [DataMember]
        public int? s2PageSize { get; set; }
        [DataMember]
        public int? s2PageNum { get; set; }
        [DataMember]
        public string s2Param { get; set; }
        [DataMember]
        public string s2Paramkey { get; set; }
    }

}
