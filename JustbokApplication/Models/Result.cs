using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class Result
    {
        public Result()
        {
            Items = new object();
        }
        public object Items { get; set; }
        public int ItemCount { get; set; }
    }
}
