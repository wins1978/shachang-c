/* 
Name: EXCEL_VENDOR_LIST
Description: Extends Linq Type Defines about Table : EXCEL_VENDOR_LIST
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
    [Table(Name = "EXCEL_VENDOR_LIST")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_VENDOR_LIST : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_VENDOR_LIST";
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
        /// TEL的字段名
        /// </summary>
        public const string F_TEL = "TEL";

        /// <summary>
        /// CONTACT_NAME的字段名
        /// </summary>
        public const string F_CONTACT_NAME = "CONTACT_NAME";

        /// <summary>
        /// COST_ADVANCE的字段名
        /// </summary>
        public const string F_COST_ADVANCE = "COST_ADVANCE";

        /// <summary>
        /// COST_ALERT的字段名
        /// </summary>
        public const string F_COST_ALERT = "COST_ALERT";

        /// <summary>
        /// NEED_COST_ADVANCE的字段名
        /// </summary>
        public const string F_NEED_COST_ADVANCE = "NEED_COST_ADVANCE";


        #endregion

        #region 私有变量
        private long _ID;
        private string _VENDOR;
        private string _TEL;
        private string _CONTACT_NAME;
        private int? _COST_ADVANCE;
        private int? _COST_ALERT;
        private string _NEED_COST_ADVANCE;
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
        /// 名称
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
        /// 电话
        /// </summary>
        [Column(Storage = "_TEL", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string TEL
        {
            get
            {
                return this._TEL ?? "";
            }
            set
            {
                if ((this._TEL != value))
                {
                    this._TEL = value;
                }
            }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        [Column(Storage = "_CONTACT_NAME", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string CONTACT_NAME
        {
            get
            {
                return this._CONTACT_NAME ?? "";
            }
            set
            {
                if ((this._CONTACT_NAME != value))
                {
                    this._CONTACT_NAME = value;
                }
            }
        }
        /// <summary>
        /// 预付金额元
        /// </summary>
        [Column(Storage = "_COST_ADVANCE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual int? COST_ADVANCE
        {
            get
            {
                return this._COST_ADVANCE;
            }
            set
            {
                if ((this._COST_ADVANCE != value))
                {
                    this._COST_ADVANCE = value;
                }
            }
        }
        /// <summary>
        /// 等只有10万货款时报警黄色，余额0时报警红色
        /// </summary>
        [Column(Storage = "_COST_ALERT", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual int? COST_ALERT
        {
            get
            {
                return this._COST_ALERT;
            }
            set
            {
                if ((this._COST_ALERT != value))
                {
                    this._COST_ALERT = value;
                }
            }
        }
        /// <summary>
        /// 需要预付；不需要预付
        /// </summary>
        [Column(Storage = "_NEED_COST_ADVANCE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string NEED_COST_ADVANCE
        {
            get
            {
                return this._NEED_COST_ADVANCE ?? "";
            }
            set
            {
                if ((this._NEED_COST_ADVANCE != value))
                {
                    this._NEED_COST_ADVANCE = value;
                }
            }
        }

        /// <summary>
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_VENDOR_LIST()
        {
        }
    }
}
