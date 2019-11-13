using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miros.Service.Models
{
    public class FileImageData
    {
        public char Orientation { get; private set; }

        public float XStartValue { get; private set; }
        public float XResolution { get; private set; }

        public float YStartValue { get; private set; }
        public float YResolution  { get; private set; }
        
        public uint[,] Data { get; private set; }

        public FileImageData(char orientation, float xStartValue, float xResolution, float yStartValue, float yResolution, uint[,] data)
        {
            Orientation = orientation;
            XStartValue = xStartValue;
            XResolution = xResolution;
            YStartValue = yStartValue;
            YResolution = yResolution;
            Data = data;
        }
    }
}
