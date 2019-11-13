using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miros.Service.Models
{
    public class FileFormat
    {
        public string NameAndVersion { get; private set; }

        public FileFormat(string nameAndVersion)
        {
            NameAndVersion = nameAndVersion;
        }        
    }
}
