using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using P = DocumentFormat.OpenXml.Presentation;
using D = DocumentFormat.OpenXml.Drawing;
using StageDisplayConverter.Helpers;

namespace StageDisplayConverter.Model
{
    class OutputWriterPowerpoint : IDisposable
    {
        Helpers.PowerpointHandling pptHandler = new Helpers.PowerpointHandling();

        public void Dispose() {
            pptHandler.CloseApplication();

        }

        internal void WriteOutputFile(LeadSheet ls, string outputPath) {
            var order = ls.Order.Split('|');

            pptHandler.CreateNewPPT();

            //write Title 
            pptHandler.AddText(ls.Title.ToUpper(), (float)20, Helpers.PowerpointHandling.Orientation.Right, System.Drawing.Color.White, false, false);

            pptHandler.ShiftY(pptHandler.PositionFromTop); //Preshift
            //write Intro Chords
            if (ls.Intro.Count != 0 && (!order.Contains("INTRO"))) {
                pptHandler.ShiftY((float)(-1.8 * Helpers.QualifierHelper.QualifiersWithTextHeight["Default"])); //Intro can fit next to the title --> so move it two lines up
                AppendText("INTRO", ls.RetrieveFromCategory("INTRO"), Helpers.QualifierHelper.QualifiersWithColors["INTRO"]);
            }



            foreach (var o in order) {
                if (!String.IsNullOrWhiteSpace(o)) {
                    if (o == "INTRO")
                        pptHandler.ShiftY((float)(-1.8 * Helpers.QualifierHelper.QualifiersWithTextHeight["Default"])); //Intro can fit next to the title --> so move it two lines up
                     AppendText(o, ls.RetrieveFromCategory(o), Helpers.QualifierHelper.QualifiersWithColors[o]);
                }
            }
        }

        private void AppendText(string category, string text, System.Drawing.Color col) {


            float  textheightCategory = Helpers.QualifierHelper.QualifiersWithTextHeight.ContainsKey(category) ? Helpers.QualifierHelper.QualifiersWithTextHeight[category] : Helpers.QualifierHelper.QualifiersWithTextHeight["Default"];
            float textheightText =  Helpers.QualifierHelper.QualifiersWithTextHeight["Default"];

            string outputCategory = Helpers.QualifierHelper.QualifiersToOutputName[category];
            if (!String.IsNullOrWhiteSpace(text)) {

                pptHandler.ShiftY(Math.Max(textheightCategory, textheightText));
                if (!outputCategory.StartsWith("VERS")) {

                    //TODO: Text is more than one line --> first line is Chords, second line is Text --> put category on Text line
                    pptHandler.AddText(outputCategory + ":", textheightCategory, Helpers.PowerpointHandling.Orientation.Left, col, false, true);
                    pptHandler.AddText(text, textheightText, Helpers.PowerpointHandling.Orientation.Middle, col, true, false);

                }
                else
                    pptHandler.AddText(text, textheightText, Helpers.PowerpointHandling.Orientation.Left, col, true, false);
            }
        }

        public void MoveSlides(List<string> paths) {

            pptHandler.CreateNewPPT();

            foreach (var filePath in paths) {
                if (filePath.EndsWith(".ppt") || filePath.EndsWith(".pptx"))
                    pptHandler.CopySlides(filePath);
                else
                    pptHandler.AddFile(filePath);
            }
            pptHandler.RemoveLastSlide();
        }

    }
}