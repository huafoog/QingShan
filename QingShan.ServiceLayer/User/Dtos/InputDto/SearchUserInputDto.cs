using QingShan.Data;
using QingShan.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.User.Dtos.InputDto
{
    public class SearchUserInputDto: PageInputDto
    {
        public EAdministratorStatus? Status { get; set; }
    }
}
