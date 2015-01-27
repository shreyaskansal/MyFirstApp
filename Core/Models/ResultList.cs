using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ResultList<T> where T:User
    {
        public List<T> list { get; set; }
        public int TotalCount { get; set; }
       
    }
}