using System;

namespace $model.Namespace
{
    /// <summary>
	/// $model.Remark
    /// </summary>
	public class ${model.Name}OutputDto
    {
    $foreach(item in Model.ColumnConfig)
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
}
