/* 
Name: EXCEL_VENDOR_COST_LAST_V
Description: Extends Linq Type Defines about Table : EXCEL_VENDOR_COST_LAST_V
*/
using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
namespace ExcelPro
{
    [Table(Name = "EXCEL_VENDOR_COST_LAST_V")]
    [DataContract]
    [Serializable]
    public partial class EXCEL_VENDOR_COST_LAST_V
    {
        #region 表名称常量
        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_VENDOR_COST_LAST_V";
        #endregion

        #region 相关表
        public static string[] TABLES = new string[]
        {

        };
        #endregion

        #region 字段名称常量

        /// <summary>
        /// VENDOR的字段名
        /// </summary>
        public const string F_VENDOR = "VENDOR";

        /// <summary>
        /// TOTAL_COST的字段名
        /// </summary>
        public const string F_TOTAL_COST = "TOTAL_COST";

        /// <summary>
        /// TOTAL_INCOME的字段名
        /// </summary>
        public const string F_TOTAL_INCOME = "TOTAL_INCOME";

        /// <summary>
        /// COST_REMAIN的字段名
        /// </summary>
        public const string F_COST_REMAIN = "COST_REMAIN";


        #endregion

        #region 私有变量
        private string _VENDOR;
        private Nullable<long> _TOTAL_COST;
        private Nullable<long> _TOTAL_INCOME;
        private Nullable<long> _COST_REMAIN;
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_VENDOR", CanBeNull = false)]
        [DataMember(IsRequired = true)]
        public virtual string VENDOR
        {
            get
            {
                return this._VENDOR == null ? "" : this._VENDOR;
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
        [Column(Storage = "_TOTAL_COST", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<long> TOTAL_COST
        {
            get
            {
                return this._TOTAL_COST;
            }
            set
            {
                if ((this._TOTAL_COST != value))
                {
                    this._TOTAL_COST = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_TOTAL_INCOME", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<long> TOTAL_INCOME
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
        /// 
        /// </summary>
        [Column(Storage = "_COST_REMAIN", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<long> COST_REMAIN
        {
            get
            {
                return this._COST_REMAIN;
            }
            set
            {
                if ((this._COST_REMAIN != value))
                {
                    this._COST_REMAIN = value;
                }
            }
        }
        #endregion
    }
}

