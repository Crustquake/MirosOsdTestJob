using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miros.Service.Models
{
    public class PolarPoint
    {
        public double Radius { get; private set; }
        public double Angle { get; private set; }
        public double Value { get; private set; }

        public PolarPoint(double radius, double angle, double value)
        {
            Radius = radius;
            Angle = angle;
            Value = value;
        }

    }
}
