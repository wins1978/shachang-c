/* 
Name: EXCEL_SETTINGS
Description: Extends Linq Type Defines about Table : EXCEL_SETTINGS
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
    [Table(Name = "EXCEL_SETTINGS")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_SETTINGS : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_SETTINGS";
        #endregion

        #region 字段名称常量

        /// <summary>
        /// ID的字段名
        /// </summary>
        public const string F_ID = "ID";

        /// <summary>
        /// KEY的字段名
        /// </summary>
        public const string F_KEY = "KEY";

        /// <summary>
        /// VALUE的字段名
        /// </summary>
        public const string F_VALUE = "VALUE";

        /// <summary>
        /// GROUP_VALUE的字段名
        /// </summary>
        public const string F_GROUP_VALUE = "GROUP_VALUE";


        #endregion

        #region 私有变量
        private long _ID;
        private string _KEY;
        private string _VALUE;
        private string _GROUP_VALUE;
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
        [Column(Storage = "_KEY", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string KEY
        {
            get
            {
                return this._KEY ?? "";
            }
            set
            {
                if ((this._KEY != value))
                {
                    this._KEY = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_VALUE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string VALUE
        {
            get
            {
                return this._VALUE ?? "";
            }
            set
            {
                if ((this._VALUE != value))
                {
                    this._VALUE = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_GROUP_VALUE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GROUP_VALUE
        {
            get
            {
                return this._GROUP_VALUE ?? "";
            }
            set
            {
                if ((this._GROUP_VALUE != value))
                {
                    this._GROUP_VALUE = value;
                }
            }
        }

        /// <summary>
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_SETTINGS()
        {
        }
    }
}

