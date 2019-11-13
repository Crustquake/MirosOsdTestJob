using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Miros.Service;
using Miros.Service.Models;
using Miros.Data.DataModels;

namespace Miros.Data
{
    public class MirosData : IMirosData
    {       
        private FileStream _fileStream;
        private SectionsSizes _sectionsSizes;

        public bool IsFileOpened { get; private set; }

        public void OpenFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new MirosDataFileNotExistException(filePath);

            try
            {
                CloseFile();

                _fileStream = new FileStream(filePath, FileMode.Open);                
                ParseHeader();

                IsFileOpened = true;
            }
            catch
            {
                throw new MirosDataOpenFileException();
            }
        }
        public void CloseFile()
        {
            _fileStream?.Close();

            IsFileOpened = false;
        }
        private void ParseHeader()
        {
            var binaryReader = GetBinaryReaderFromPosition(SectionsSizes.FORMAT_FIELD_SIZE);

            var systemDataSectionSize = binaryReader.ReadUInt32();
            var statisticsDataSectionSize = binaryReader.ReadUInt32();
            var auxiliaryDataSectionSize = binaryReader.ReadUInt32();
            var registerDataSectionSize = binaryReader.ReadUInt32();
            var imageDataSectionSize = binaryReader.ReadUInt32();

            _sectionsSizes = new SectionsSizes(systemDataSectionSize,
                statisticsDataSectionSize,
                auxiliaryDataSectionSize,
                registerDataSectionSize,
                imageDataSectionSize);
        }
        public FileFormat GetFormat()
        {
            var binaryReader = GetBinaryReaderFromPosition(0);

            var formatName = new string(binaryReader.ReadChars(SectionsSizes.FORMAT_FIELD_SIZE));

            return new FileFormat(formatName);
        }
        public FileSystemData GetSystemData()
        {
            throw new NotImplementedException();
        }
        public FileStatisticsData GetStatisticsData()
        {
            throw new NotImplementedException();
        }
        public FileAuxiliaryData GetAuxilaryData()
        {
            throw new NotImplementedException();
        }
        public FileRegisterData GetRegisterData()
        {
            throw new NotImplementedException();
        }
        public FileImageData GetImageData()
        {
            var imageStartPoint = _sectionsSizes.GetImageDataSectionOffset();

            var binaryReader = GetBinaryReaderFromPosition(imageStartPoint);

            var imageData = ReadImageData(binaryReader);
            return imageData;
        }
        private FileImageData ReadImageData(BinaryReader binaryReader)
        {
            var orientation = binaryReader.ReadChar();

            var xNumberOfElements = binaryReader.ReadUInt32();
            var xStartValue = binaryReader.ReadSingle();
            var xResolution = binaryReader.ReadSingle();

            var yNumberOfElements = binaryReader.ReadUInt32();
            var yStartValue = binaryReader.ReadSingle();
            var yResolution = binaryReader.ReadSingle();

            var elementSize = (int)binaryReader.ReadUInt32();
            var entireSize = binaryReader.ReadUInt32();

            if (1 + 8 * 4 + entireSize * elementSize != _sectionsSizes.ImageDataSectionSize)
                throw new Exception();

            if (xNumberOfElements * yNumberOfElements != entireSize)
                throw new Exception();

            uint[,] data = new uint[xNumberOfElements, yNumberOfElements];
            byte[] buffer = new byte[] { 0, 0, 0, 0 };
            for (int index = 0; index < entireSize; index++)
            {
                binaryReader.Read(buffer, 0, elementSize);
                data[index % xNumberOfElements, index / xNumberOfElements] = BitConverter.ToUInt32(buffer, 0);
            }

            return new FileImageData(orientation, xStartValue, xResolution, yStartValue, yResolution, data);
        }
        private BinaryReader GetBinaryReaderFromPosition(long position)
        {
            _fileStream.Seek(position, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(_fileStream, Encoding.ASCII, true);
            return binaryReader;
        }

        #region IDisposable Support
        private bool isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _fileStream.Dispose();
                }
                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
