using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// add PowerPoint namespace 
using Powerpoint = Microsoft.Office.Interop.PowerPoint;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;

namespace StageDisplayConverter.Helpers
{



    class PowerpointHandling
    {
        private static double SlideHeightCm = 30.2;//32;
        private static double SlideWidthCm = 17;//19.125;
        private static int BorderWidthRight = 10;
        internal float PositionFromTop = BorderWidthRight;

        internal enum Orientation
        {
            Left,
            Middle,
            Right
        }


        Powerpoint.Application pptApplication = new Powerpoint.Application();
        Microsoft.Office.Interop.PowerPoint.Slides slides;
        Microsoft.Office.Interop.PowerPoint._Slide slide;
        Powerpoint.Presentation pptPresentation;

        internal void CreateNewPPT() { CreateNewPPT(""); }
        internal void CreateNewPPT(string title) {

            // Create the Presentation File
            pptPresentation = pptApplication.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoTrue);

            pptPresentation.PageSetup.SlideSize = Powerpoint.PpSlideSizeType.ppSlideSizeCustom; //equals 16*9, but also fits the typical DIN A4 Width (excluding DIN A4 Border)
            pptPresentation.PageSetup.SlideWidth = (float)MathHelper.ConvertCmToPoint(SlideWidthCm);
            pptPresentation.PageSetup.SlideHeight = (float)MathHelper.ConvertCmToPoint(SlideHeightCm);

            pptPresentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationVertical;
            var customLayout = pptPresentation.SlideMaster.CustomLayouts[7];

            // Create new Slide
            slides = pptPresentation.Slides;
            slide = slides.AddSlide(1, customLayout);
            //Test
            slide.FollowMasterBackground = Microsoft.Office.Core.MsoTriState.msoFalse;
            slide.Background.Fill.ForeColor.RGB = 0;
        }

        internal void Focus() {

            pptApplication.Visible = Microsoft.Office.Core.MsoTriState.msoCTrue;
        }

        internal void AddText(string text, float textSize, Orientation orientation, System.Drawing.Color color, bool shiftY, bool underline) {
            var powerpointColorEquivaltent = ToBgr(color);

            //System.Drawing.Size size = System.Windows.Forms.TextRenderer.MeasureText(text, new System.Drawing.Font("Arial", textSize));
            //size = ConvertSize(size);

            //PP handling is not perfect... ( because you cannot determine the future size properly beforehand) ... so
            //first write into a textbox (to get the size),... 
            Powerpoint.Shape s;
            s = slide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 0, 0, 1, 1);
            s.Apply();
            s.TextEffect.FontName = "Arial";
            s.TextEffect.FontBold = Microsoft.Office.Core.MsoTriState.msoTrue;
            s.TextEffect.FontSize = textSize;
            s.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoFalse;
            s.TextFrame.TextRange.Text = text;
            s.TextFrame.TextRange.Font.Underline = (underline) ? MsoTriState.msoCTrue : MsoTriState.msoFalse;
            s.TextFrame.TextRange.Font.Color.RGB = powerpointColorEquivaltent;
            s.TextFrame.AutoSize = Powerpoint.PpAutoSize.ppAutoSizeShapeToFitText;



            //...  then put it to the proper position, 
            var size = new Size(Convert.ToInt32(s.Width), Convert.ToInt32(s.Height));
            if (orientation == Orientation.Left) { //Category Name or Verse
                s.Left = (float)10; s.Top = PositionFromTop;
            }
            else if (orientation == Orientation.Right) {//is title
                s.Left = pptPresentation.PageSetup.SlideWidth - size.Width - BorderWidthRight; s.Top = PositionFromTop;
                s.TextEffect.Alignment = Microsoft.Office.Core.MsoTextEffectAlignment.msoTextEffectAlignmentCentered;
                s.ShapeStyle = Microsoft.Office.Core.MsoShapeStyleIndex.msoLineStylePreset3;
                s.Line.Weight = 2;
                s.Line.ForeColor.RGB = ToBgr(Color.White); s.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
            }
            // s = slide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, pptPresentation.PageSetup.SlideWidth - size.Width, PositionFromTop, size.Width, size.Height);
            else { //Category other than Verse
                s.Left = textSize * 5; s.Top = PositionFromTop;
            }


            //...then shiftY (shift the position count) if required
            if (shiftY)
                PositionFromTop += (float)(size.Height * 1.0);
        }

        internal void AddFile(string filePath) {
            int targetIndex = (pptPresentation.Slides.Count < 1) ? 1 : pptPresentation.Slides.Count +1;
            pptPresentation.Slides.Add(targetIndex, Powerpoint.PpSlideLayout.ppLayoutBlank);

            //pptPresentation.Slides[targetIndex].Background.Fill.ForeColor.RGB = 0;

            //pptPresentation.Slides[targetIndex].Shapes.AddPicture(filePath, MsoTriState.msoCrue, MsoTriState.msoFalse
            //set picture as Background
            pptPresentation.Slides[targetIndex].FollowMasterBackground = Microsoft.Office.Core.MsoTriState.msoFalse;
            pptPresentation.Slides[targetIndex].Background.Fill.UserPicture(filePath);
        }

        internal void ShiftY(double distance) {
            PositionFromTop += (float)distance;
        }
        internal void RemoveLastSlide() {
            if (pptPresentation.Slides.Count >= 1)
                pptPresentation.Slides[1].Delete();
        }
        internal void CopySlides(string filePath) {
            var pres = pptApplication.Presentations;
            var sourcePresentation = pres.Open(filePath, MsoTriState.msoTrue, MsoTriState.msoTrue, MsoTriState.msoFalse);

            for (int slideCount = 1; slideCount <= sourcePresentation.Slides.Count; slideCount++) {
                sourcePresentation.Slides[slideCount].Copy();
                int targetIndex = (pptPresentation.Slides.Count < 1) ? 1 : pptPresentation.Slides.Count;

                //a normal Paste will not keep the formating --> Execute MSO
                pptPresentation.Windows[1].View.GotoSlide(targetIndex);
                pptApplication.CommandBars.ExecuteMso("PasteSourceFormatting");

           

                //pptPresentation.Slides.Paste(targetIndex);
                //pptPresentation.Slides[targetIndex].FollowMasterBackground = Microsoft.Office.Core.MsoTriState.msoFalse;
                //pptPresentation.Slides[targetIndex].CustomLayout = sourcePresentation.Slides[slideCount].CustomLayout;
                //pptPresentation.Slides[targetIndex].Background.Fill.ForeColor.RGB = 0;
            }
            sourcePresentation.Close();
        }

        internal void Save(string path) {
            //      string fileName = Path.GetDirectoryName(
            //Assembly.GetExecutingAssembly().Location) + "\\Sample1.pptx";
            //      oPre.SaveAs(fileName,
            //          PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation,
            //          Office.MsoTriState.msoTriStateMixed);
            //      oPre.Close();
        }


        internal void CloseApplication() {
          //TODO Activate  pptApplication.Quit();

            // See Solution2.AutomatePowerPoint 
            GC.Collect();
            GC.WaitForPendingFinalizers();
            // GC needs to be called twice in order to get the Finalizers called  
            // - the first time in, it simply makes a list of what is to be  
            // finalized, the second time in, it actually is finalizing. Only  
            // then will the object do its automatic ReleaseComObject. 
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }


        public static int ToBgr(System.Drawing.Color color) {
            return ToBgr(color.R, color.G, color.B);
        }

        public static int ToBgr(int r, int g, int b) {
            //  & 0xFFFFFF -> Strip away alpha channel which powerpoint doesn't like
            return System.Drawing.Color.FromArgb(b, g, r).ToArgb() & 0xFFFFFF;
        }
    }
}
