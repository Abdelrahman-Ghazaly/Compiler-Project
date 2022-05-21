using System;
using System.IO;
using System.Text;
using System.Data;
using System.Diagnostics;


namespace Compiler
{
    public partial class Home : System.Web.UI.Page
    {

        private void compile (String code)
        {
            lexer.Tokinize(code, table);
            lineNo2 = table.get("lineNo");
            lexeme2 = table.get("lexeme");
            ReturnToken2 = table.get("ReturnToken");
            lexemeNoInLine2 = table.get("lexemeNoInLine");
            matchability2 = table.get("matchability");
        }

        private string[] splitingCodeToLines(string code)
        {
            string[] lines = code.Split('\n');
            Console.WriteLine(lines);
            return lines;
        }

        private String deletingComments (string code)
        {
            //spliting code by "\n" to turn into lines
            string[] lines = splitingCodeToLines(code);
            //removing lines that has ***
            code = deleteSingleLine(lines);
            //spliting the returned code by "\n" to turn into lines
            lines = splitingCodeToLines(code);
            // removing the code between </ and />
            code = deletingMultiLine(lines);
            return code;
        }

        private string deleteSingleLine (string[] lines)
        {
            string code = "";

            foreach(string line in lines)
            {
                if (line.Contains("***"))
                {
                    //skipping the commented lines
                    continue;
                }
                else
                {
                    //addimg the existing line to code not comment (dosen't have ***)
                    code = code + line + '\n';
                }

            }

            return code;
        }

        private string deletingMultiLine(string[] lines)
        {
            string code = "";

            for(int index=0; index < lines.Length; index++)
            {
                if (lines[index].Contains("</"))
                {
                    //looping unti findind the end of the coomment "/>"
                    for(int i = index; i< lines.Length; i++)
                    {
                        if (lines[i].Contains("/>"))
                        {
                            index = i;
                            break;
                            //now the index equals to the end of the comment
                            //and will skip it.
                        }
                    }

                }
                else
                {
                    //adding non comment ele to the code string
                    code = code + lines[index] + "\n";
                }
            }

            return code;
        }

        private void scannerOutPut()
        {
            ParserOutPut.DataSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Line NO");
            dt.Columns.Add("Lexeme");
            dt.Columns.Add("Return Token");
            dt.Columns.Add("Lexeme No In Line");
            int lineSize = 0;
            while (lineNo2[lineSize] != null)
            {
                lineSize++;
            }
            for (int i=0;i< lineSize; i++)
            {              
                dt.Rows.Add(lineNo2[i],lexeme2[i],ReturnToken2[i],lexemeNoInLine2[i]);
            }
            ScannerOutPut.DataSource = dt;
            ScannerOutPut.DataBind();            
        }

        private void parserOutPut()
        {
            ScannerOutPut.DataSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Line NO");
            dt.Columns.Add("Lexeme");
            dt.Columns.Add("Matchability");
            int lineSize = 0;
            while (lineNo2[lineSize] != null)
            {
                lineSize++;
            }
            for (int i = 0; i < lineSize; i++)
            {
                dt.Rows.Add(lineNo2[i], lexeme2[i], matchability2[i]);
            }
            ParserOutPut.DataSource = dt;
            ParserOutPut.DataBind();
        }

        Dictionary_ table = Dictionary_.GetInstance();
        public static string[] lineNo2 = new string[1000];
        public static string[] lexeme2 = new string[1000];
        public static string[] ReturnToken2 = new string[1000];
        public static string[] lexemeNoInLine2 = new string[1000];
        public static string[] matchability2 = new string[1000];
        Lexer lexer = new Lexer();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Parse_Click(object sender, EventArgs e)
        {
            string code = Request.Form["CodeArea"];
            code = deletingComments(code);
            compile(code);
            parserOutPut();
        }

        protected void comment_Click(object sender, EventArgs e)
        {

        }

        protected void uncomment_Click(object sender, EventArgs e)
        {

        }

        protected void Browse_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileName = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Data/") + fileName);
            }

            foreach (string strfile in Directory.GetFiles(Server.MapPath("~/Data/")))
            {
                FileInfo fi = new FileInfo(strfile);

                using (FileStream fs = File.Open(strfile, FileMode.Open, FileAccess.Read))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string code="";

                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        code += temp.GetString(b);
                    }
                    compile(code);
                    scannerOutPut();
                }
            }
        }

        protected void scan_Click(object sender, EventArgs e)
        {            
            string code = Request.Form["CodeArea"];
            compile(code);
            code = deletingComments(code);
            scannerOutPut();
        }
    }
}
