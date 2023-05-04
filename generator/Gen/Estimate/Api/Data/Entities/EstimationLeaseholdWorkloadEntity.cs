using System;
using QingShan.Data;
using FreeSql.DataAnnotations;

namespace Estimate.Api.Data.Entities
{
    /// <summary>
	/// @Model.Remark
    /// </summary>
	[Table(Name ="Estimation_LeaseholdWorkload")]
    public class EstimationLeaseholdWorkloadEntity : QingShan.DatabaseAccessor.EntityBase
    {


        
        
        
            /// <summary>
		    /// 区域
            /// </summary>
            public System.Int32 AreaId { get; set; }
        
        
        
        
        
        
        
        
        
            /// <summary>
		    /// 征租地（万元）
            /// </summary>
            public System.Decimal Fee { get; set; }
        
        
        
            /// <summary>
		    /// 单位（井口）
            /// </summary>
            public System.Int32 Unit { get; set; }
        
        
        
    }
}
