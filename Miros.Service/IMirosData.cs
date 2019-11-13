using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Miros.Service.Models;

namespace Miros.Service
{
    public interface IMirosData : IDisposable
    {
        bool IsFileOpened { get; }

        void OpenFile(string filePath);
        void CloseFile();

        FileSystemData GetSystemData();
        FileStatisticsData GetStatisticsData();
        FileAuxiliaryData GetAuxilaryData();
        FileRegisterData GetRegisterData();
        FileImageData GetImageData();
    }

    #region Miros service exceptions
    [Serializable]
    public class MirosDataException : ApplicationException
    {
        public MirosDataException() { }
        public MirosDataException(string message) : base(message) { }
        public MirosDataException(string message, Exception inner) : base(message, inner) { }
        protected MirosDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class MirosDataFileNotExistException : MirosDataException
    {
        public MirosDataFileNotExistException() { }
        public MirosDataFileNotExistException(string message) : base(message) { }
        public MirosDataFileNotExistException(string message, Exception inner) : base(message, inner) { }
        protected MirosDataFileNotExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class MirosDataOpenFileException : MirosDataException
    {
        public MirosDataOpenFileException() { }
        public MirosDataOpenFileException(string message) : base(message) { }
        public MirosDataOpenFileException(string message, Exception inner) : base(message, inner) { }
        protected MirosDataOpenFileException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } 
    #endregion
}
