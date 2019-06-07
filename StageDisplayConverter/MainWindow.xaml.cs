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
using StageDisplayConverter.ViewModel;

namespace StageDisplayConverter
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal MainWindowViewModel MWVM { get; }

        public MainWindow() {
            InitializeComponent();

            ResizeMode = ResizeMode.NoResize;


            this.MWVM = new ViewModel.MainWindowViewModel(this);
            this.DataContext = MWVM;
            MWVM.ConvertClickMethod();
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
         
            MWVM.Dispose();

        }

        private void B_Convert_Click(object sender, RoutedEventArgs e) {
            MWVM.ConvertClickMethod();
        }

        private void B_RemoveWord_Click(object sender, RoutedEventArgs e) {
            MWVM.RemoveSelectedWordFile();
        }
        private void B_RemovePowerpoint_Click(object sender, RoutedEventArgs e) {
            MWVM.RemoveSelectedPowerpointFile();
        }

        private void Rectangle_Drop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (var file in files)
                    MWVM.AddWordFile(file);
            }
        }

        private void B_New_Click(object sender, RoutedEventArgs e) {
            MWVM.Reset();
        }

        private void PPTDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (var file in files)
                    MWVM.AddPowerpointFile(file);
            }
        }

        private void B_MergePPTs_Click(object sender, RoutedEventArgs e) {
            MWVM.MergePowerPoints();
        }

        private void MovePPTDown_Click(object sender, RoutedEventArgs e) {
            int i = MWVM.MovePPTDown();
            LB_PPTsToMerge.SelectedIndex = i;
        }
        private void MovePPTUp_Click(object sender, RoutedEventArgs e) {
            int i = MWVM.MovePPTUp();
            LB_PPTsToMerge.SelectedIndex = i;
        }

    }
}
