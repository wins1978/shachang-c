/* 
Name: EXCEL_IMPORT_DAILY
Description: Extends Linq Type Defines about Table : EXCEL_IMPORT_DAILY
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
    [Table(Name = "EXCEL_IMPORT_DAILY")]
    [DataContract()]
    [Serializable]
    public partial class EXCEL_IMPORT_DAILY : Tencent.OA.ATQ.Model.IModel
    {
        #region 表名称常量

        /// <summary>
        /// 表名称
        /// </summary>
        public const string TABLE_NAME = "EXCEL_IMPORT_DAILY";
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
        private DateTime? _IMPORT_DATE;
        private DateTime? _CREATE_DATETIME;
        private string _IMPORT_DATE_STR;
        private string _FILE_NAME;
        private string _GUID_KEY;
        private int? _ROW_INDEX;
        private DateTime? _REPORT_DATE;
        private decimal? _UNIT_PRICE;
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
        /// 首次称重操作员
        /// </summary>
        [Column(Storage = "_DRIVER_NAME", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string DRIVER_NAME
        {
            get
            {
                return this._DRIVER_NAME ?? "";
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
        /// 磅单编号
        /// </summary>
        [Column(Storage = "_NUMBER_FLAG", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string NUMBER_FLAG
        {
            get
            {
                return this._NUMBER_FLAG ?? "";
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
        /// 首次称重日期
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
        /// 毛重时间
        /// </summary>
        [Column(Storage = "_TIME_STR", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string TIME_STR
        {
            get
            {
                return this._TIME_STR ?? "";
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
        /// 车号
        /// </summary>
        [Column(Storage = "_CAR_NUMBER", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string CAR_NUMBER
        {
            get
            {
                return this._CAR_NUMBER ?? "";
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
        /// 货物名称
        /// </summary>
        [Column(Storage = "_SHOP_NUMBER", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string SHOP_NUMBER
        {
            get
            {
                return this._SHOP_NUMBER ?? "";
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
        /// 毛重
        /// </summary>
        [Column(Storage = "_GROSS_WEIGHT", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GROSS_WEIGHT
        {
            get
            {
                return this._GROSS_WEIGHT ?? "";
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
        /// 皮重
        /// </summary>
        [Column(Storage = "_TARE_WEIGHT", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string TARE_WEIGHT
        {
            get
            {
                return this._TARE_WEIGHT ?? "";
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
        /// 净重
        /// </summary>
        [Column(Storage = "_NET_WEIGHT", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string NET_WEIGHT
        {
            get
            {
                return this._NET_WEIGHT ?? "";
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
        /// 供应商净重
        /// </summary>
        [Column(Storage = "_VENDOR_NET_WEIGHT", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string VENDOR_NET_WEIGHT
        {
            get
            {
                return this._VENDOR_NET_WEIGHT ?? "";
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
        /// 收货单位
        /// </summary>
        [Column(Storage = "_TAKE_DEPT", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string TAKE_DEPT
        {
            get
            {
                return this._TAKE_DEPT ?? "";
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
        /// 发货单位
        /// </summary>
        [Column(Storage = "_SEND_OUT_DEPT", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string SEND_OUT_DEPT
        {
            get
            {
                return this._SEND_OUT_DEPT ?? "";
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
        /// 石粉、石仔
        /// </summary>
        [Column(Storage = "_GROUP_NAME", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GROUP_NAME
        {
            get
            {
                return this._GROUP_NAME ?? "";
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
        [Column(Storage = "_IMPORT_DATE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual DateTime? IMPORT_DATE
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
        [Column(Storage = "_CREATE_DATETIME", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual DateTime? CREATE_DATETIME
        {
            get
            {
                return this._CREATE_DATETIME;
            }
            set
            {
                if ((this._CREATE_DATETIME != value))
                {
                    this._CREATE_DATETIME = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Storage = "_IMPORT_DATE_STR", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string IMPORT_DATE_STR
        {
            get
            {
                return this._IMPORT_DATE_STR ?? "";
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
        [Column(Storage = "_FILE_NAME", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string FILE_NAME
        {
            get
            {
                return this._FILE_NAME ?? "";
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
        [Column(Storage = "_GUID_KEY", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual string GUID_KEY
        {
            get
            {
                return this._GUID_KEY ?? "";
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
        [Column(Storage = "_ROW_INDEX", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual int? ROW_INDEX
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
        [Column(Storage = "_REPORT_DATE", IsPrimaryKey = false, CanBeNull = true)]
        [DataMember(IsRequired = true)]
        public virtual DateTime? REPORT_DATE
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
        /// 主表中，用做外键的列的名称
        /// </summary>
		public virtual string ForeignKeyName { get; private set; }
        #endregion

        public EXCEL_IMPORT_DAILY()
        {
        }
    }
}
