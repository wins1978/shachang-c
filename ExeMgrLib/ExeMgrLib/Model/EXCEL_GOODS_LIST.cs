/* 
Name: EXCEL_GOODS_LIST
Description: Extends Linq Type Defines about Table : EXCEL_GOODS_LIST
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
    [Table(Name = "EXCEL_GOODS_LIST")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_GOODS_LIST : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_GOODS_LIST";
        #endregion

        #region 字段名称常量

        /// <summary>
        /// ID的字段名
        /// </summary>
        public const string F_ID = "ID";

        /// <summary>
        /// GOODS_NAME的字段名
        /// </summary>
        public const string F_GOODS_NAME = "GOODS_NAME";

        /// <summary>
        /// GOODS_TYPE的字段名
        /// </summary>
        public const string F_GOODS_TYPE = "GOODS_TYPE";

        /// <summary>
        /// NODES的字段名
        /// </summary>
        public const string F_NODES = "NODES";

        /// <summary>
        /// ORDER_INDEX的字段名
        /// </summary>
        public const string F_ORDER_INDEX = "ORDER_INDEX";


        #endregion

        #region 私有变量
        private long _ID;
        private string _GOODS_NAME;
        private string _GOODS_TYPE;
        private string _NODES;
        private int? _ORDER_INDEX;
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
        /// 石仔；石粉
        /// </summary>
        [Column(Storage = "_GOODS_TYPE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GOODS_TYPE
        {
            get
            {
                return this._GOODS_TYPE ?? "";
            }
            set
            {
                if ((this._GOODS_TYPE != value))
                {
                    this._GOODS_TYPE = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_NODES", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string NODES
        {
            get
            {
                return this._NODES ?? "";
            }
            set
            {
                if ((this._NODES != value))
                {
                    this._NODES = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_ORDER_INDEX", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual int? ORDER_INDEX
        {
            get
            {
                return this._ORDER_INDEX;
            }
            set
            {
                if ((this._ORDER_INDEX != value))
                {
                    this._ORDER_INDEX = value;
                }
            }
        }

        /// <summary>
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_GOODS_LIST()
        {
        }
    }
}

