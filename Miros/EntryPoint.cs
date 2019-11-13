using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Miros.Presentation.Views;
using Miros.Service;

namespace Miros.Presentation
{    
    class EntryPoint
    {
        [STAThread]
        static void Main()
        {
            try
            {
                var container = AutofacConfig.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var mirosService = container.Resolve<IMirosService>();
                    var mainWindow = new MainWindow(mirosService);
                }
            }
            catch(Exception exception)
            {
                Debug.WriteLine(exception.ToString());
                //TODO: add logging
            }
        }
    }
}
