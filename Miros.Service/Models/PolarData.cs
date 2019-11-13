using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miros.Service.Models
{
    public class PolarData
    {
        public IEnumerable<PolarPoint> Points { get; private set; }
        public double RadianAngleResolution { get; private set; }

        public PolarData(IEnumerable<PolarPoint> points, double radianAngleResolution)
        {
            Points = points;
            RadianAngleResolution = radianAngleResolution;
        }

    }
}
