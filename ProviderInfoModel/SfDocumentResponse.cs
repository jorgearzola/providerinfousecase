using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderInfoModel
{
    public class SfDocumentResponse
    {
        public string msg { get; set; }

        public SfDocumentResponse(string message)
        {
            msg = message;
        }
    }
}
