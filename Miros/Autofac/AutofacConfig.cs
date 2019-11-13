using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Autofac.Core;

namespace Miros.Presentation
{
    class AutofacConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            var container = builder.Build();
            return container;
        }
    }
}
