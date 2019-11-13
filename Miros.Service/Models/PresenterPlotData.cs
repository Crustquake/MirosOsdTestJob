using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Miros.Service.Models
{
    public class PresenterPlotData
    {


        public WriteableBitmap PlotWriteableBitmap { get; private set; }
        public Orientation PlotOrientation { get; private set; }


        public PresenterPlotData(WriteableBitmap plotWriteableBitmap, Orientation orientation )
        {
            PlotWriteableBitmap = plotWriteableBitmap;
            PlotOrientation = orientation;
        }
    }
}
