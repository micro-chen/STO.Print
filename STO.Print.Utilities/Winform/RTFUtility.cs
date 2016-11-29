using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// RTF字符格式辅助类
    /// </summary>
    public static class RTFUtility
    {
        static RTFUtility()
        {

        }

        public static string Bold(string s)
        {
            return "\\b\r\n" + s + "\\b0\r\n";
        }

        public static string Italic(string s)
        {
            return "\\i\r\n" + s + "\\i0\r\n";
        }

        public static string Underline(string s)
        {
            return "\\ul\r\n" + s + "\\ul0\r\n";
        }

        public static string LineBreak()
        {
            return LineBreak(1);
        }

        public static string LineBreak(int n)
        {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                s += "\\par\r\n";
            }
            return s;
        }

        public static string AlignCenter(string s)
        {
            return "\r\n\\qc\r\n" + s;
        }

        public static string AlignLeft(string s)
        {
            return "\r\n\\ql\r\n" + s;
        }

        public static string AlignRight(string s)
        {
            return "\r\n\\qr\r\n" + s;
        }

        public static string AlignFull(string s)
        {
            return "\r\n\\qf\r\n" + s;
        }

        public static string FontSize(string s, int n)
        {
            return "\r\n\\fs" + Convert.ToString(n * 2) + "\r\n" + s;
        }

        public static string ParagraphBorder(string s)
        {
            s = s.Replace("\\par", "\\line");
            return "\\par {\\pard \\brdrt \\brdrs \\brdrw10 \\brsp125 \\brdrl \\brdrs \\brdrw10 \\brsp125 \\brdrb \\brdrs \\brdrw10 \\brsp125 \\brdrr \\brdrs \\brdrw10 \\brsp125 " +
                s + " \\par}";
        }

        public static string BuildTable(int NumRows, int NumCells, ArrayList values)
        {
            StringBuilder sb = new StringBuilder();
            int totalValues = NumRows * NumCells;
            //pad with blanks
            for (int i = values.Count; i <= totalValues; i++)
            {
                values.Add("");
                //Console.WriteLine("padding...");
            }
            IEnumerator enumer = values.GetEnumerator();
            enumer.MoveNext();

            int j = 1;
            // write table contents
            for (int k = 1; k <= NumRows; k++)
            {
                sb.Append("\\trowd\\trautofit1\\intbl");
                j = 1;
                for (int i = 1; i <= NumCells; i++)
                {
                    sb.Append("\\cellx" + j);
                    j = j + 1;
                }

                sb.Append("{");

                //DATA FIELDS
                for (int i = 1; i <= NumCells; i++)
                {
                    //Console.WriteLine(enumer.Current);
                    sb.Append("\\pard " + enumer.Current.ToString() + "\\cell \\pard");
                    enumer.MoveNext();

                }

                sb.Append("}");

                sb.Append("{");

                sb.Append("\\trowd\\trautofit1\\intbl\\trqc");
                j = 1;
                for (int i = 1; i <= NumCells; i++)
                {
                    sb.Append("\\clpadt100\\clpadft3\\clpadr100\\clpadfr3\\clwWidth1\\clftsWidth\\clwWidth1\\clftsWidth1\\clbrdrt\\brdrs\\brdrw10\\clbrdrl\\brdrs\\brdrw10\\clbrdrb\\brdrs\\brdrw10\\clbrdrr\\brdrs\\brdrw10\\clNoWrap\\cellx" + j);
                    j = j + 1;
                }
                sb.Append("\\row }");

            }
            return "\n\\pard\\par\n{" + sb.ToString() + "}\n\\pard\n";
        }


        /// <summary>
        /// RTF文本转换成HTML文本。
        /// </summary>
        /// <returns></returns>
        public static string RtfToHtml(RichTextBox m_pText)
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("<html>\r\n");

            // Go to text start.
            m_pText.SelectionStart = 0;
            m_pText.SelectionLength = 1;

            Font currentFont = m_pText.SelectionFont;
            Color currentSelectionColor = m_pText.SelectionColor;
            Color currentBackColor = m_pText.SelectionBackColor;

            int numberOfSpans = 0;
            int startPos = 0;
            while (m_pText.Text.Length > m_pText.SelectionStart)
            {
                m_pText.SelectionStart++;
                m_pText.SelectionLength = 1;

                // Font style or size or color or back color changed
                if (m_pText.Text.Length == m_pText.SelectionStart || (currentFont.Name != m_pText.SelectionFont.Name || currentFont.Size != m_pText.SelectionFont.Size || currentFont.Style != m_pText.SelectionFont.Style || currentSelectionColor != m_pText.SelectionColor || currentBackColor != m_pText.SelectionBackColor))
                {
                    string currentTextBlock = m_pText.Text.Substring(startPos, m_pText.SelectionStart - startPos);

                    //--- Construct text bloxh html -----------------------------------------------------------------//
                    // Make colors to html color syntax: #hex(r)hex(g)hex(b)
                    string htmlSelectionColor = "#" + currentSelectionColor.R.ToString("X2") + currentSelectionColor.G.ToString("X2") + currentSelectionColor.B.ToString("X2");
                    string htmlBackColor = "#" + currentBackColor.R.ToString("X2") + currentBackColor.G.ToString("X2") + currentBackColor.B.ToString("X2");
                    string textStyleStartTags = "";
                    string textStyleEndTags = "";
                    if (currentFont.Bold)
                    {
                        textStyleStartTags += "<b>";
                        textStyleEndTags += "</b>";
                    }
                    if (currentFont.Italic)
                    {
                        textStyleStartTags += "<i>";
                        textStyleEndTags += "</i>";
                    }
                    if (currentFont.Underline)
                    {
                        textStyleStartTags += "<u>";
                        textStyleEndTags += "</u>";
                    }
                    retVal.Append("<span\n style=\"color:" + htmlSelectionColor + "; font-size:" + currentFont.Size + "pt; font-family:" + currentFont.FontFamily.Name + "; background-color:" + htmlBackColor + ";\">" + textStyleStartTags + currentTextBlock.Replace("\n", "</br>") + textStyleEndTags);
                    //-----------------------------------------------------------------------------------------------//

                    startPos = m_pText.SelectionStart;
                    currentFont = m_pText.SelectionFont;
                    currentSelectionColor = m_pText.SelectionColor;
                    currentBackColor = m_pText.SelectionBackColor;
                    numberOfSpans++;
                }
            }

            for (int i = 0; i < numberOfSpans; i++)
            {
                retVal.Append("</span>");
            }

            retVal.Append("\r\n</html>\r\n");

            return retVal.ToString();
        }

    } //end RTFUtility
}
