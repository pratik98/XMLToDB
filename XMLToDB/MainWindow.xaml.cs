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
namespace XMLToDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog opnfiledlg = new OpenFileDialog();
            //if(opnfiledlg.ShowDialog()==true)
            //{
            //    String dirPath = opnfiledlg.FileName;
            //    System.Windows.MessageBox.Show(dirPath);
            //}

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();

            if(!String.IsNullOrWhiteSpace(fbd.SelectedPath))
            {

                txtEditor.Text = getXml(fbd.SelectedPath);
               
            }


        }

        private string getXml(String Path)
        { 
             StringBuilder sb = new StringBuilder();
             if (!String.IsNullOrWhiteSpace(Path))
             {
                 
                 string[] files = Directory.GetFiles(Path);
                 string[] folders = Directory.GetDirectories(Path);

                 int fldrCount = folders.Length;
                 int fileCount = files.Length;
                 if(fldrCount ==0 && fileCount ==0)
                 {
                     return sb.ToString();
                 }

                 while (fldrCount > 0)
                 {
                     fldrCount--;
                     sb.AppendLine(getXml(folders[fldrCount]));
                     
                 }
                 while(fileCount >0)
                 {
                     fileCount--;
                     sb.AppendLine("Path:"+files[fileCount]);
                    // sb.Append( File.ReadAllText(files[fileCount]));
                                         
                 }
             }
             return sb.ToString();
        }
    }
}
