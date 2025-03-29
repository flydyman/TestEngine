using System.IO;
using System.IO.Compression;
using System.Xml;

namespace TestEngine.Models
{
    public class FileManager
    {
        List<FileGameModel> _files = new List<FileGameModel>();
        FileGameModel? expl;
        XmlDocument xml;
        /// <summary>
        /// Open zipped file and load it's content in memory
        /// </summary>
        /// <param name="fileName"></param> 
        public FileManager(string fileName)
        {
            if (File.Exists(fileName))
            {
                // load and unpack
                using(FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry entry in arch.Entries)
                        {
                            FileGameModel f = new FileGameModel();    
                            if (entry.Name == "info.xml")
                            {
                                f.Name = "info";
                                f.Alias = "info";
                                // Read content into string
                                using (var o  = new StreamReader(entry.Open()))
                                {
                                    f.Content = o.ReadToEnd();
                                }
                                f.FileType = FileGameType.Explanator;
                                expl = f;
                                xml = new XmlDocument();
                                xml.LoadXml(expl.Content);
                            } else if (expl == null)
                            {
                                Console.WriteLine("Unexpected missing explanator");
                            } else {
                                // TODO: parsing into separate module
                                //f.Name = xml
                            }
                            if (!String.IsNullOrEmpty(f.Name)) _files.Add(f);
                        }
                    }
                }
            } else {
                throw new FileNotFoundException($"File \"{fileName}\" is not found!");
            }

        }
    }
}