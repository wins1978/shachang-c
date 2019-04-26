/* 
Name: EXCEL_DAILY_WEEKREPORT_V
Description: Extends Linq Type Defines about Table : EXCEL_DAILY_WEEKREPORT_V
*/
using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
namespace ExcelPro
{
    [Table(Name = "EXCEL_DAILY_WEEKREPORT_V")]
    [DataContract]
    [Serializable]
    public partial class EXCEL_DAILY_WEEKREPORT_V
    {
        #region 表名称常量
        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_DAILY_WEEKREPORT_V";
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
        /// GOODS_NAME的字段名
        /// </summary>
        public const string F_GOODS_NAME = "GOODS_NAME";

        /// <summary>
        /// UNIT_PRICE的字段名
        /// </summary>
        public const string F_UNIT_PRICE = "UNIT_PRICE";

        /// <summary>
        /// WEEKLY_WEIGHT的字段名
        /// </summary>
        public const string F_WEEKLY_WEIGHT = "WEEKLY_WEIGHT";

        /// <summary>
        /// MONTHLY_WEIGHT的字段名
        /// </summary>
        public const string F_MONTHLY_WEIGHT = "MONTHLY_WEIGHT";


        #endregion

        #region 私有变量
        private string _VENDOR;
        private string _GOODS_NAME;
        private Nullable<decimal> _UNIT_PRICE;
        private Nullable<double> _WEEKLY_WEIGHT;
        private Nullable<double> _MONTHLY_WEIGHT;
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_VENDOR", CanBeNull = true)]
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
        [Column(Storage = "_GOODS_NAME", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GOODS_NAME
        {
            get
            {
                return this._GOODS_NAME == null ? "" : this._GOODS_NAME;
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
        [Column(Storage = "_UNIT_PRICE", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<decimal> UNIT_PRICE
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
        [Column(Storage = "_WEEKLY_WEIGHT", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> WEEKLY_WEIGHT
        {
            get
            {
                return this._WEEKLY_WEIGHT;
            }
            set
            {
                if ((this._WEEKLY_WEIGHT != value))
                {
                    this._WEEKLY_WEIGHT = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_MONTHLY_WEIGHT", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> MONTHLY_WEIGHT
        {
            get
            {
                return this._MONTHLY_WEIGHT;
            }
            set
            {
                if ((this._MONTHLY_WEIGHT != value))
                {
                    this._MONTHLY_WEIGHT = value;
                }
            }
        }
        #endregion
    }
}

