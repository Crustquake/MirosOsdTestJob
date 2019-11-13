using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Miros.Service.Models;

namespace Miros.Service
{
    public class MirosService : IMirosService
    {
        private IMirosData _mirosData;

        public MirosService(IMirosData mirosData)
        {
            _mirosData = mirosData;
        }
        public void OpenNewFile(string filePath)
        {
            try
            {
                if (_mirosData.IsFileOpened)
                    _mirosData.CloseFile();

                _mirosData.OpenFile(filePath);
            }
            catch (Exception exception)
            {
                throw new MirosServiceFileOpeningException("Exception during open file", exception);
            }
        }

        public PresenterPlotData GetPlotData()
        {
            FileImageData imageData = _mirosData.GetImageData();
            var orientation = GetOrientation(imageData.Orientation);

            PolarData polarData = GetPolarData(imageData);

            HeatMap heatMap = new HeatMap();
            var drawableBitmap = heatMap.GetDrawableBitmap(polarData);
            
            return new PresenterPlotData(drawableBitmap, orientation);
        }

        private PolarData GetPolarData(FileImageData imageData)
        {
            var orientAngle = -Math.PI / 2; //In case orientation 'T' it's true north, in case 'R' it's vessel heading
            var radianAngleResolution = Math.PI * imageData.YResolution / 180;

            var rangeSize = imageData.Data.GetLength(0);
            var angleSize = imageData.Data.GetLength(1);

            PolarPoint[] polarPoints = new PolarPoint[rangeSize * angleSize];            

            var ranges = Enumerable
                .Range(0, rangeSize)
                .Select(rangeIndex => imageData.XStartValue + rangeIndex * imageData.XResolution)
                .ToArray();

            var angles = Enumerable
                .Range(0, angleSize)
                .Select(angleIndex => orientAngle + Math.PI * (imageData.YStartValue + angleIndex * imageData.YResolution) / 180)
                .ToArray();

            for (int rangeIndex = 0; rangeIndex < rangeSize; rangeIndex++)
            {
                var range = ranges[rangeIndex];

                for (int angleIndex = 0; angleIndex < angleSize; angleIndex++)
                {
                    var angle = angles[angleIndex];

                    polarPoints[rangeIndex + angleIndex * rangeSize] = new PolarPoint(range, angle, imageData.Data[rangeIndex, angleIndex]);
                }
            }

            PolarData polarData = new PolarData(polarPoints, radianAngleResolution);
            return polarData;
        }
        private Orientation GetOrientation(char orientationText)
        {
            if (orientationText.Equals('T'))
                return Orientation.TrueNorth;
            else if (orientationText.Equals('R'))
                return Orientation.RelalativeToVesselHeading;
            else
                throw new MirosServiceException("Undefined orientation");
        }
    }
}
