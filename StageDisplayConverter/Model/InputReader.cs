using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace StageDisplayConverter.Model
{
    class InputReader : IDisposable
    {
        InputReaderWord IRW = new InputReaderWord();
        public void Dispose() {
            IRW.Dispose();
        }

        internal LeadSheet ReadInputFile(string inputPath) {
            if (String.IsNullOrWhiteSpace(inputPath))
                return null;
            if (inputPath.EndsWith(".doc") || inputPath.EndsWith(".docx")) {
                return IRW.ReadWordFile(inputPath);
            }
            else {
                System.Windows.MessageBox.Show("Dateityp nicht unterstützt. Bitte doc oder docx verwenden.");
            }

            return null;
        }




    }
}
