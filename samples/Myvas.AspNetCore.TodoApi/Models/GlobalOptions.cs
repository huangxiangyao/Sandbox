using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.TodoApi.Models
{
    public class GlobalOptions
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
