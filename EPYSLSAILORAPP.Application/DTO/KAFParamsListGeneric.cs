using BDO.Core.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EPYSLSAILORAPP.Application.DTO
{
    [Serializable]
    [DataContract(Name = "S2Parameters", Namespace = "http://www.KAF.com/types")]
    public class KAFParamsListGeneric : BaseEntity
    {
        protected string _paramname;
        protected string _paramnamear;
        protected string _paramvalue;
        public KAFParamsListGeneric()
        {
        }


      
        [DataMember]
        public string paramname
        {
            get { return _paramname; }
            set { _paramname = value; }
        }
        [DataMember]
        public string paramnamear
        {
            get { return _paramnamear; }
            set { _paramnamear = value; }
        }
        [DataMember]
        public string paramvalue
        {
            get { return _paramvalue; }
            set { _paramvalue = value; }
        }

    }

}
