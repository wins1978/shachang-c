/* 
Name: EXCEL_DAILY_SUMMARY
Description: Extends Linq Type Defines about Table : EXCEL_DAILY_SUMMARY
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
    [Table(Name = "EXCEL_DAILY_SUMMARY")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_DAILY_SUMMARY : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_DAILY_SUMMARY";
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
        /// SHIFENG_W的字段名
        /// </summary>
        public const string F_SHIFENG_W = "SHIFENG_W";

        /// <summary>
        /// SHIZAI_W的字段名
        /// </summary>
        public const string F_SHIZAI_W = "SHIZAI_W";

        /// <summary>
        /// CASH的字段名
        /// </summary>
        public const string F_CASH = "CASH";

        /// <summary>
        /// DEBT的字段名
        /// </summary>
        public const string F_DEBT = "DEBT";

        /// <summary>
        /// TOTAL_INCOME的字段名
        /// </summary>
        public const string F_TOTAL_INCOME = "TOTAL_INCOME";


        #endregion

        #region 私有变量
        private long _ID;
        private string _DATE_STR;
        private DateTime? _REPORT_DATE;
        private string _FILE_NAME;
        private string _GUID_KEY;
        private decimal? _SHIFENG_W;
        private decimal? _SHIZAI_W;
        private decimal? _CASH;
        private decimal? _DEBT;
        private decimal? _TOTAL_INCOME;
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
        /// 石粉吨数
        /// </summary>
        [Column(Storage = "_SHIFENG_W", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual decimal? SHIFENG_W
        {
            get
            {
                return this._SHIFENG_W;
            }
            set
            {
                if ((this._SHIFENG_W != value))
                {
                    this._SHIFENG_W = value;
                }
            }
        }
        /// <summary>
        /// 石仔吨数
        /// </summary>
        [Column(Storage = "_SHIZAI_W", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual decimal? SHIZAI_W
        {
            get
            {
                return this._SHIZAI_W;
            }
            set
            {
                if ((this._SHIZAI_W != value))
                {
                    this._SHIZAI_W = value;
                }
            }
        }
        /// <summary>
        /// 现金
        /// </summary>
        [Column(Storage = "_CASH", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual decimal? CASH
        {
            get
            {
                return this._CASH;
            }
            set
            {
                if ((this._CASH != value))
                {
                    this._CASH = value;
                }
            }
        }
        /// <summary>
        /// 欠账
        /// </summary>
        [Column(Storage = "_DEBT", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual decimal? DEBT
        {
            get
            {
                return this._DEBT;
            }
            set
            {
                if ((this._DEBT != value))
                {
                    this._DEBT = value;
                }
            }
        }
        /// <summary>
        /// 总收入
        /// </summary>
        [Column(Storage = "_TOTAL_INCOME", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual decimal? TOTAL_INCOME
        {
            get
            {
                return this._TOTAL_INCOME;
            }
            set
            {
                if ((this._TOTAL_INCOME != value))
                {
                    this._TOTAL_INCOME = value;
                }
            }
        }

        /// <summary>
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_DAILY_SUMMARY()
        {
        }
    }
}

