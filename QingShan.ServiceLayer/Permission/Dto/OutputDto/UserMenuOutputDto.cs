using QingShan.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.Permission.Dto.OutputDto
{
    public class UserMenuOutputDto: TreeNodeDto<UserMenuOutputDto>
    {
        public string Path { get; set; }

        public string Component { get; set; }

        public List<UserMenuOutputDto> routes => Children;

    }
}
