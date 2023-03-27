using System;
using QingShan.Data;

namespace $model.Namespace
{
    /// <summary>
	/// $model.Remark
    /// </summary>
	public class ${model.Name}InputDto
    {
        $for(item in model.ColumnConfig)
        $if(item.PropName == "CreateTime")
        ${ elif(item.PropName == "DeleteTime")}
         ${ elif(item.PropName == "CreatedId")}
        $else
        <text>
        /// <summary>
		/// $item.Remark
        /// </summary>
        public $item.CsType $item.PropName { get; set; }
        </text>
    $end
    $end
    }
    /// <summary>
	/// $model.Remark
    /// </summary>
    public class Page${model.Name}InputDto:PageInputDto
    {

    }
}
