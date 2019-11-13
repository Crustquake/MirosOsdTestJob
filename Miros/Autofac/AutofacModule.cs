using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Miros.Service;
using Miros.Data;

namespace Miros.Presentation
{
    class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MirosService>().As<IMirosService>();
            builder.RegisterType<MirosData>().As<IMirosData>();
            base.Load(builder);
        }
    }
}
