/* 
Name: EXCEL_C_DAILY_SUMMARY
Description: Extends Linq Type Defines about Table : EXCEL_C_DAILY_SUMMARY
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
    [Table(Name = "EXCEL_C_DAILY_SUMMARY")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_C_DAILY_SUMMARY : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_C_DAILY_SUMMARY";
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
        /// TITLE的字段名
        /// </summary>
        public const string F_TITLE = "TITLE";


        #endregion

        #region 私有变量
        private long _ID;
        private string _DATE_STR;
        private DateTime? _REPORT_DATE;
        private string _FILE_NAME;
        private string _TITLE;
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
        [Column(Storage = "_TITLE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string TITLE
        {
            get
            {
                return this._TITLE ?? "";
            }
            set
            {
                if ((this._TITLE != value))
                {
                    this._TITLE = value;
                }
            }
        }

        /// <summary>
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_C_DAILY_SUMMARY()
        {
        }
    }
}

