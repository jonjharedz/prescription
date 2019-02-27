using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webtest.Utility
{
    public static  class CommonHelper
    {

        public static string GenerateNewId(long pId)
        {
            return "PRSC-" + DateTime.Now.Year + "-" + pId.ToString().PadLeft(6, '0');
        }

        public static long ExtractSequenceId(string PId)
        {
            if (string.IsNullOrEmpty(PId)) return -1;
            var arrId = PId.Split('-');
            if(arrId.Length>2)
            {
                return long.Parse(arrId[2]);
            } else
            {
                return -1;
            }
        }
    }
}