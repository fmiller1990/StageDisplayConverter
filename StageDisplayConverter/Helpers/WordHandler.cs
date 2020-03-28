using Microsoft.Office.Core;
using StageDisplayConverter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWord = Microsoft.Office.Interop.Word;
namespace StageDisplayConverter.Helpers
{
    class WordHandler
    {
        public const int spaceWidthAtArial10 = 3;

        WinWord.Application application = new WinWord.Application();
        WinWord.Document doc;

        internal string RetrieveText(string inputPath) {
            var text = doc.Content.Text;
            doc.Close();
            return text;
        }

        string Text = "";
        internal WordDocumentInfo RetrieveTextWithPosition(string inputPath) {
            var ret = new WordDocumentInfo();
            Text = "";
            doc = OpenDocument(inputPath);

            //get header
            string header = "";
            foreach (WinWord.Section aSection in application.ActiveDocument.Sections) {
                foreach (WinWord.HeaderFooter aHeader in aSection.Headers)
                    header += aHeader.Range.Text;
            }
            ret.Header = header;


            int left = 0;
            int top = 0;
            int width = 0;
            int height = 0;


            WinWord.Window w = doc.ActiveWindow;
            WinWord.Paragraphs paragraphs = doc.Paragraphs;

            int charIndex = 0;
            int unindentedParagraphLeft=0;
            foreach (WinWord.Paragraph paragraph in paragraphs) {
                var range = paragraph.Range;

                if (!(range.TextVisibleOnScreen == 1)) {
                    //text not visible --> change view
                    application.GoForward();
                }


                if (range.ParagraphFormat.FirstLineIndent != 0) { // if it is indented
                    //get difference to unindented Line and add as space
                    w.GetPoint(out left, out top, out width, out height, range);
                    ReplaceTab(left - unindentedParagraphLeft, range.Font.Size);

                }
                else {
                    try {
                        w.GetPoint(out left, out top, out width, out height, range);
                    }
                    catch {
                        
                    }
                    finally {
                        unindentedParagraphLeft = left;
                    }
                }


                for (int i = 0; i < paragraph.Range.Text.Count(); i++) {//for each character in the paragraph
                    range.Start = charIndex;
                    range.End = charIndex + 1;

                    //if its a tab
                    if (range.Text == "\t"|| range.Text == "\r") {
                        //determine width
                        w.GetPoint(out left, out top, out width, out height, range);
                        ReplaceTab((float)width, range.Font.Size);

                    }
                    //if its a normal char
                    else {
                        Text += range.Text;
                    }
                    charIndex++;
                }
                Text += "\n";
            }
            ret.Text = Text;

            doc.Close(SaveChanges: MsoTriState.msoFalse);
            return ret;
        }

        private void ReplaceTab(float width, float fontsize) {
            //determine textsize 
            var fontSize = fontsize;
            //determine factor
            var fontSizeSpaceWidth = (fontSize * spaceWidthAtArial10) / 10;
            //how many spaces are 1 tab
            int spaceCount = (int)(width / fontSizeSpaceWidth);
            Text += new String(' ', spaceCount);

        }

        internal void CloseApplication() {

            // Close word.
            application.Quit();
        }

        private WinWord.Document OpenDocument(string inputPath) {
            object missing = System.Reflection.Missing.Value;
            return application.Documents.Open(inputPath, ReadOnly: MsoTriState.msoCTrue);
        }

    }
}
