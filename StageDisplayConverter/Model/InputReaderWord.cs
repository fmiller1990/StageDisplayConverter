using StageDisplayConverter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageDisplayConverter.Model
{
    class InputReaderWord
    {

        //private Dictionary<string, int> IndicesByQualifiers = new Dictionary<string, int>();
        //private Dictionary<int, string> QualifiersByIndex = new Dictionary<int, string>();
        WordHandler WH = new Helpers.WordHandler();

        internal void Dispose() {
            WH.CloseApplication();
        }

        internal LeadSheet ReadWordFile(string inputPath) {

            var documentInfo = WH.RetrieveTextWithPosition(inputPath);
            var header = documentInfo.Header;
            var documentLines = documentInfo.Text.Split('\r', '\a', '\f', '\v', '\n').ToList();
            string title = "";


            if (header.Contains("0")|| header.Contains("\t")) { //zero in header is equivalent to tab
                title = header.Split('0','\t')[1];
            }
            else {
                title = documentLines[0];
                documentLines.RemoveAt(0);

                if (documentLines[0].TrimStart('\t', ' ').StartsWith("(")) {
                    //second line starts with bracket --> also part of the title, but not relevant to be on the screen
                    documentLines.RemoveAt(0);
                }
            }

            var ls = new LeadSheet(title);

            var currentCategoryName = "";
            var currentCategoryLines = new List<string>();
            // organize by type
            foreach (var documentLine in documentLines) {
                var line = documentLine;
                if (IsCategoryQualifier(line)) {

                    //write to Leadsheet 
                    ls.WriteToCategory(currentCategoryName, currentCategoryLines);
                    ls.Order += currentCategoryName + "|";

                    //Get new Type
                    currentCategoryLines = new List<string>();

                    //Get Category Name
                    currentCategoryName = GetCategoryQualifier(line);
                    //it might be that there is already real text within this line
                    var remainingLine = TrimLineFromQualifier(line, currentCategoryName);
                    if (!String.IsNullOrEmpty(remainingLine.Trim()))
                        currentCategoryLines.Add(remainingLine);
                }
                else {
                    //only non- Blanks 
                    if (line.Trim() != string.Empty)
                        currentCategoryLines.Add(line);//do not add .Trim() here, bc. we also need the whitespaces
                }
            }

            //also write last Category
            ls.WriteToCategory(currentCategoryName, currentCategoryLines);
            ls.Order += currentCategoryName;

            return ls;
        }

        private bool IsCategoryQualifier(string textLine) {

            if (!String.IsNullOrEmpty(GetCategoryQualifier(textLine)))
                return true;
            else return false;
        }

        private string GetCategoryQualifier(string textLine) {
            foreach (var qualifier in Helpers.QualifierHelper.QualifiersWithColors.Keys) {
                if (textLine.Replace(" ", "").Replace("[", "").ToUpper().StartsWith(qualifier.ToUpper()))
                    return qualifier;
            }
            return String.Empty;
        }

        private string TrimLineFromQualifier(string textLine, string qualifier) {
            int indentMakeup = qualifier.Length;
            foreach (var character in qualifier) {
                //sometimes there is a " " in the textline, so we need to remove only the parts of the actual qualifier
                // not : textLine = textLine.Remove(0, qualifier.Length);
                while ((textLine.StartsWith(" ") || textLine.StartsWith(" ") || textLine.StartsWith(" "))){
                    textLine = textLine.Remove(0, 1);
                    indentMakeup ++;
                }
                textLine = textLine.Remove(0, 1);
            }
            textLine = textLine.TrimStart('[', ']',':');
            textLine = new string(' ', indentMakeup) + textLine;//TODO Simply Replacing a char with space is not exact due to not all chars being equally wide to space
            return textLine;
        }
    }
}
