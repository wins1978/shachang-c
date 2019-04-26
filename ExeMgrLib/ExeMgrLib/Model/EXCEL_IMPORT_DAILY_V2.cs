/* 
Name: EXCEL_IMPORT_DAILY_V2
Description: Extends Linq Type Defines about Table : EXCEL_IMPORT_DAILY_V2
*/
using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
namespace ExcelPro
{
    [Table(Name = "EXCEL_IMPORT_DAILY_V2")]
    [DataContract]
    [Serializable]
    public partial class EXCEL_IMPORT_DAILY_V2
    {
        #region 表名称常量
        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_IMPORT_DAILY_V2";
        #endregion

        #region 相关表
        public static string[] TABLES = new string[]
        {

        };
        #endregion

        #region 字段名称常量

        /// <summary>
        /// ID的字段名
        /// </summary>
        public const string F_ID = "ID";

        /// <summary>
        /// DRIVER_NAME的字段名
        /// </summary>
        public const string F_DRIVER_NAME = "DRIVER_NAME";

        /// <summary>
        /// NUMBER_FLAG的字段名
        /// </summary>
        public const string F_NUMBER_FLAG = "NUMBER_FLAG";

        /// <summary>
        /// DATE_STR的字段名
        /// </summary>
        public const string F_DATE_STR = "DATE_STR";

        /// <summary>
        /// TIME_STR的字段名
        /// </summary>
        public const string F_TIME_STR = "TIME_STR";

        /// <summary>
        /// CAR_NUMBER的字段名
        /// </summary>
        public const string F_CAR_NUMBER = "CAR_NUMBER";

        /// <summary>
        /// SHOP_NUMBER的字段名
        /// </summary>
        public const string F_SHOP_NUMBER = "SHOP_NUMBER";

        /// <summary>
        /// GROSS_WEIGHT的字段名
        /// </summary>
        public const string F_GROSS_WEIGHT = "GROSS_WEIGHT";

        /// <summary>
        /// TARE_WEIGHT的字段名
        /// </summary>
        public const string F_TARE_WEIGHT = "TARE_WEIGHT";

        /// <summary>
        /// NET_WEIGHT的字段名
        /// </summary>
        public const string F_NET_WEIGHT = "NET_WEIGHT";

        /// <summary>
        /// VENDOR_NET_WEIGHT的字段名
        /// </summary>
        public const string F_VENDOR_NET_WEIGHT = "VENDOR_NET_WEIGHT";

        /// <summary>
        /// TAKE_DEPT的字段名
        /// </summary>
        public const string F_TAKE_DEPT = "TAKE_DEPT";

        /// <summary>
        /// SEND_OUT_DEPT的字段名
        /// </summary>
        public const string F_SEND_OUT_DEPT = "SEND_OUT_DEPT";

        /// <summary>
        /// GROUP_NAME的字段名
        /// </summary>
        public const string F_GROUP_NAME = "GROUP_NAME";

        /// <summary>
        /// IMPORT_DATE的字段名
        /// </summary>
        public const string F_IMPORT_DATE = "IMPORT_DATE";

        /// <summary>
        /// IMPORT_DATE_STR的字段名
        /// </summary>
        public const string F_IMPORT_DATE_STR = "IMPORT_DATE_STR";

        /// <summary>
        /// FILE_NAME的字段名
        /// </summary>
        public const string F_FILE_NAME = "FILE_NAME";

        /// <summary>
        /// GUID_KEY的字段名
        /// </summary>
        public const string F_GUID_KEY = "GUID_KEY";

        /// <summary>
        /// ROW_INDEX的字段名
        /// </summary>
        public const string F_ROW_INDEX = "ROW_INDEX";

        /// <summary>
        /// REPORT_DATE的字段名
        /// </summary>
        public const string F_REPORT_DATE = "REPORT_DATE";

        /// <summary>
        /// UNIT_PRICE的字段名
        /// </summary>
        public const string F_UNIT_PRICE = "UNIT_PRICE";

        /// <summary>
        /// TOTAL_PRICE的字段名
        /// </summary>
        public const string F_TOTAL_PRICE = "TOTAL_PRICE";


        #endregion

        #region 私有变量
        private long _ID;
        private string _DRIVER_NAME;
        private string _NUMBER_FLAG;
        private string _DATE_STR;
        private string _TIME_STR;
        private string _CAR_NUMBER;
        private string _SHOP_NUMBER;
        private string _GROSS_WEIGHT;
        private string _TARE_WEIGHT;
        private string _NET_WEIGHT;
        private string _VENDOR_NET_WEIGHT;
        private string _TAKE_DEPT;
        private string _SEND_OUT_DEPT;
        private string _GROUP_NAME;
        private Nullable<DateTime> _IMPORT_DATE;
        private string _IMPORT_DATE_STR;
        private string _FILE_NAME;
        private string _GUID_KEY;
        private Nullable<int> _ROW_INDEX;
        private Nullable<DateTime> _REPORT_DATE;
        private Nullable<double> _UNIT_PRICE;
        private Nullable<double> _TOTAL_PRICE;
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_ID", CanBeNull = false)]
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
        [Column(Storage = "_DRIVER_NAME", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string DRIVER_NAME
        {
            get
            {
                return this._DRIVER_NAME == null ? "" : this._DRIVER_NAME;
            }
            set
            {
                if ((this._DRIVER_NAME != value))
                {
                    this._DRIVER_NAME = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_NUMBER_FLAG", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string NUMBER_FLAG
        {
            get
            {
                return this._NUMBER_FLAG == null ? "" : this._NUMBER_FLAG;
            }
            set
            {
                if ((this._NUMBER_FLAG != value))
                {
                    this._NUMBER_FLAG = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_DATE_STR", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string DATE_STR
        {
            get
            {
                return this._DATE_STR == null ? "" : this._DATE_STR;
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
        /// 
        /// </summary>
        [Column(Storage = "_TIME_STR", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string TIME_STR
        {
            get
            {
                return this._TIME_STR == null ? "" : this._TIME_STR;
            }
            set
            {
                if ((this._TIME_STR != value))
                {
                    this._TIME_STR = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_CAR_NUMBER", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string CAR_NUMBER
        {
            get
            {
                return this._CAR_NUMBER == null ? "" : this._CAR_NUMBER;
            }
            set
            {
                if ((this._CAR_NUMBER != value))
                {
                    this._CAR_NUMBER = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_SHOP_NUMBER", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string SHOP_NUMBER
        {
            get
            {
                return this._SHOP_NUMBER == null ? "" : this._SHOP_NUMBER;
            }
            set
            {
                if ((this._SHOP_NUMBER != value))
                {
                    this._SHOP_NUMBER = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_GROSS_WEIGHT", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GROSS_WEIGHT
        {
            get
            {
                return this._GROSS_WEIGHT == null ? "" : this._GROSS_WEIGHT;
            }
            set
            {
                if ((this._GROSS_WEIGHT != value))
                {
                    this._GROSS_WEIGHT = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_TARE_WEIGHT", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string TARE_WEIGHT
        {
            get
            {
                return this._TARE_WEIGHT == null ? "" : this._TARE_WEIGHT;
            }
            set
            {
                if ((this._TARE_WEIGHT != value))
                {
                    this._TARE_WEIGHT = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_NET_WEIGHT", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string NET_WEIGHT
        {
            get
            {
                return this._NET_WEIGHT == null ? "" : this._NET_WEIGHT;
            }
            set
            {
                if ((this._NET_WEIGHT != value))
                {
                    this._NET_WEIGHT = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_VENDOR_NET_WEIGHT", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string VENDOR_NET_WEIGHT
        {
            get
            {
                return this._VENDOR_NET_WEIGHT == null ? "" : this._VENDOR_NET_WEIGHT;
            }
            set
            {
                if ((this._VENDOR_NET_WEIGHT != value))
                {
                    this._VENDOR_NET_WEIGHT = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_TAKE_DEPT", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string TAKE_DEPT
        {
            get
            {
                return this._TAKE_DEPT == null ? "" : this._TAKE_DEPT;
            }
            set
            {
                if ((this._TAKE_DEPT != value))
                {
                    this._TAKE_DEPT = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_SEND_OUT_DEPT", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string SEND_OUT_DEPT
        {
            get
            {
                return this._SEND_OUT_DEPT == null ? "" : this._SEND_OUT_DEPT;
            }
            set
            {
                if ((this._SEND_OUT_DEPT != value))
                {
                    this._SEND_OUT_DEPT = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_GROUP_NAME", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GROUP_NAME
        {
            get
            {
                return this._GROUP_NAME == null ? "" : this._GROUP_NAME;
            }
            set
            {
                if ((this._GROUP_NAME != value))
                {
                    this._GROUP_NAME = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_IMPORT_DATE", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<DateTime> IMPORT_DATE
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
        [Column(Storage = "_IMPORT_DATE_STR", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string IMPORT_DATE_STR
        {
            get
            {
                return this._IMPORT_DATE_STR == null ? "" : this._IMPORT_DATE_STR;
            }
            set
            {
                if ((this._IMPORT_DATE_STR != value))
                {
                    this._IMPORT_DATE_STR = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_FILE_NAME", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string FILE_NAME
        {
            get
            {
                return this._FILE_NAME == null ? "" : this._FILE_NAME;
            }
            set
            {
                if ((this._FILE_NAME != value))
                {
                    this._FILE_NAME = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_GUID_KEY", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GUID_KEY
        {
            get
            {
                return this._GUID_KEY == null ? "" : this._GUID_KEY;
            }
            set
            {
                if ((this._GUID_KEY != value))
                {
                    this._GUID_KEY = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_ROW_INDEX", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<int> ROW_INDEX
        {
            get
            {
                return this._ROW_INDEX;
            }
            set
            {
                if ((this._ROW_INDEX != value))
                {
                    this._ROW_INDEX = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_REPORT_DATE", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<DateTime> REPORT_DATE
        {
            get
            {
                return this._REPORT_DATE;
            }
            set
            {
                if ((this._REPORT_DATE != value))
                {
                    this._REPORT_DATE = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_UNIT_PRICE", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> UNIT_PRICE
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
        [Column(Storage = "_TOTAL_PRICE", CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual Nullable<double> TOTAL_PRICE
        {
            get
            {
                return this._TOTAL_PRICE;
            }
            set
            {
                if ((this._TOTAL_PRICE != value))
                {
                    this._TOTAL_PRICE = value;
                }
            }
        }
        #endregion
    }
}

