using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetCourseSqlDataReader
{
    public class UserInfoModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public int DeptId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
