using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageDisplayConverter.Model
{
    public class LeadSheet
    {
        public string Title = "";
        public Key Key; //TODO Get Key
        public string Order = "";

        public List<string> Intro = new List<string>();
        public List<string> Vers1 = new List<string>();
        public List<string> Vers2 = new List<string>();
        public List<string> Vers3 = new List<string>();
        public List<string> Vers4 = new List<string>();
        public List<string> Vers5 = new List<string>();
        public List<string> Vers6 = new List<string>();
        public List<string> Vers7 = new List<string>();
        public List<string> Vers8 = new List<string>();
        public List<string> PreChorus = new List<string>();
        public List<string> PreChorus2 = new List<string>();
        public List<string> Chorus = new List<string>();
        public List<string> Chorus2 = new List<string>();
        public List<string> Bridge = new List<string>();
        public List<string> Bridge1 = new List<string>();
        public List<string> Bridge2 = new List<string>();
        public List<string> Bridge3 = new List<string>();
        public List<string> Interlude = new List<string>();
        public List<string> Interlude1 = new List<string>();
        public List<string> Interlude2 = new List<string>();
        public List<string> Interlude3 = new List<string>();
        public List<string> Instrumental = new List<string>();
        public List<string> Outro = new List<string>();
        public List<string> Hook = new List<string>();
        public List<string> Ending = new List<string>();
        public LeadSheet(string title) {
            Title = title;
        }

        internal void WriteToCategory(string categoryName, List<string> lines) {
            //trim lines at the beginning
            //get the amount of space each line has at the beginning
            int minSpacing=Int32.MaxValue; List<string> trimmedLines = new List<string>();
            foreach (var line in lines) {
                int spaceCount = line.TakeWhile(x=>x == ' ').Count();
                minSpacing = spaceCount <= minSpacing ? spaceCount : minSpacing;
            }
            //remove whitespaces and add
            if (minSpacing != 0 && minSpacing != Int32.MaxValue) {
                foreach (var line in lines) {
                    trimmedLines.Add(line.Remove(0, minSpacing));
                }
            }
            else trimmedLines = lines;

            switch (categoryName) {
                case "":
                case "INTRO":
                    Intro.AddRange(trimmedLines);
                    break;
                case "VERSE1":
                case "VERS1":
                case "V1":
                    Vers1.AddRange(trimmedLines);
                    break;
                case "VERSE2":
                case "VERS2":
                case "V2":
                    Vers2.AddRange(trimmedLines);
                    break;
                case "VERSE3":
                case "VERS3":
                case "V3":
                    Vers3.AddRange(trimmedLines);
                    break;
          
                case "VERSE4":
                case "VERS4":
                case "V4":
                    Vers4.AddRange(trimmedLines);
                    break;
                case "VERSE5":
                case "VERS5":
                case "V5":
                    Vers5.AddRange(trimmedLines);
                    break;
                case "VERSE6":
                case "VERS6":
                case "V6":
                    Vers6.AddRange(trimmedLines);
                    break;
                case "VERSE7":
                case "VERS7":
                case "V7":
                    Vers7.AddRange(trimmedLines);
                    break;
                case "VERSE8":
                case "VERS8":
                case "V8":
                    Vers8.AddRange(trimmedLines);
                    break;
                case "VERSE":
                case "VERS":
                    Vers1.AddRange(trimmedLines);
                    break;
                case "PRE-CHORUS2":
                case "PRECHORUS2":
                    PreChorus2.AddRange(trimmedLines);
                    break;
                case "PRE-CHORUS":
                case "PRECHORUS":
                    PreChorus.AddRange(trimmedLines);
                    break;
                case "CHORUS2":
                    Chorus2.AddRange(trimmedLines);
                    break;
                case "CHORUS":
                    Chorus.AddRange(trimmedLines);
                    break;
                case "BRIDGE1":
                    Bridge1.AddRange(trimmedLines);
                    break;
                case "BRIDGE2":
                    Bridge2.AddRange(trimmedLines);
                    break;
                case "BRIDGE3":
                    Bridge2.AddRange(trimmedLines);
                    break;
                case "BRIDGE":
                    Bridge.AddRange(trimmedLines);
                    break;
                case "INTERLUDE":
                    Interlude.AddRange(trimmedLines);
                    break;
                case "INTERLUDE1":
                    Interlude1.AddRange(trimmedLines);
                    break;
                case "INTERLUDE2":
                    Interlude2.AddRange(trimmedLines);
                    break;
                case "INTERLUDE3":
                    Interlude3.AddRange(trimmedLines);
                    break;
                case "INSTRUMENTAL":
                    Instrumental.AddRange(trimmedLines);
                    break;
                case "OUTRO":
                    Outro.AddRange(trimmedLines);
                    break;
                case "HOOK":
                    Hook.AddRange(trimmedLines);
                    break;
                case "ENDING":
                    Ending.AddRange(trimmedLines);
                    break;
                default:
                    System.Windows.MessageBox.Show("Bitte Informieren Sie ihren zuständigen Entwickler unter dem Stichwort " + categoryName + " mit Fehlercode #1001");
                    throw new InvalidOperationException();
            }
        }

        internal string RetrieveFromCategory(string categoryName) {
            switch (categoryName) {
                case "VERSE1":
                case "VERS1":
                case "V1":
                    return string.Join("\n", Vers1);

                case "VERSE2":
                case "VERS2":
                case "V2":
                    return string.Join("\n", Vers2);

                case "VERSE3":
                case "VERS3":
                case "V3":
                    return string.Join("\n", Vers3);

                case "VERSE4":
                case "VERS4":
                case "V4":
                    return string.Join("\n", Vers4);

                case "VERSE5":
                case "VERS5":
                case "V5":
                    return string.Join("\n", Vers5);

                case "VERSE6":
                case "VERS6":
                case "V6":
                    return string.Join("\n", Vers6);

                case "VERSE7":
                case "VERS7":
                case "V7":
                    return string.Join("\n", Vers7);

                case "VERSE8":
                case "VERS8":
                case "V8":
                    return string.Join("\n", Vers8);

                case "VERSE":
                case "VERS":
                    return string.Join("\n", Vers1);
                case "PRE-CHORUS2":
                case "PRECHORUS2":
                    return string.Join("\n", PreChorus2);
                case "PRE-CHORUS":
                case "PRECHORUS":
                    return string.Join("\n", PreChorus);
                case "CHORUS2":
                    return string.Join("\n", Chorus2);
                case "CHORUS":
                    return string.Join("\n", Chorus);

                case "BRIDGE1":
                    return string.Join("\n", Bridge1);

                case "BRIDGE2":
                    return string.Join("\n", Bridge2);

                case "BRIDGE3":
                    return string.Join("\n", Bridge3);

                case "BRIDGE":
                    return string.Join("\n", Bridge);

                case "INTERLUDE3":
                    return string.Join("\n", Interlude3);
                case "INTERLUDE2":
                    return string.Join("\n", Interlude2);
                case "INTERLUDE1":
                    return string.Join("\n", Interlude1);
                case "INTERLUDE":
                    return string.Join("\n", Interlude);
                case "INSTRUMENTAL":
                    return string.Join("\n", Instrumental);
                case "OUTRO":
                    return string.Join("\n", Outro);
                case "HOOK":
                    return string.Join("\n", Hook);
                case "ENDING":
                    return string.Join("\n", Ending);

                case "INTRO":
                    return string.Join("\n", Intro);

                default:
                    System.Windows.MessageBox.Show("Bitte Informieren Sie ihren zuständigen Entwickler unter dem Stichwort " + categoryName + " mit Fehlercode #1002");
                    throw new InvalidOperationException();
            }
        }

        public override string ToString() {
            return this.Title + " in " + Key;
        }
    }
}
