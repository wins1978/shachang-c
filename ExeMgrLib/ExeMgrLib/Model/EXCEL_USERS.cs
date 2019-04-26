/* 
Name: EXCEL_USERS
Description: Extends Linq Type Defines about Table : EXCEL_USERS
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
    [Table(Name = "EXCEL_USERS")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_USERS : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_USERS";
        #endregion

        #region 字段名称常量

        /// <summary>
        /// ID的字段名
        /// </summary>
        public const string F_ID = "ID";

        /// <summary>
        /// USER_NAME的字段名
        /// </summary>
        public const string F_USER_NAME = "USER_NAME";

        /// <summary>
        /// PASSWORD的字段名
        /// </summary>
        public const string F_PASSWORD = "PASSWORD";

        /// <summary>
        /// IS_VALID的字段名
        /// </summary>
        public const string F_IS_VALID = "IS_VALID";


        #endregion

        #region 私有变量
        private long _ID;
        private string _USER_NAME;
        private string _PASSWORD;
        private string _IS_VALID;
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
        [Column(Storage = "_USER_NAME", IsPrimaryKey = false, CanBeNull = false)]
        [DataMember(IsRequired = true)]
        public virtual string USER_NAME
        {
            get
            {
                return this._USER_NAME ?? "";
            }
            set
            {
                if ((this._USER_NAME != value))
                {
                    this._USER_NAME = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_PASSWORD", IsPrimaryKey = false, CanBeNull = false)]
        [DataMember(IsRequired = true)]
        public virtual string PASSWORD
        {
            get
            {
                return this._PASSWORD ?? "";
            }
            set
            {
                if ((this._PASSWORD != value))
                {
                    this._PASSWORD = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_IS_VALID", IsPrimaryKey = false, CanBeNull = false)]
        [DataMember(IsRequired = true)]
        public virtual string IS_VALID
        {
            get
            {
                return this._IS_VALID ?? "";
            }
            set
            {
                if ((this._IS_VALID != value))
                {
                    this._IS_VALID = value;
                }
            }
        }

        /// <summary>
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_USERS()
        {
        }
    }
}
