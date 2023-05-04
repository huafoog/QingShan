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
        $if(checkField.IsShow(item.PropName))
        /// <summary>
		/// $item.Remark
        /// </summary>
        public $item.CsType $item.PropName { get; set; }
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
