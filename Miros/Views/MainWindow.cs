using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using Microsoft.Win32;

using Miros.Service;
using Miros.Service.Models;



namespace Miros.Presentation.Views
{
    public partial class MainWindow
    {
        private IMirosService _mirosService;
        private RoutedEventHandler _openFile = delegate { };
        private CancelEventHandler _closeView = delegate { };

        public MainWindow(IMirosService mirosService)
            : this()
        {
            _mirosService = mirosService;

            _openFile = (sender, args) => OpenAndShowFile();
            openFileButton.Click += _openFile;

            _closeView = (sender, args) => CloseView();
            this.Closing += _closeView;

            Show();
            Dispatcher.Run();
        }
        private void CloseView()
        {
            Dispatcher.ExitAllFrames();
        }
        private void OpenAndShowFile()
        {
            try
            { 
                var filePath = SelectFileFromDialog();

                if (filePath != null)
                    ReadFileAndShowPlot(filePath);
            }
            catch (Exception exception)
            {
                infoTextBlock.Text = exception.ToString();
            }
        }
        private string SelectFileFromDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;
            else
                return null;
        }
        private void ReadFileAndShowPlot(string filepath)
        {
            filePathTextBlock.Text = filepath;
            _mirosService.OpenNewFile(filepath);

             var plotData = _mirosService.GetPlotData();

            //ToDo: use readable names
            plotInfoTextBlock.Text = $"{plotData.PlotOrientation.ToString()} orientation";

            plotImage.Source = plotData.PlotWriteableBitmap;
        }
    }
}
