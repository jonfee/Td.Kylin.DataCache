using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.DataCache.CacheModel
{
    public class B2CProductCategoryCacheModel
    {
        ///<summary>
		///商品类目ID
		///</summary>
		public long CategoryID { get; set; }

        /// <summary>
        /// 所属区域ID
        /// </summary>
        public int AreaID { get; set; }

        ///<summary>
        ///类目名称
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///类目图标
        ///</summary>
        public string Ico { get; set; }

        ///<summary>
        ///排序值
        ///</summary>
        public int OrderNo { get; set; }
    }
}
