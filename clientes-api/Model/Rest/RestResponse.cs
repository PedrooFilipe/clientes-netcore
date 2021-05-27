using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clientes_api.Model.Rest
{
    public class RestResponse<T> where T : class
    {
        public T Data { get; set; }
        public string ResponseMessage {get; set;}
        public Paginator Paginator { get;  set;}
    }

    public class Paginator
    {
        public int Current { get; set; }
        public int Total { get; set; }

    }
}
