using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AboutMePage.Models
{


    public class InstagramItem
    {
        public List<Data> Data { get; set; }
    }
    public class Data
    {
        public string Media_Url { get; set; }
        

    }
}