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
        $if(item.PropName == "CreateTime")
        ${ elif(item.PropName == "DeleteTime")}
         ${ elif(item.PropName == "CreatedId")}
         ${ elif(item.PropName == "Id")}
        $else
            <text>
            /// <summary>
		    /// $item.Remark
            /// </summary>
            public $item.CsType $item.PropName { get; set; }
            </text>
        ${end}
${end}
$end
    }
}
