using System;
using QingShan.Data;

namespace Estimate.Api.Services.EstimationLeaseholdWorkload.Dto
{
    /// <summary>
	/// 征租地工作量
    /// </summary>
	public class EstimationLeaseholdWorkloadInputDto
    {
        
        
        /// <summary>
		/// 
        /// </summary>
        public System.String Id { get; set; }
        
        
        
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
    /// <summary>
	/// 征租地工作量
    /// </summary>
    public class PageEstimationLeaseholdWorkloadInputDto:PageInputDto
    {

    }
}
