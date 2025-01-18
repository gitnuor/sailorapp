using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BDO.Core.Base
{

    public abstract class BaseEntity
    {

        public string strCommonSerachParam { get; set; }

        private Int64? currentEntityState = 2;

        public string status
        {
            get;
            set;
        }

        public string strMasterID
        {
            get;
            set;
        }

        public bool? isExt
        {
            get;
            set;
        }

        public int Start { get; set; }
        public int Length { get; set; }

        public string strDetailID
        {
            get;
            set;
        }


        public string SortExpression
        {
            get;
            set;
        }


        public long TotalRecord
        {
            get;
            set;
        }


        public int PageSize
        {
            get;
            set;
        }


        public int CurrentPage
        {
            get;
            set;
        }


        public long RowNumber
        {
            get;
            set;
        }



        public Int64? current_state
        {
            get { return currentEntityState; }
            set { currentEntityState = value; }
        }



        protected BaseEntity()
        {
            this.currentEntityState = 1;
        }

    }

    
}
