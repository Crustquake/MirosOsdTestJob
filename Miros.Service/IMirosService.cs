using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Miros.Service.Models;

namespace Miros.Service
{
    public interface IMirosService
    {
        void OpenNewFile(string filePath);
        PresenterPlotData GetPlotData();
    }

    #region Miros service exceptions
    [Serializable]
    public class MirosServiceException : ApplicationException
    {
        public MirosServiceException() { }
        public MirosServiceException(string message) : base(message) { }
        public MirosServiceException(string message, Exception inner) : base(message, inner) { }
        protected MirosServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class MirosServiceFileOpeningException : MirosServiceException
    {
        public MirosServiceFileOpeningException() { }
        public MirosServiceFileOpeningException(string message) : base(message) { }
        public MirosServiceFileOpeningException(string message, Exception inner) : base(message, inner) { }
        protected MirosServiceFileOpeningException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } 
    #endregion
}
