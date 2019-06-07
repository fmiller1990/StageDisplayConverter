using StageDisplayConverter.Helpers;
using StageDisplayConverter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageDisplayConverter.ViewModel
{
    class MainWindowViewModel : MVVMBase
    {
        private ObservableCollection<string> _FilesToConvert = new ObservableCollection<string>();
        public ObservableCollection<string> FilesToConvert {
            get { return _FilesToConvert; }
            internal set {
                _FilesToConvert = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilesToConvert"));
            }
        }

        private string _FilesToConvert_Selected = "";
        public string FilesToConvert_Selected {
            get { return _FilesToConvert_Selected; }
            set {
                _FilesToConvert_Selected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilesToConvert_Selected"));
            }
        }

        private ObservableCollection<string> _FilesToMerge = new ObservableCollection<string>();
        public ObservableCollection<string> FilesToMerge {
            get { return _FilesToMerge; }
            internal set {
                _FilesToMerge = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilesToMerge"));
            }
        }


        private string _FilesToMerge_Selected = "";
        public string FilesToMerge_Selected {
            get { return _FilesToMerge_Selected; }
            set {
                _FilesToMerge_Selected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilesToMerge_Selected"));
            }
        }



        private MainWindow MainWindow;
        private InputReader IR = new InputReader();
        internal OutputWriterPowerpoint OW = new OutputWriterPowerpoint();

        public MainWindowViewModel(MainWindow mainWindow) {
            this.MainWindow = mainWindow;
            Reset();
        }

        internal void Reset() {
            FilesToConvert = new ObservableCollection<string>();
            FilesToMerge = new ObservableCollection<string>();
        }

        internal void RemoveSelectedWordFile() {
            if (String.IsNullOrEmpty(FilesToConvert_Selected)) {
                System.Windows.MessageBox.Show("No File to remove selected");
                return;
            }
            else {
                if (FilesToConvert.Contains(_FilesToConvert_Selected))
                    FilesToConvert.Remove(FilesToConvert_Selected);
            }
        }

        internal void MergePowerPoints() {
            OW.MoveSlides(FilesToMerge.ToList());
        }

        internal void RemoveSelectedPowerpointFile() {
            if (String.IsNullOrEmpty(FilesToMerge_Selected)) {
                System.Windows.MessageBox.Show("No File to remove selected");
                return;
            }
            else {
                if (FilesToMerge.Contains(_FilesToMerge_Selected))
                    FilesToMerge.Remove(FilesToMerge_Selected);
            }
        }

        internal void ConvertClickMethod() {
            var files = FilesToConvert.ToList().Intersect(FilesToConvert.ToList()).ToList();
            //TODO Remove
            // files = new List<string>() { @"D:\Programmierung\StageDisplayConverter\ExampleData\Doc\02 Wir rufen deinen Namen.docx" };

            foreach (var filePath in files) {
                //TODO Activate 
                var leadSheet = IR.ReadInputFile(filePath);
                //var leadSheet = new Model.LeadSheet("TestDatei");
                //leadSheet.Intro.Add( " A H M F");
                //leadSheet.Vers1.Add("Dies Ist ein Test");
                //leadSheet.Vers1.Add("Er besteht aus mehreren Zeilen");
                //leadSheet.Vers1.Add("Diese ist eine davon");
                //leadSheet.Vers1.Add("Dies Ist ein Test");
                //leadSheet.Order = "Vers1";

                if (leadSheet == null)
                    System.Windows.MessageBox.Show("Dateiname leer");
                else
                    new Model.OutputWriterPowerpoint().WriteOutputFile(leadSheet, "");

            }
        }

        internal void AddWordFile(string file) {
            if (file.EndsWith(".doc") || file.EndsWith(".docx"))
                FilesToConvert.Add(file);
        }

        internal void AddPowerpointFile(string file) {
            if (file.EndsWith(".ppt") || file.EndsWith(".pptx" )|| file.EndsWith(".jpg"))
                FilesToMerge.Add(file);
        }

        internal int MovePPTUp() {
            var i = FilesToMerge.IndexOf(FilesToMerge_Selected);
            if (i > 0) {
                var selected = FilesToMerge_Selected;
                FilesToMerge.RemoveAt(i);
                FilesToMerge.Insert(i - 1, selected);
                i--;
            }
            return i;
        }

        internal int MovePPTDown() {
            var i = FilesToMerge.IndexOf(FilesToMerge_Selected);
            if (i >= 0 && i < (FilesToMerge.Count - 1)) { // -1 bc. at the last position you cannot go further down
                var selected = FilesToMerge_Selected;
                FilesToMerge.RemoveAt(i);
                FilesToMerge.Insert(i + 1, selected);
                i++;
            }
            return i;
        }

        internal void Dispose() {

            IR.Dispose();
            OW.Dispose();


        }
    }
}
