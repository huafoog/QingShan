using System;
using QingShan.Data;
using FreeSql.DataAnnotations;

namespace $model.Namespace
{
    /// <summary>
	/// @Model.Remark
    /// </summary>
	[Table(Name ="${model.RealName}")]
    public class ${model.Name}Entity : QingShan.DatabaseAccessor.EntityBase
    {
$if(model.ColumnConfig != null)
$for(item in model.ColumnConfig)
        $if(checkField.IsShowWithId(item.PropName))
            /// <summary>
		    /// $item.Remark
            /// </summary>
            public $item.CsType $item.PropName { get; set; }
        ${end}
        ${end}
        $end
    }
}
