/* 
Name: EXCEL_VENDOR_COST_CUR_V
Description: Extends Linq Type Defines about Table : EXCEL_VENDOR_COST_CUR_V
*/
using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
namespace ExcelPro
{
    [Table(Name = "EXCEL_VENDOR_COST_CUR_V")]
    [DataContract]
    [Serializable]
    public partial class EXCEL_VENDOR_COST_CUR_V
    {
        #region 表名称常量
        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_VENDOR_COST_CUR_V";
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
        /// LAST_MON_REMAIN的字段名
        /// </summary>
        public const string F_LAST_MON_REMAIN = "LAST_MON_REMAIN";

        /// <summary>
        /// CUR_MON_COST的字段名
        /// </summary>
        public const string F_CUR_MON_COST = "CUR_MON_COST";

        /// <summary>
        /// CUR_MON_INCOME的字段名
        /// </summary>
        public const string F_CUR_MON_INCOME = "CUR_MON_INCOME";

        /// <summary>
        /// CUR_MON_REMAIN的字段名
        /// </summary>
        public const string F_CUR_MON_REMAIN = "CUR_MON_REMAIN";

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
        private Nullable<double> _LAST_MON_REMAIN;
        private Nullable<double> _CUR_MON_COST;
        private Nullable<double> _CUR_MON_INCOME;
        private Nullable<double> _CUR_MON_REMAIN;
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
        [Column(Storage = "_LAST_MON_REMAIN", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> LAST_MON_REMAIN
        {
            get
            {
                return this._LAST_MON_REMAIN;
            }
            set
            {
                if ((this._LAST_MON_REMAIN != value))
                {
                    this._LAST_MON_REMAIN = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_CUR_MON_COST", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> CUR_MON_COST
        {
            get
            {
                return this._CUR_MON_COST;
            }
            set
            {
                if ((this._CUR_MON_COST != value))
                {
                    this._CUR_MON_COST = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_CUR_MON_INCOME", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> CUR_MON_INCOME
        {
            get
            {
                return this._CUR_MON_INCOME;
            }
            set
            {
                if ((this._CUR_MON_INCOME != value))
                {
                    this._CUR_MON_INCOME = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_CUR_MON_REMAIN", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> CUR_MON_REMAIN
        {
            get
            {
                return this._CUR_MON_REMAIN;
            }
            set
            {
                if ((this._CUR_MON_REMAIN != value))
                {
                    this._CUR_MON_REMAIN = value;
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

