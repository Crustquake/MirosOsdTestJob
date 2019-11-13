using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miros.Data.DataModels
{
    public class SectionsSizes
    {
        public const int FORMAT_FIELD_SIZE = 10;
        private const int SECTION_SIZE_FIELD_SIZE = 4;

        public uint SystemDataSectionSize { get; private set; }
        public uint StatisticsDataSectionSize { get; private set; }
        public uint AuxiliaryDataSectionSize { get; private set; }
        public uint RegisterDataSectionSize { get; private set; }
        public uint ImageDataSectionSize { get; private set; }

        public SectionsSizes(uint systemDataSectionSize,
            uint statisticsDataSectionSize,
            uint auxiliaryDataSectionSize,
            uint registerDataSectionSize,
            uint imageDataSectionSize)
        {
            SystemDataSectionSize = systemDataSectionSize;
            StatisticsDataSectionSize = statisticsDataSectionSize;
            AuxiliaryDataSectionSize = auxiliaryDataSectionSize;
            RegisterDataSectionSize = registerDataSectionSize;
            ImageDataSectionSize = imageDataSectionSize;
        }

        public long GetSystemDataSectionOffset()
        {
            return FORMAT_FIELD_SIZE + 5 * SECTION_SIZE_FIELD_SIZE;
        }
        public long GetStatisticsDataSectionOffset()
        {
            return GetSystemDataSectionOffset() + SystemDataSectionSize;
        }
        public long GetAuxiliaryDataSectionOffset()
        {
            return GetStatisticsDataSectionOffset() + StatisticsDataSectionSize;
        }
        public long GetRegisterDataSectionOffset()
        {
            return GetAuxiliaryDataSectionOffset() + AuxiliaryDataSectionSize;
        }
        public long GetImageDataSectionOffset()
        {
            return GetRegisterDataSectionOffset() + RegisterDataSectionSize;
        }
    }
}
