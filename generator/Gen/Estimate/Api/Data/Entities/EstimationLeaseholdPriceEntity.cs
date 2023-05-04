using System;
using QingShan.Data;
using FreeSql.DataAnnotations;

namespace Estimate.Api.Data.Entities
{
    /// <summary>
	/// @Model.Remark
    /// </summary>
	[Table(Name ="Estimation_LeaseholdPrice")]
    public class EstimationLeaseholdPriceEntity : QingShan.DatabaseAccessor.EntityBase
    {


        
        
        
            /// <summary>
		    /// 区域
            /// </summary>
            public System.Int32 AreaId { get; set; }
        
        
        
        
        
        
        
        
        
            /// <summary>
		    /// 数量
            /// </summary>
            public System.Decimal Number { get; set; }
        
        
        
            /// <summary>
		    /// 单位
            /// </summary>
            public System.String Unit { get; set; }
        
        
        
    }
}
