using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadTex_MVC_Project.Models.Connection_Model
{
    public static class Connection_Model
    {
        public static RoadTex_MVC_Model_Local DB;

        public static RoadTex_MVC_Model_Local Connect()
        {
            DB = new RoadTex_MVC_Model_Local();
            return DB;
        }
    }
}