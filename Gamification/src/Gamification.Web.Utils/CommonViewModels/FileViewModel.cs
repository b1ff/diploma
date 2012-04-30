using System.IO;

namespace Gamification.Web.Utils.CommonViewModels
{
    public class FileViewModel
    {
        public FileViewModel()
        {
        }

        public FileViewModel(string name, long sizeInBytes, Stream inputStream)
        {
            Name = name;
            SizeInBytes = sizeInBytes;
            InputStream = inputStream;
        }

        public string Name { get; private set; }

        public long SizeInBytes { get; private set; }

        public Stream InputStream { get; private set; }
    }
}
