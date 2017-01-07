﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using XMLToDB.codelibrary;
namespace XMLToDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> allPaths = new List<string>();
        ILogger logger;
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            logger = new FileLogger();
            StringBuilder jsonText = new StringBuilder();
            if(!String.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                var filePaths = getXmlFile(fbd.SelectedPath);
                StringBuilder sb = new StringBuilder();
                 var json = new object();
                          
                foreach (var item in filePaths)
                {
                    try
                    {  
                        sb.Append(File.ReadAllText(item.ToString()));
                        json = new JavaScriptSerializer().Serialize(GetXmlData(XElement.Parse(sb.ToString())));
                    }
                    catch (Exception ex)
                     {
                        logger.log("\n"+ex.StackTrace.ToString());
                    }
                }
                 jsonText.AppendLine( json.ToString());
               }
            txtEditor.Text = jsonText.ToString();
        }
        private List<String> getXmlFile(String Path)
        {
            
             if (!String.IsNullOrWhiteSpace(Path))
             {
                 string[] files = Directory.GetFiles(Path);
                 string[] folders = Directory.GetDirectories(Path);

                 int fldrCount = folders.Length;
                 int fileCount = files.Length;
                 if(fldrCount ==0 && fileCount ==0)
                 {
                     return allPaths;
                 }

                 while (fldrCount > 0)
                 {
                     fldrCount--;
                     //sb.AppendLine(getXmlFile(folders[fldrCount]));

                    getXmlFile(folders[fldrCount]);
                 }
                 while(fileCount >0)
                 {
                     fileCount--;
                   //  sb.AppendLine("Path:"+files[fileCount]);
                     allPaths.Add( files[fileCount]);
                                         
                 }
             }
             return allPaths;
        }
        private static Dictionary<string, object> GetXmlData(XElement xml)
        {
            var attr = xml.Attributes().ToDictionary(d => d.Name.LocalName, d => (object)d.Value);
            if (xml.HasElements) attr.Add("_value", xml.Elements().Select(e => GetXmlData(e)));
            else if (!xml.IsEmpty) attr.Add("_value", xml.Value);
             return new Dictionary<string, object> { { xml.Name.LocalName, attr } };
        }
    }
}
