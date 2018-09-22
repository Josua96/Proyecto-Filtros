using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class OneParamFilterInfo
    {
        public int id { get; set; }
        public string image { get; set; }
        public string filterName { get; set; }

        public double paramValue { get; set; }

    }
}