/* 
Name: EXCEL_VENDOR_GOODS
Description: Extends Linq Type Defines about Table : EXCEL_VENDOR_GOODS
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
    [Table(Name = "EXCEL_VENDOR_GOODS")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_VENDOR_GOODS : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_VENDOR_GOODS";
        #endregion

        #region 字段名称常量

        /// <summary>
        /// ID的字段名
        /// </summary>
        public const string F_ID = "ID";

        /// <summary>
        /// VENDER的字段名
        /// </summary>
        public const string F_VENDER = "VENDER";

        /// <summary>
        /// GOODS_NAME的字段名
        /// </summary>
        public const string F_GOODS_NAME = "GOODS_NAME";

        /// <summary>
        /// UNIT_PRICE的字段名
        /// </summary>
        public const string F_UNIT_PRICE = "UNIT_PRICE";

        /// <summary>
        /// UPDATE_DATE的字段名
        /// </summary>
        public const string F_UPDATE_DATE = "UPDATE_DATE";

        /// <summary>
        /// DATE_STR的字段名
        /// </summary>
        public const string F_DATE_STR = "DATE_STR";


        #endregion

        #region 私有变量
        private long _ID;
        private string _VENDER;
        private string _GOODS_NAME;
        private decimal? _UNIT_PRICE;
        private DateTime? _UPDATE_DATE;
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
        [Column(Storage = "_VENDER", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string VENDER
        {
            get
            {
                return this._VENDER ?? "";
            }
            set
            {
                if ((this._VENDER != value))
                {
                    this._VENDER = value;
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

        public EXCEL_VENDOR_GOODS()
        {
        }
    }
}
