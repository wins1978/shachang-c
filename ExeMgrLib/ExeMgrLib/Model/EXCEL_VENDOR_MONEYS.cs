/* 
Name: EXCEL_VENDOR_MONEYS
Description: Extends Linq Type Defines about Table : EXCEL_VENDOR_MONEYS
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
    [Table(Name = "EXCEL_VENDOR_MONEYS")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_VENDOR_MONEYS : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_VENDOR_MONEYS";
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
        /// UPDATE_DATE的字段名
        /// </summary>
        public const string F_UPDATE_DATE = "UPDATE_DATE";

        /// <summary>
        /// MONEY_ADVANCE的字段名
        /// </summary>
        public const string F_MONEY_ADVANCE = "MONEY_ADVANCE";

        /// <summary>
        /// IMPORT_DATE的字段名
        /// </summary>
        public const string F_IMPORT_DATE = "IMPORT_DATE";

        /// <summary>
        /// NOTES的字段名
        /// </summary>
        public const string F_NOTES = "NOTES";

        /// <summary>
        /// DATE_STR的字段名
        /// </summary>
        public const string F_DATE_STR = "DATE_STR";


        #endregion

        #region 私有变量
        private long _ID;
        private string _VENDOR;
        private DateTime? _UPDATE_DATE;
        private decimal? _MONEY_ADVANCE;
        private DateTime? _IMPORT_DATE;
        private string _NOTES;
        private string _DATE_STR;
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
        /// 
        /// </summary>
        [Column(Storage = "_MONEY_ADVANCE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual decimal? MONEY_ADVANCE
        {
            get
            {
                return this._MONEY_ADVANCE;
            }
            set
            {
                if ((this._MONEY_ADVANCE != value))
                {
                    this._MONEY_ADVANCE = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_IMPORT_DATE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual DateTime? IMPORT_DATE
        {
            get
            {
                return this._IMPORT_DATE;
            }
            set
            {
                if ((this._IMPORT_DATE != value))
                {
                    this._IMPORT_DATE = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_NOTES", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string NOTES
        {
            get
            {
                return this._NOTES ?? "";
            }
            set
            {
                if ((this._NOTES != value))
                {
                    this._NOTES = value;
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
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_VENDOR_MONEYS()
        {
        }
    }
}

