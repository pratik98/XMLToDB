using System;
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
            XMLProcessor xmlProObj = new XMLProcessor();
            StringBuilder jsonText = new StringBuilder();
            if(!String.IsNullOrWhiteSpace(fbd.SelectedPath))
            {                
                var filePaths = xmlProObj.getXmlFiles(fbd.SelectedPath);

                string xmlContent = string.Empty;
                 var json = new object();
                      
                foreach (var item in filePaths)
                {
                    try
                    {
                        xmlContent = string.Empty;
                        xmlContent=File.ReadAllText(item.ToString());

                        json = new JavaScriptSerializer().Serialize(xmlProObj.GetXmlData(XElement.Parse(xmlContent)));
                    }
                    catch (Exception ex)
                     {
                        logger.log("\n"+ex.StackTrace.ToString());
                    }
                    jsonText.AppendLine(json.ToString());
                }
                
               }
            txtEditor.Text = jsonText.ToString();
        }
               
    }
}
