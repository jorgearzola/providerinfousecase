using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderInfoModel
{
    public class SFDocument
    {
        //this is a class that will be used to transfer the data between layers. A DTO/Model.

        public DateTime Stamp { get; set; }
        public string FileName { get; set; }
        public byte[] Document { get; set; }
        public string Type { get; set; }
        public SFUser User { get; set; }


    }
}
