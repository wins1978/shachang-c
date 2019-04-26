/* 
Name: EXCEL_VENDOR_COST_V
Description: Extends Linq Type Defines about Table : EXCEL_VENDOR_COST_V
*/
using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
namespace ExcelPro
{
    [Table(Name = "EXCEL_VENDOR_COST_V")]
    [DataContract]
    [Serializable]
    public partial class EXCEL_VENDOR_COST_V
    {
        #region 表名称常量
        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_VENDOR_COST_V";
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
        /// LAST_REMAIN的字段名
        /// </summary>
        public const string F_LAST_REMAIN = "LAST_REMAIN";

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

        /// <summary>
        /// COST_ALERT的字段名
        /// </summary>
        public const string F_COST_ALERT = "COST_ALERT";

        /// <summary>
        /// REMAIN_PER的字段名
        /// </summary>
        public const string F_REMAIN_PER = "REMAIN_PER";


        #endregion

        #region 私有变量
        private string _VENDOR;
        private Nullable<double> _LAST_REMAIN;
        private Nullable<double> _TOTAL_COST;
        private Nullable<double> _TOTAL_INCOME;
        private Nullable<double> _COST_REMAIN;
        private Nullable<double> _COST_ALERT;
        private Nullable<double> _REMAIN_PER;
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
        [Column(Storage = "_LAST_REMAIN", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> LAST_REMAIN
        {
            get
            {
                return this._LAST_REMAIN;
            }
            set
            {
                if ((this._LAST_REMAIN != value))
                {
                    this._LAST_REMAIN = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_TOTAL_COST", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> TOTAL_COST
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
        public virtual Nullable<double> TOTAL_INCOME
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
        public virtual Nullable<double> COST_REMAIN
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
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_COST_ALERT", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> COST_ALERT
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
        /// 
        /// </summary>
        [Column(Storage = "_REMAIN_PER", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> REMAIN_PER
        {
            get
            {
                return this._REMAIN_PER;
            }
            set
            {
                if ((this._REMAIN_PER != value))
                {
                    this._REMAIN_PER = value;
                }
            }
        }
        #endregion
    }
}

