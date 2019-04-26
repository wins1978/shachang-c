/* 
Name: EXCEL_DAILY_DETAIL
Description: Extends Linq Type Defines about Table : EXCEL_DAILY_DETAIL
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace ExcelPro
{
    [Table(Name = "EXCEL_DAILY_DETAIL")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_DAILY_DETAIL : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_DAILY_DETAIL";
        #endregion

        #region 字段名称常量

        /// <summary>
        /// ID的字段名
        /// </summary>
        public const string F_ID = "ID";

        /// <summary>
        /// DATE_STR的字段名
        /// </summary>
        public const string F_DATE_STR = "DATE_STR";

        /// <summary>
        /// REPORT_DATE的字段名
        /// </summary>
        public const string F_REPORT_DATE = "REPORT_DATE";

        /// <summary>
        /// FILE_NAME的字段名
        /// </summary>
        public const string F_FILE_NAME = "FILE_NAME";

        /// <summary>
        /// GUID_KEY的字段名
        /// </summary>
        public const string F_GUID_KEY = "GUID_KEY";

        /// <summary>
        /// VENDOR的字段名
        /// </summary>
        public const string F_VENDOR = "VENDOR";

        /// <summary>
        /// GOODS_NAME的字段名
        /// </summary>
        public const string F_GOODS_NAME = "GOODS_NAME";

        /// <summary>
        /// UNIT_PRICE的字段名
        /// </summary>
        public const string F_UNIT_PRICE = "UNIT_PRICE";

        /// <summary>
        /// WEIGHT的字段名
        /// </summary>
        public const string F_WEIGHT = "WEIGHT";

        /// <summary>
        /// GROUP_NAME的字段名
        /// </summary>
        public const string F_GROUP_NAME = "GROUP_NAME";

        /// <summary>
        /// IS_CHECK的字段名
        /// </summary>
        public const string F_IS_CHECK = "IS_CHECK";


        #endregion

        #region 私有变量
        private long _ID;
        private string _DATE_STR;
        private DateTime? _REPORT_DATE;
        private string _FILE_NAME;
        private string _GUID_KEY;
        private string _VENDOR;
        private string _GOODS_NAME;
        private decimal? _UNIT_PRICE;
        private decimal? _WEIGHT;
        private string _GROUP_NAME;
        private string _IS_CHECK;
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_ID", IsPrimaryKey = true, CanBeNull = false)]
        [DataMember(IsRequired = true)]
        public virtual long ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_DATE_STR", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string DATE_STR
        {
            get
            {
                return this._DATE_STR ?? "";
            }
            set
            {
                if ((this._DATE_STR != value))
                {
                    this._DATE_STR = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_REPORT_DATE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual DateTime? REPORT_DATE
        {
            get
            {
                return this._REPORT_DATE;
            }
            set
            {
                if ((this._REPORT_DATE != value))
                {
                    this._REPORT_DATE = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_FILE_NAME", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string FILE_NAME
        {
            get
            {
                return this._FILE_NAME ?? "";
            }
            set
            {
                if ((this._FILE_NAME != value))
                {
                    this._FILE_NAME = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_GUID_KEY", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GUID_KEY
        {
            get
            {
                return this._GUID_KEY ?? "";
            }
            set
            {
                if ((this._GUID_KEY != value))
                {
                    this._GUID_KEY = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_VENDOR", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string VENDOR
        {
            get
            {
                return this._VENDOR ?? "";
            }
            set
            {
                if ((this._VENDOR != value))
                {
                    this._VENDOR = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_GOODS_NAME", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GOODS_NAME
        {
            get
            {
                return this._GOODS_NAME ?? "";
            }
            set
            {
                if ((this._GOODS_NAME != value))
                {
                    this._GOODS_NAME = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_UNIT_PRICE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual decimal? UNIT_PRICE
        {
            get
            {
                return this._UNIT_PRICE;
            }
            set
            {
                if ((this._UNIT_PRICE != value))
                {
                    this._UNIT_PRICE = value;
                }
            }
        }
        /// <summary>
        /// 吨
        /// </summary>
        [Column(Storage = "_WEIGHT", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual decimal? WEIGHT
        {
            get
            {
                return this._WEIGHT;
            }
            set
            {
                if ((this._WEIGHT != value))
                {
                    this._WEIGHT = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_GROUP_NAME", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GROUP_NAME
        {
            get
            {
                return this._GROUP_NAME ?? "";
            }
            set
            {
                if ((this._GROUP_NAME != value))
                {
                    this._GROUP_NAME = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_IS_CHECK", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string IS_CHECK
        {
            get
            {
                return this._IS_CHECK ?? "";
            }
            set
            {
                if ((this._IS_CHECK != value))
                {
                    this._IS_CHECK = value;
                }
            }
        }

        /// <summary>
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_DAILY_DETAIL()
        {
        }
    }
}

