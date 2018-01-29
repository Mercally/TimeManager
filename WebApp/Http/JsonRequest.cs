using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeManager.WebApp.Http
{
    public class JsonRequest
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public int Id  { get; set; }
    }
}