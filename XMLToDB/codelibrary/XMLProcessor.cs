using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
namespace XMLToDB.codelibrary
{
    class XMLProcessor
    {
        List<string> allPaths = new List<string>();
        ILogger logger;
        public List<String> getXmlFiles(String Path)
        {
            if (!String.IsNullOrWhiteSpace(Path))
            {
                string[] files = Directory.GetFiles(Path);
                string[] folders = Directory.GetDirectories(Path);

                int fldrCount = folders.Length;
                int fileCount = files.Length;
                if (fldrCount == 0 && fileCount == 0)
                {
                    return allPaths;
                }
                            
                while (fileCount > 0)
                {
                    fileCount--;
                    if (files[fileCount].EndsWith(".xml") || files[fileCount].EndsWith(".xml.Meta"))
                    {
                        allPaths.Add(files[fileCount]);
                    }
                    
                }
                while (fldrCount > 0)
                {
                    fldrCount--;
                    getXmlFiles(folders[fldrCount]);
                }
            }
            return allPaths;
        }
        public  Dictionary<string, object> GetXmlData(XElement xml)
        {
            var attr = xml.Attributes().ToDictionary(d => d.Name.LocalName, d => (object)d.Value);
            if (xml.HasElements) attr.Add("_value", xml.Elements().Select(e => GetXmlData(e)));
            else if (!xml.IsEmpty) attr.Add("_value", xml.Value);
            return new Dictionary<string, object> { { xml.Name.LocalName, attr } };
        }
        
    }
}
