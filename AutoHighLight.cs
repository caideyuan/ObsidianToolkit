using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ObAuto
{
    public class AutoHighLight
    {
        List<string> keywords = new List<string>();
        List<string> functions = new List<string>();
        List<string> strings = new List<string>();
        List<string> whitespace = new List<string>();

        public AutoHighLight()
        {
        }
        public AutoHighLight(List<string> keywords,List<string> functions, List<string> strings, List<string> whitespace)
        {
            this.keywords = keywords;
            this.functions = functions;
            this.strings = strings;
            this.whitespace = whitespace;
        }

        public void SetRtbTextColor(ComponentFactory.Krypton.Toolkit.KryptonRichTextBox kryRTbx)
        {
            kryRTbx.SelectAll();
            kryRTbx.SelectionColor = Color.Black;
            SetRtbTextColor(kryRTbx, keywords, Color.Blue);
            //SetRtbTextColor(kryRTbx, functions, Color.Magenta);
            //SetRtbTextColor(kryRTbx, strings, Color.Red);
            //SetRtbTextColor(kryRTbx, whitespace, Color.Black);
        }
        public void SetRtbTextColor(ComponentFactory.Krypton.Toolkit.KryptonRichTextBox kryRTbx, List<string> words, Color color)
        {
            foreach (string word in words)
            {
                Regex r = new Regex(word);
                foreach (Match m in r.Matches(kryRTbx.Text))
                {
                    kryRTbx.Select(m.Index, m.Length);
                    kryRTbx.SelectionColor = color;
                }
            }
        }

        public void SetWords()
        {
            this.keywords.AddRange(new string[] { 
                "auto", "double", "int", "struct", "break", "else", "long", "switch", "case", "enum", "register", "typedef", "char", "extern", "return", "union", "const", "float", "short", "unsigned", "continue", "for", "signed", "void", "default", "goto", "sizeof", "volatile", "do", "if", "static", "while" 
            });
            this.functions = null;
            this.strings = new List<string>();
            this.whitespace.AddRange(new string[] { "\t","\n"," " });
        }
    }
}
