using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeMgrLib.Model
{
    //首次称重操作员	磅单编号	首次称重日期	毛重时间	车号	货物名称	毛重	皮重	净重	收货单位	发货单位

    public class InputInfo
    {
        public string RowIndex { get; set; }

        /// <summary>
        /// 首次称重操作员 (old--驾驶员)
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 磅单编号
        /// </summary>
        public string NumberFlag { get; set; }

        /// <summary>
        /// 首次称重日期
        /// </summary>
        public string Date { get; set; }

        public string ImportDateTime { get; set; }

        /// <summary>
        /// 毛重时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 货物名称
        /// </summary>
        public string ShopNumber { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public string GrossWeight { get; set; }

        /// <summary>
        /// 皮重
        /// </summary>
        public string TareWeight { get; set; }

        /// <summary>
        /// 净重
        /// </summary>
        public string NetWeight { get; set; }

        /// <summary>
        /// 供应商净重
        /// </summary>
        public string VendorNetWeight { get; set; }

        /// <summary>
        /// 收货单位
        /// </summary>
        public string TakeDept { get; set; }

        /// <summary>
        /// 发货单位
        /// </summary>
        public string SendOutDept { get; set; }

        /// <summary>
        /// 石粉、石仔
        /// </summary>
        public string GroupName { get; set; }

        public string UnitPrice { get; set; }
    }

    public class InputResults
    {
        public string ErrMessage { get; set; }

        public List<InputInfo> InputInfoList { get; set; }
    }



}
