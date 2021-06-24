using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMongoBookAPI.Middleware
{
    public class Mongosettings : IMongosettings
    {
        public string Connection { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
