/* 
Name: EXCEL_DAILY_MONEY
Description: Extends Linq Type Defines about Table : EXCEL_DAILY_MONEY
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
    [Table(Name = "EXCEL_DAILY_MONEY")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_DAILY_MONEY : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_DAILY_MONEY";
        #endregion

        #region 字段名称常量

        /// <summary>
        /// ID的字段名
        /// </summary>
        public const string F_ID = "ID";

        /// <summary>
        /// VENDOR的字段名
        /// </summary>
        public const string F_VENDOR = "VENDOR";

        /// <summary>
        /// TOTAL_PRICE的字段名
        /// </summary>
        public const string F_TOTAL_PRICE = "TOTAL_PRICE";

        /// <summary>
        /// DATE_STR的字段名
        /// </summary>
        public const string F_DATE_STR = "DATE_STR";

        /// <summary>
        /// REPORT_DATE的字段名
        /// </summary>
        public const string F_REPORT_DATE = "REPORT_DATE";

        /// <summary>
        /// UPDATE_DATE的字段名
        /// </summary>
        public const string F_UPDATE_DATE = "UPDATE_DATE";


        #endregion

        #region 私有变量
        private long _ID;
        private string _VENDOR;
        private decimal _TOTAL_PRICE;
        private string _DATE_STR;
        private DateTime? _REPORT_DATE;
        private DateTime? _UPDATE_DATE;
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
        [Column(Storage = "_VENDOR", IsPrimaryKey = false, CanBeNull = false)]
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
        [Column(Storage = "_TOTAL_PRICE", IsPrimaryKey = false, CanBeNull = false)]
        [DataMember(IsRequired = true)]
        public virtual decimal TOTAL_PRICE
        {
            get
            {
                return this._TOTAL_PRICE;
            }
            set
            {
                if ((this._TOTAL_PRICE != value))
                {
                    this._TOTAL_PRICE = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_DATE_STR", IsPrimaryKey = false, CanBeNull = false)]
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
        [Column(Storage = "_UPDATE_DATE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual DateTime? UPDATE_DATE
        {
            get
            {
                return this._UPDATE_DATE;
            }
            set
            {
                if ((this._UPDATE_DATE != value))
                {
                    this._UPDATE_DATE = value;
                }
            }
        }

        /// <summary>
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_DAILY_MONEY()
        {
        }
    }
}

