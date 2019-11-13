using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Miros.Service.Models;

namespace Miros.Service
{
    public class HeatMap
    {
        public WriteableBitmap GetDrawableBitmap(PolarData polarData)
        {
            double maxRadius = polarData.Points.Max(point => point.Radius);
            double maxEllipseRadius = maxRadius * polarData.RadianAngleResolution;
            double minValue = polarData.Points.Min(point => point.Value);
            double maxValue = polarData.Points.Max(point => point.Value);

            if (maxValue == minValue)
                maxValue = minValue + 1;

            var cartesianPoints = polarData.Points
                .Select(point => new
                {
                    //Polar data is north(vessel) orient 
                    X = point.Radius * Math.Sin(point.Angle),
                    Y = point.Radius * Math.Cos(point.Angle),
                    Intensity = (byte)(32 * (point.Value - minValue) / (maxValue - minValue)),
                    EllipseRadius = polarData.RadianAngleResolution * point.Radius
            });

            double minX = cartesianPoints.Min(point => point.X);
            double maxX = cartesianPoints.Max(point => point.X);
            double minY = cartesianPoints.Min(point => point.Y);
            double maxY = cartesianPoints.Max(point => point.Y);


            WriteableBitmap writeableBitmap = BitmapFactory.New(
                (int)(maxX - minX + 2 * maxEllipseRadius),
                (int)(maxY - minY + 2 * maxEllipseRadius));

            
            using (writeableBitmap.GetBitmapContext())
            {
                writeableBitmap.Clear(Colors.Transparent);

                foreach (var point in cartesianPoints)
                {
                    var radius = (int)Math.Ceiling(point.EllipseRadius);
                    var color = point.Intensity * 0x01000000;
                    var xCenter = (int)(point.X - minX + 10);
                    var yCenter = (int)(point.Y - minY + 10);
                    writeableBitmap.FillEllipseCentered(xCenter, yCenter, radius, radius, color, true); 
                }
            }
            return writeableBitmap;
        }
    }
}
