using System;

namespace $model.Namespace
{
    /// <summary>
	/// $model.Remark
    /// </summary>
	public class ${model.Name}OutputDto
    {
    $foreach(item in Model.ColumnConfig)
    $if(checkField.IsShow(item.PropName))
        /// <summary>
		/// $item.Remark
        /// </summary>
        public $item.CsType $item.PropName { get; set; }
    $end
    $end
    }
}
