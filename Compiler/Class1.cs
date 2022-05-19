using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Compiler
{
    public class Dictionary_
    {
        string[] s_output = { "lineNo", "lexeme", "ReturnToken", "lexemeNoInLine", "matchability" };
        static int counter1 = 0;
        static int counter2 = 0;
        static int counter3 = 0;
        static int counter4 = 0;
        static int counter5 = 0;
        EnvironmentVariableTarget i;
        // Instance Variables
        static string[] lineNo = new string[1000];
        static string[] lexeme = new string[1000];
        static string[] ReturnToken = new string[1000];
        static string[] matchability = new string[1000];
        static string[] lexemeNoInLine = new string[1000];

        // Constructor Declaration of Class
        private Dictionary_() { }
        private static Dictionary_ _instance;
        public static Dictionary_ GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Dictionary_();
            }
            ClearAll();
            return _instance;
        }
        public void add(string key, dynamic value)
        {
            foreach (string item in s_output)
            {
                if (item.Equals(key))
                {
                    if (item == "lineNo")
                        lineNo[counter1++] = value;
                    else if (item == "lexeme")
                        lexeme[counter2++] = value;
                    else if (item == "ReturnToken")
                        ReturnToken[counter3++] = value;
                    else if (item == "lexemeNoInLine")
                        lexemeNoInLine[counter4++] = value;
                    else if (item == "matchability")
                        matchability[counter5++] = value;
                }
            }
        }

        public static void ClearAll()
        {

            lineNo = new string[1000];
            lexeme = new string[1000];
            ReturnToken = new string[1000];
            lexemeNoInLine = new string[1000];
            matchability = new string[1000];
            counter1 = 0;
            counter2 = 0;
            counter3 = 0;
            counter4 = 0;
            counter5 = 0;
        }
        public string[] get(string key)
        {
            if (key == "lineNo")
                return lineNo;
            else if (key == "lexeme")
                return lexeme;
            else if (key == "ReturnToken")
                return ReturnToken;
            else if (key == "lexemeNoInLine")
                return lexemeNoInLine;
            else
                return matchability;
        }


    }
}