using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageDisplayConverter.Helpers
{
    internal class QualifierHelper
    {
        internal static Dictionary<string, System.Drawing.Color> QualifiersWithColors = new Dictionary<string, System.Drawing.Color> {
                { "VERS1" , System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "VERSE1" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "V1" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                { "VERS2" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "VERSE2" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "V2" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                { "VERS3" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "VERSE3" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "V3" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                { "VERS4" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "VERSE4" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "V4" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                { "VERS5" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "VERSE5" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "V5" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                { "VERS6" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "VERSE6" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "V6" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                { "VERS7" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "VERSE7" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "V7" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                { "VERS8" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "VERSE8" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                        { "V8" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                          { "VERSE" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
            { "VERS" ,System.Drawing.Color.FromArgb(255, 255,255,255) },
                      
                {  "PRECHORUS2",System.Drawing.Color.FromArgb(255, 255,217,102) },
                        {  "PRE-CHORUS2",System.Drawing.Color.FromArgb(255, 255,217,102) },
                {  "PRECHORUS",System.Drawing.Color.FromArgb(255, 255,217,102) },
                        {  "PRE-CHORUS",System.Drawing.Color.FromArgb(255, 255,217,102) },
                {  "CHORUS2",System.Drawing.Color.FromArgb(255, 255,255,0) },
                {  "CHORUS",System.Drawing.Color.FromArgb(255, 255,255,0) },
                {  "BRIDGE1",System.Drawing.Color.FromArgb(255, 15,219,15) },
                {  "BRIDGE2",System.Drawing.Color.FromArgb(255, 15,219,15) },
                {  "BRIDGE3",System.Drawing.Color.FromArgb(255, 15,219,15) },
                {  "BRIDGE",System.Drawing.Color.FromArgb(255, 15,219,15) },
                {  "INTERLUDE3",System.Drawing.Color.FromArgb(255,66, 238, 244) },
                {  "INTERLUDE2",System.Drawing.Color.FromArgb(255,66, 238, 244) },
                {  "INTERLUDE1",System.Drawing.Color.FromArgb(255,66, 238, 244) },
                {  "INTERLUDE",System.Drawing.Color.FromArgb(255,66, 238, 244) },
                {  "OUTRO",System.Drawing.Color.FromArgb(255, 0,176,240)},
                 {  "HOOK",System.Drawing.Color.FromArgb(255,66, 238, 244)},
                
                {  "INTRO",System.Drawing.Color.FromArgb(255, 204, 0, 0)},
                {  "ENDING",System.Drawing.Color.FromArgb(255, 204, 0, 0)},
            };

        internal static Dictionary<string, float> QualifiersWithTextHeight = new Dictionary<string, float> {
                {  "PRECHORUS2",(float)12 },
                {  "PRE-CHORUS2",(float)12 },
                {  "PRECHORUS",(float)12 },
                {  "PRE-CHORUS",(float)12 },
                {  "INTERLUDE3",(float)12 },
                {  "INTERLUDE2",(float)12 },
                {  "INTERLUDE1",(float)12 },
                {  "INTERLUDE",(float)12},
                {  "Default",(float)15 },

            };

        internal static Dictionary<string, string> QualifiersToOutputName = new Dictionary<string, string> {
                { "VERS1" , "VERS 1" },
                        { "VERSE1" ,"VERS 1" },
                        { "V1" ,"VERS 1"},
                { "VERS2" ,"VERS 2" },
                        { "VERSE2" ,"VERS 2" },
                        { "V2" ,"VERS 2" },
                { "VERS3" ,"VERS 3" },
                        { "VERSE3" ,"VERS 3" },
                        { "V3" ,"VERS 3" },
                { "VERS4" ,"VERS 4" },
                        { "VERSE4" ,"VERS 4" },
                        { "V4" ,"VERS 4" },
                { "VERS5" ,"VERS 5" },
                        { "VERSE5" ,"VERS 5" },
                        { "V5" ,"VERS 5"},
                { "VERS6" ,"VERS 6"},
                        { "VERSE6" ,"VERS 6" },
                        { "V6" ,"VERS 6" },
                { "VERS7" ,"VERS 7" },
                        { "VERSE7" ,"VERS 7" },
                        { "V7" ,"VERS 7" },
                { "VERS8" ,"VERS 8" },
                        { "VERSE8" ,"VERS 8" },
                        { "V8" ,"VERS 8" },
                        { "VERSE" ,"VERS" },
                { "VERS" ,"VERS" },
                        
                {  "PRECHORUS2","PRE-CH 2" },
                        {  "PRE-CHORUS2","PRE-CH 2" },
                {  "PRECHORUS","PRE-CH" },
                        {  "PRE-CHORUS","PRE-CH" },
                 {  "CHORUS2", "CH 2" },
                {  "CHORUS", "CH" },
                {  "BRIDGE1", "BR 1" },
                {  "BRIDGE2","BR 2" },
                {  "BRIDGE3","BR 3" },
                {  "BRIDGE","BR" },
                {  "HOOK","Hook" },
                {  "INTERLUDE3","Interl. 3" },
                {  "INTERLUDE2","Interl. 2" },
                {  "INTERLUDE1","Interl. 1" },
                {  "INTERLUDE","Interl." },
                {  "OUTRO", "Outro"},
                {  "ENDING", "Ending"},
                {  "INTRO", "Intro"}
             };
    }
}
